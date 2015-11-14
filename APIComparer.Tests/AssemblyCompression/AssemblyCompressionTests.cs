using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using APIComparer;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using NUnit.Framework;

[TestFixture]
[Explicit]
public class AssemblyCompressionTests
{
    [Test]
    public void Run()
    {
        var assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var directory = Path.Combine(assemblyLocation, "AssemblyCompression");
        var beforeAssemblyPath = Path.Combine(directory, "NServiceBus.Core.dll");
        var moduleDefinition = ModuleDefinition.ReadModule(beforeAssemblyPath);
        CompressAssembly(moduleDefinition);
        var afterAssemblyPath = beforeAssemblyPath.Replace(".dll", "2.dll");
        File.Delete(afterAssemblyPath);

        var writerParameters = new WriterParameters
        {
            WriteSymbols = true
        };
        moduleDefinition.Write(afterAssemblyPath, writerParameters);
        var beforeSize = new FileInfo(beforeAssemblyPath).Length + new FileInfo(beforeAssemblyPath.Replace(".dll", ".pdb")).Length;
        Trace.WriteLine($"before {beforeSize/1024} kbytes");
        var afterSize = new FileInfo(afterAssemblyPath).Length + new FileInfo(afterAssemblyPath.Replace(".dll", ".pdb")).Length;
        Trace.WriteLine($"after {afterSize/1024} kbytes");

        Verifier.Verify(afterAssemblyPath);
    }

    private static void CompressAssembly(ModuleDefinition moduleDefinition)
    {
        moduleDefinition.RemoveUnwantedAttributes();
        moduleDefinition.Assembly.RemoveUnwantedAttributes();
        moduleDefinition.Resources.Clear();

        foreach (var typeToRemove in moduleDefinition.GetTypes().Where(x => x.IsCompilerGenerated()).ToList())
        {
            moduleDefinition.RemoveType(typeToRemove);
        }

        foreach (var type in moduleDefinition.GetTypes())
        {
            ProcessType(moduleDefinition, type);
        }
    }

    private static void ProcessType(ModuleDefinition moduleDefinition, TypeDefinition type)
    {
        foreach (var toRemove in type.Properties.Where(x => x.IsCompilerGenerated()).ToList())
        {
            type.Properties.Remove(toRemove);
        }
        foreach (var toRemove in type.Methods.Where(x => x.IsCompilerGenerated() && !x.Name.StartsWith("get_") && !x.Name.StartsWith("set_")).ToList())
        {
            type.Methods.Remove(toRemove);
        }
        foreach (var toRemove in type.Fields.Where(x => x.IsCompilerGenerated()).ToList())
        {
            type.Fields.Remove(toRemove);
        }

        foreach (var property in type.Properties)
        {
            property.RemoveUnwantedAttributes();
        }

        foreach (var field in type.Fields)
        {
            field.RemoveUnwantedAttributes();
        }

        var exceptionReference = new TypeReference("System", "Exception", moduleDefinition.TypeSystem.String.Module, moduleDefinition.TypeSystem.String.Scope);
        exceptionReference = moduleDefinition.Import(exceptionReference);
        var ctor = moduleDefinition.Import(exceptionReference.Resolve().GetConstructors().First(c => !c.HasParameters));

        foreach (var method in type.Methods)
        {
            method.RemoveUnwantedAttributes();
            if (method.HasBody)
            {
                //todo: preserve a single pdb line
                var body = method.Body;
                var validSequencePoint = method.GetValidSequencePoint();
                body.Variables.Clear();
                body.ExceptionHandlers.Clear();
                body.Instructions.Clear();

                body.Instructions.Add(Instruction.Create(OpCodes.Newobj, ctor));

                var instruction = Instruction.Create(OpCodes.Throw);
                if (validSequencePoint != null)
                {
                    instruction.SequencePoint = validSequencePoint;
                }
                body.Instructions.Add(instruction);
            }
        }
    }

    public int Foo()
    {
        throw new Exception();
    }
}
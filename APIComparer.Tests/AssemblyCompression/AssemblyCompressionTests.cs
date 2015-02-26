using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using APIComparer;
using Mono.Cecil;
using NUnit.Framework;

[TestFixture]
[Explicit]
public class AssemblyCompressionTests
{
    [Test]
    public void Run()
    {
        var directory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AssemblyCompression");
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
        Debug.WriteLine(new FileInfo(beforeAssemblyPath).Length);
        Debug.WriteLine(new FileInfo(afterAssemblyPath).Length);
    }

    static void CompressAssembly(ModuleDefinition moduleDefinition)
    {
        moduleDefinition.Resources.Clear();

        List<TypeDefinition> typesToRemove = new List<TypeDefinition>();
        foreach (var typeDefinition in moduleDefinition.GetTypes())
        {
            //if (typeDefinition.ContainsAttribute<CompilerGeneratedAttribute>())
            //{
            //    typesToRemove.Add(typeDefinition);
            //    continue;
            //}

            if (typeDefinition.Name.Contains("f__AnonymousType0"))
            {
                Debug.WriteLine(typeDefinition.FullName);
                foreach (var result in typeDefinition.CustomAttributes.Select(x => x.AttributeType.Name))
                {
                    Debug.WriteLine(result);
                }
            }
            foreach (var methodDefinition in typeDefinition.Methods)
            {
                if (methodDefinition.HasBody)
                {
                    //todo: preserve a single pdb line
                    methodDefinition.Body.ExceptionHandlers.Clear();
                    methodDefinition.Body.Instructions.Clear();
                }
            }
        }
        moduleDefinition.RemoveTypes(typesToRemove);
    }

}

using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;

public static class Verifier
{
    static Verifier()
    {
        exePath = Environment.ExpandEnvironmentVariables(@"%programfiles(x86)%\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\PEVerify.exe");

        if (!File.Exists(exePath))
        {
            exePath = Environment.ExpandEnvironmentVariables(@"%programfiles(x86)%\Microsoft SDKs\Windows\v8.0A\Bin\NETFX 4.0 Tools\PEVerify.exe");
        }
        peverifyFound = File.Exists(exePath);
        if (!peverifyFound)
        {
            Assert.Ignore("Could not find PEVerify");
        }
    }

    public static void Verify(string assemblyPath)
    {
        if (!peverifyFound)
        {
            Assert.Ignore("Could not find PEVerify");
        }
        var output = Validate(assemblyPath);

        var stringReader = new StringReader(output);
        string line;
        while ((line = stringReader.ReadLine()) != null)
        {
            if (line.Contains("Error"))
            {
                if (line.Contains("The system cannot find the file specified."))
                {
                    continue;
                }
                if (line.Contains("Unable to resolve token."))
                {
                    continue;
                }

                Trace.WriteLine(line);
            }
        }
    }

    public static string Validate(string assemblyPath2)
    {
        var process = Process.Start(new ProcessStartInfo(exePath, "\"" + assemblyPath2 + "\"")
        {
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        });

        process.WaitForExit();
        return process.StandardOutput.ReadToEnd().Trim().Replace(assemblyPath2, "");
    }

    private static string exePath;
    private static bool peverifyFound;
}
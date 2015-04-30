namespace APIComparer.Backend.Reporting
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;
    using HandlebarsDotNet;

    public class Helpers_Html
    {
        private static readonly Regex codify = new Regex(@"`(?<x>[\w\.\(\)]*)`", RegexOptions.Compiled);

        public static void Codify(TextWriter writer, dynamic context, params object[] args)
        {
            if (args.Length != 1)
            {
                throw new InvalidOperationException("Codify helper only supports one argument");
            }

            var input = args[0] as string ?? string.Empty;
            writer.WriteSafeString(codify.Replace(input, @"<code>${x}</code>"));
        }
    }
}
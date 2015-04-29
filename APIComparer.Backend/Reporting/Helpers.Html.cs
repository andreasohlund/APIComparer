namespace APIComparer.Backend.Reporting
{
    using System;
    using System.IO;
    using HandlebarsDotNet;

    public class Helpers_Html
    {
        public static void Codify(TextWriter writer, dynamic context, params object[] args)
        {
            if (args.Length != 1)
            {
                throw new InvalidOperationException("Codify helper only supports one argument");
            }
            // TODO
            writer.WriteSafeString(args[0]);
        }
    }
}
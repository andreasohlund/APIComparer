namespace APIComparer.Backend.Reporting
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using APIComparer.VersionComparisons;
    using HandlebarsDotNet;

    public class APIUpgradeToHtmlFormatter
    {
        static Action<TextWriter, object> template;

        static APIUpgradeToHtmlFormatter()
        {
            var partials = typeof(Templates_Html)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Select(m => new
                {
                    Name = m.Name.ToLowerInvariant(),
                    Invoke = new Func<string>(() => (string)m.Invoke(null, null))
                }).ToArray();

            foreach (var @partial in partials)
            {
                if (@partial.Name == "comparison")
                {
                    using (var templateReader = new StringReader(@partial.Invoke()))
                    {
                        template = Handlebars.Compile(templateReader);
                        templateReader.Close();
                    }
                    continue;
                }

                using (var templateReader = new StringReader(@partial.Invoke()))
                {
                    var partialTemplate = Handlebars.Compile(templateReader);
                    Handlebars.RegisterTemplate(@partial.Name, partialTemplate);
                }
            }
        }

        public void Render(TextWriter writer, PackageDescription description, DiffedCompareSet[] diffedCompareSets)
        {
            var data = ViewModelBuilder.Build(description, diffedCompareSets);

            template(writer, data);
        }
    }
}
namespace APIComparer.Backend.Reporting
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Mime;
    using System.Reflection;
    using HandlebarsDotNet;

    public class APIUpgradeToHtmlFormatter : IFormatter
    {
        static APIUpgradeToHtmlFormatter()
        {
            DynamicLoadHelpers();
            DynamicLoadTemplateAndPartials();
        }

        public ContentType ContentType => new ContentType("text/html");

        public void Render(TextWriter writer, PackageDescription description, DiffedCompareSet[] diffedCompareSets)
        {
            var data = ViewModelBuilder.Build(description, diffedCompareSets);

            template(writer, data);
        }

        private static void DynamicLoadHelpers()
        {
            var blockHelpers = from helper in typeof(Helpers_Html).GetMethods(BindingFlags.Static | BindingFlags.Public)
                let action = Delegate.CreateDelegate(typeof(HandlebarsBlockHelper), null, helper, false) as HandlebarsBlockHelper
                where action != null
                select new
                {
                    Name = helper.Name.ToLowerInvariant(),
                    Invoke = action
                };

            foreach (var blockHelper in blockHelpers)
            {
                Handlebars.RegisterHelper(blockHelper.Name, blockHelper.Invoke);
            }

            var helpers = from helper in typeof(Helpers_Html).GetMethods(BindingFlags.Static | BindingFlags.Public)
                let action = Delegate.CreateDelegate(typeof(HandlebarsHelper), null, helper, false) as HandlebarsHelper
                where action != null
                select new
                {
                    Name = helper.Name.ToLowerInvariant(),
                    Invoke = action
                };

            foreach (var helper in helpers)
            {
                Handlebars.RegisterHelper(helper.Name, helper.Invoke);
            }
        }

        private static void DynamicLoadTemplateAndPartials()
        {
            var partials = typeof(Templates_Html)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Select(m => new
                {
                    Name = m.Name.ToLowerInvariant(),
                    Invoke = new Func<string>(() => (string) m.Invoke(null, null))
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

        private static Action<TextWriter, object> template;
    }
}
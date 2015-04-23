namespace APIComparer.Backend
{
    using System;
    using System.IO;
    using HandlebarsDotNet;

    public class CompareViewEngine
    {
        static Action<TextWriter, object> template;

        static CompareViewEngine()
        {
            using (var templateReader = File.OpenText("./publicinternal.tpl"))
            {
                var partialTemplate = Handlebars.Compile(templateReader);
                Handlebars.RegisterTemplate("publicinternal", partialTemplate);
            }

            using (var templateReader = File.OpenText("./obsolete.tpl"))
            {
                var partialTemplate = Handlebars.Compile(templateReader);
                Handlebars.RegisterTemplate("obsolete", partialTemplate);
            }

            using (var templateReader = File.OpenText("./target.tpl"))
            {
                var partialTemplate = Handlebars.Compile(templateReader);
                Handlebars.RegisterTemplate("target", partialTemplate);
            }

            using (var templateReader = File.OpenText("./comparisontemplate.tpl"))
            {
                template = Handlebars.Compile(templateReader);
                templateReader.Close();
            }
        }


        public void Render(TextWriter writer, object data)
        {
            template(writer, data);
        }
    }
}

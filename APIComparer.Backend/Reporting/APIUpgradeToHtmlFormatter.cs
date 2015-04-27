namespace APIComparer.Backend.Reporting
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using APIComparer.VersionComparisons;
    using HandlebarsDotNet;
    using Mono.Cecil;

    public class APIUpgradeToHtmlFormatter
    {
        static Action<TextWriter, object> template;

        static APIUpgradeToHtmlFormatter()
        {
            using (var templateReader = File.OpenText("./reporting/obsolete.tpl"))
            {
                var partialTemplate = Handlebars.Compile(templateReader);
                Handlebars.RegisterTemplate("obsolete", partialTemplate);
            }

            using (var templateReader = File.OpenText("./reporting/madeinternal.tpl"))
            {
                var partialTemplate = Handlebars.Compile(templateReader);
                Handlebars.RegisterTemplate("madeinternal", partialTemplate);
            }

            using (var templateReader = File.OpenText("./reporting/removedpublic.tpl"))
            {
                var partialTemplate = Handlebars.Compile(templateReader);
                Handlebars.RegisterTemplate("removedpublic", partialTemplate);
            }

            using (var templateReader = File.OpenText("./reporting/target.tpl"))
            {
                var partialTemplate = Handlebars.Compile(templateReader);
                Handlebars.RegisterTemplate("target", partialTemplate);
            }

            using (var templateReader = File.OpenText("./reporting/comparison.tpl"))
            {
                template = Handlebars.Compile(templateReader);
                templateReader.Close();
            }
        }

        public void Render(TextWriter writer, PackageDescription description, DiffedCompareSet[] diffedCompareSets)
        {
            var data = ViewModelBuilder.Build(description, diffedCompareSets);

            //var matchingTypeDiffs = diff.MatchingTypeDiffs.ToList();
            //if (matchingTypeDiffs.Any())
            //{
            //    writer.WriteLine();
            //    writer.WriteLine("## The following types have differences.");
            //    writer.WriteLine();
            //    foreach (var typeDiff in diff.MatchingTypeDiffs)
            //    {
            //        if (typeDiff.LeftType.HasObsoleteAttribute())
            //        {
            //            continue;
            //        }
            //        if (typeDiff.RightType.HasObsoleteAttribute())
            //        {
            //            continue;
            //        }

            //        if (!typeDiff.LeftType.IsPublic)
            //        {
            //            continue;
            //        }
            //        if (!typeDiff.RightType.IsPublic)
            //        {
            //            continue;
            //        }
            //        if (HasDifferences(typeDiff))
            //        {
            //            WriteOut(typeDiff, writer, info);
            //        }
            //    }
            //}

            template(writer, data);
        } 
    }

    public class ViewModelBuilder
    {
        public static object Build(PackageDescription description, DiffedCompareSet[] diffedCompareSets)
        {
            return new
            {
                targets = BuildTargets(description, diffedCompareSets)
            };
        }

        static IEnumerable<object> BuildTargets(PackageDescription description, DiffedCompareSet[] diffedCompareSets)
        {
            return 
                from diffedSet in diffedCompareSets
                let diff = diffedSet.Diff
                let set = diffedSet.Set
                let removedPublicTypes = BuildRemovedPublicTypes(description, diff)
                let typesMadeInternal = BuildTypesMadeInternal(description, diff)
                let obsoletes = BuildTypesObsoleted(description, diff)
                select new
                {
                    set.Name,
                    set.ComparedTo,
                    noLongerSupported = diff is EmptyDiff,
                    hasRemovedPublicTypes = removedPublicTypes.Any(),
                    removedPublicTypes,
                    hasTypesMadeInternal = typesMadeInternal.Any(),
                    typesMadeInternal,
                    hasObsoletes = obsoletes.Any(),
                    obsoletes
                };
        }

        static IEnumerable<object> BuildRemovedPublicTypes(PackageDescription description, Diff diff)
        {
            foreach (TypeDefinition definition in diff.RemovedPublicTypes())
            {
                yield return new
                {
                    name = definition.GetName()
                };
            }
        }

        static IEnumerable<object> BuildTypesMadeInternal(PackageDescription description, Diff diff)
        {
            foreach (TypeDiff typeDiff in diff.TypesChangedToNonPublic())
            {
                yield return new
                {
                    name = typeDiff.RightType.GetName()
                };
            }
        }

        static IEnumerable<object> BuildTypesObsoleted(PackageDescription description, Diff diff)
        {
            foreach (TypeDefinition typeDiff in diff.RightAllTypes.TypeWithObsoletes())
            {
                yield return new
                {
                    name = typeDiff.GetName()
                };
            }
        }
    }
}
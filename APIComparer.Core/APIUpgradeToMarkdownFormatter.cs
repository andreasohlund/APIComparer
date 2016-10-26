namespace APIComparer
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class APIUpgradeToMarkdownFormatter
    {
        public void WriteOut(List<ApiChanges> apiChanges, TextWriter writer)
        {
            var currentChanges = apiChanges.Single(c => c.Version == "Current");
            var upcomingChanges = apiChanges.Where(c => c.Version != "Current").ToList();
            if (upcomingChanges.Any())
            {
                writer.WriteLine("# Changes in current version");
                writer.WriteLine();
            }

            WriteOut(currentChanges, writer);

            foreach (var change in upcomingChanges)
            {
                writer.WriteLine($"# Upcoming changes in Version - {change.Version}");
                writer.WriteLine();

                WriteOut(change, writer);
            }
        }

        void WriteOut(ApiChanges apiChanges, TextWriter writer)
        {
            if (apiChanges.NoLongerSupported)
            {
                writer.WriteLine("No longer supported");
                return;
            }

            if (apiChanges.RemovedTypes.Any())
            {
                writer.WriteLine("## The following types are no longer available");
                writer.WriteLine();

                foreach (var removedType in apiChanges.RemovedTypes)
                {
                    WriteRemovedType(writer, removedType, 3);
                }
            }

            if (apiChanges.ChangedTypes.Any())
            {
                writer.WriteLine("## Types with removed members");
                writer.WriteLine();

                foreach (var changedType in apiChanges.ChangedTypes)
                {
                    WriteChangedType(writer, changedType, 3);
                }
            }

        }

        static void WriteRemovedType(TextWriter writer, RemovedType removedType, int headingSize)
        {
            writer.WriteLine($"{new string('#', headingSize)} {removedType.Name}");

            var upgradeInstructions = removedType.UpgradeInstructions ?? "No upgrade instructions provided.";

            writer.WriteLine(upgradeInstructions);
            writer.WriteLine();
        }

        static void WriteChangedType(TextWriter writer, ChangedType changedType, int headingSize)
        {
            writer.WriteLine($"{new string('#', headingSize)} {changedType.Name}");

            var removedFields = changedType.RemovedMembers.Where(tc => tc.IsField)
                .ToList();

            if (removedFields.Any())
            {
                writer.WriteLine($"{new string('#', headingSize + 1)} Removed fields");
                writer.WriteLine();

                foreach (var typeChange in removedFields)
                {
                    WriteRemovedMember(writer, typeChange);
                }

                writer.WriteLine();
            }

            var removedMethods = changedType.RemovedMembers.Where(tc => tc.IsMethod)
              .ToList();

            if (removedMethods.Any())
            {
                writer.WriteLine($"{new string('#', headingSize + 1)} Removed methods");
                writer.WriteLine();

                foreach (var typeChange in removedMethods)
                {
                    WriteRemovedMember(writer, typeChange);
                }

                writer.WriteLine();
            }

            var changedEnumMembers = changedType.ChangedEnumMembers;

            if (changedEnumMembers.Any())
            {
                writer.WriteLine($"{new string('#', headingSize + 1)} Changed Enum Members");
                writer.WriteLine();

                foreach (var typeChange in changedEnumMembers)
                {
                    WriteRemovedMember(writer, typeChange);
                }

                writer.WriteLine();
            }
        }

        static void WriteRemovedMember(TextWriter writer, ChangedType.RemovedMember removedMember)
        {
            var upgradeInstructions = removedMember.UpgradeInstructions ?? "No upgrade instructions provided.";
            writer.WriteLine($"* `{removedMember.Name}` - {upgradeInstructions}");
        }

        static void WriteRemovedMember(TextWriter writer, ChangedType.ChangedEnumMember changedMember)
        {
            var upgradeInstructions = changedMember.UpgradeInstructions ?? "No upgrade instructions provided.";
            writer.WriteLine($"* `{changedMember.Name}` - {upgradeInstructions}");
        }
    }
}
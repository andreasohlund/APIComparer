namespace APIComparer.Backend.Reporting
{
    public class Templates_Html
    {
        // Root template
        public static string Comparison()
        {
            return @"
{{ #targets }}
{{> target}}
{{ /targets }}";
        }

        public static string Target()
        {
            return @"
<h1>{{ Name }}(Compared to {{ ComparedTo }})</h1>
{{#if noLongerSupported }}
<p>No longer supported.</p>
{{else}}
{{> removedpublic }}
{{> madeinternal }}
{{> typedifference }}
{{> obsolete }}
{{/if}}
";
        }

        public static string RemovedPublic()
        {
            return @"
{{ #if hasRemovedPublicTypes }}
<h2>The following public types have been removed</h2>
<ul>
  {{ #removedPublicTypes }}
  <li><code>{{ name }}</code></li>
  {{ /removedPublicTypes }}
</ul>
{{/if}}
";
        }

        public static string MadeInternal()
        {
            return @"
{{ #if hasTypesMadeInternal }}
<h2>The following public types have been made internal.</h2>
<ul>
  {{ #typesMadeInternal }}
  <li><code>{{name}}</code></li>
  {{ /typesMadeInternal }}
</ul>
{{/if}}
";
        }

        public static string FieldsChangedToNonPublic()
        {
            return @"
{{ #if hasFieldsChangedToNonPublic }}
<h4>Fields changed to non-public</h4>
<ul>
  {{ #fieldsChangedToNonPublic }}
  <li><code>{{ name }}</code></li>
  {{ /fieldsChangedToNonPublic }}
</ul>
{{ /if }}
";
        }

        public static string FieldsRemoved()
        {
            return @"
{{ #if hasFieldsRemoved }}
<h4>Fields Removed</h4>
<ul>
  {{ #fieldsRemoved }}
  <li><code>{{ name }}</code></li>
  {{/fieldsRemoved }}
</ul>
{{ /if }}
";
        }

        public static string MethodsChangedToNonPublic()
        {
            return @"
{{ #if hasMethodsChangedToNonPublic }}
<h4>Methods changed to non-public</h4>
<ul>
  {{ #methodsChangedToNonPublic }}
  <li><code>{{ name }}</code></li>
  {{ /methodsChangedToNonPublic }}
</ul>
{{ /if }}
";
        }

        public static string MethodsRemoved()
        {
            return @"
{{ #if hasMethodsRemoved }}
<h4>Methods Removed</h4>
<ul>
  {{ #methodsRemoved }}
  <li><code>{{ name }}</code></li>
  {{ /methodsRemoved }}
</ul>
{{ /if }}
";
        }

        public static string TypeDifference()
        {
            return @"
{{#if hasTypeDifferences }}
<h2>The following types have differences.</h2>
{{#typeDifferences }}
<h3>{{ name }}</h3>
{{> fieldsChangedToNonPublic }}
{{> fieldsRemoved }}
{{> methodsChangedToNonPublic }}
{{> methodsRemoved }}
{{/typeDifferences }}
{{/if}}
";
        }

        public static string ObsoleteMethods()
        {
            return @"
{{ #if hasObsoleteMethods }}
<h4>Obsolete Methods</h4>
<ul>
  {{ #obsoleteMethods }}
  <li><code>{{ name }}</code><br/> {{ codify obsolete }}</li>
  {{/obsoleteMethods }}
</ul>
{{ /if }}
";
        }

        public static string ObsoleteFields()
        {
            return @"
{{ #if hasObsoleteFields }}
<h4>Obsolete Fields</h4>
<ul>
  {{ #obsoleteFields }}
  <li><code>{{ name }}</code><br/> {{ codify obsolete }}</li>
  {{/obsoleteFields }}
</ul>
{{ /if }}
";
        }

        public static string Obsolete()
        {
            return @"
{{#if hasObsoletes}}
<h2>The following types have Obsoletes.</h2>
{{#obsoletes}}
<h3>{{name}}</h3>
{{> obsoleteFields }}
{{> obsoleteMethods }}
{{/obsoletes}}
{{/if}}
";
        }
    }
}
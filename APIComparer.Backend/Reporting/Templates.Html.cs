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
<h1>{{ Name }}{{ ComparedTo }}</h1>
{{#if noLongerSupported }}
    <p>No longer supported.</p>
{{else}}
    {{#if hasChanges }}
        {{> removedpublic }}
        {{> madeinternal }}
        {{> typedifference }}
    {{else}}
        <p>No differences found.<p/>
    {{/if}}
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
{{> fieldsObsoleted }}
{{> methodsChangedToNonPublic }}
{{> methodsRemoved }}
{{> methodsObsoleted }}
{{/typeDifferences }}
{{/if}}
";
        }

        public static string MethodsObsoleted()
        {
            return @"
{{ #if hasMethodsObsoleted ~}}
<h4>Methods Obsoleted</h4>
<ul>
  {{ #methodsObsoleted ~}}
  <li><code>{{ name }}</code><br/> {{ codify obsolete }}</li>
  {{~/methodsObsoleted }}
</ul>
{{~ /if }}
";
        }

        public static string FieldsObsoleted()
        {
            return @"
{{ #if hasFieldsObsoleted ~}}
<h4>Fields Obsoleted</h4>
<ul>
  {{ #fieldsObsoleted ~}}
  <li><code>{{ name }}</code><br/> {{ codify obsolete }}</li>
  {{~/fieldsObsoleted }}
</ul>
{{~ /if }}
";
        }
    }
}
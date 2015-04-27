<h1>{{Name}}</h1>
{{#if noLongerSupported}}
<p>No longer supported.</p>
{{else}}
{{> removedpublic}}
{{> madeinternal}}
{{> obsolete}}
{{/if}}
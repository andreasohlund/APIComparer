{{#if hasRemovedPublicTypes}}
<h2>The following public types have been removed</h2>
<ul>
  {{#removedPublicTypes}}
  <li><code>{{name}}</code></li>
  {{/removedPublicTypes}}
</ul>
{{/if}}
# APIComparer

Compares NuGetPackages/Assemblies and displays changes in the public api.

Example:

http://apicomparer-preview.particular.net/compare/rabbitmq.client/3.4.3...3.5.0

## Wildcards

If you don't specify the full semver version (x.y.z) the site will query nuget.org and redirect to the current highest versions. You can chose to only specify `major` or `major.minor`. Both left and right versions will be expanded.

Examples:

http://apicomparer.particular.net/compare/rabbitmq.client/3.4...3.5 - latest 3.4 against latest 3.5
http://apicomparer.particular.net/compare/rabbitmq.client/2...3 - latest 2 against latest 3
http://apicomparer.particular.net/compare/rabbitmq.client/3.4.3...3.5.0 - fully qualified version, will not be expanded

This feature is helpful if you want to include links in release notes how a give version compare the the latest X.Y of older versions.

### Environments

We currently have 2 environments. Please login to the particular azure account to manage them

#### Live
http://apicomparer.particular.net/

#### Preview (Test)
http://apicomparer-preview.particular.net/

### Continous deployment

The current setup auto deploys each successful build to the preview slot. 

### Buildserver

http://builds.particular.net/project.html?projectId=Tooling_APIComparer&tab=projectOverview

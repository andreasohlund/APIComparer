# APIComparer

Compares NuGetPackages/Assemblies and displays changes in the public api.

Example:

http://apicomparer-preview.particular.net/compare/rabbitmq.client/3.4.3...3.5.0

### Environments

We currently have 2 environments. Please login to the particular azure account to manage them

#### Live (not released yet)
http://apicomparer.particular.net/

#### Preview (Test)
http://apicomparer-preview.particular.net/

### Continous deployment

The current setup auto deploys each successful build to the preview slot. 

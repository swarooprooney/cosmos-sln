# User Secrets

The RepositoryOptions:CosmosConnectionString is set in the user secrets. To know more about user secrets read https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows

Use the following command to set the user secret

`dotnet user-secrets set "RepositoryOptions:CosmosConnectionString" "your connection string"`

# Integration tests

I have followed official documentation for intergration tests - https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-7.0.

There are few blogs which might interest you

1. https://goatreview.com/dotnet-testing-webapplicationfactory/
2. https://blog.markvincze.com/overriding-configuration-in-asp-net-core-integration-tests/
3. A good article about how webappilcationfactory is booted up - https://andrewlock.net/exploring-dotnet-6-part-6-supporting-integration-tests-with-webapplicationfactory-in-dotnet-6/

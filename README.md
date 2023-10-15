# User Secrets

The RepositoryOptions:CosmosConnectionString is set in the user secrets. To know more about user secrets read https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows

Use the following command to set the user secret

`dotnet user-secrets set "RepositoryOptions:CosmosConnectionString" "your connection string"`

# Integration tests

1. I have followed official documentation for intergration tests - https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-7.0.

2. I have also used test containers to standup a new instance of docker container on local. More about that can be read at https://testcontainers.com/modules/cosmosdb/

There are few blogs which might interest you

1. https://goatreview.com/dotnet-testing-webapplicationfactory/
2. https://blog.markvincze.com/overriding-configuration-in-asp-net-core-integration-tests/
3. A good article about how webappilcationfactory is booted up - https://andrewlock.net/exploring-dotnet-6-part-6-supporting-integration-tests-with-webapplicationfactory-in-dotnet-6/

# How to build this project as a docker image?

1. Replace the connection string in docker file - search for {your connection string goes here}. Replace this with a connection string from azure portal, the local ones don't work due to certificate issue
2. Command to Build a image - docker build -t swarooprooney/cosmos-int-sample:1.0 .
3. Command to run the docker image as container - docker run -p 5000:80 {imagename}
4. To see the settings of the container you just created - docker inspect {imageid}

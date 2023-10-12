# CodeBridge
This is my test assignment to CodeBridge.

# Short Description
Project architecture was written by using onion architecture. For exception handling was used global exception handling middleware and for validation - FluentValidation nuget. To make flexible database seeding I decided to use FluentAPI. For pagination and sorting I used specification pattern but changed it a bit for convenience. Here specification is not predefined but is created by every request based on sort and paging parameters. Last, but not least are unit tests. They are written on NUnit library and cover application layer.

I wrote some step-by-step guide how to setup project
# How to launch project
1. Create local project from repository
2. Change in appsettings.json connection string to yours
3. go to CodeBridge.Infrastructure folder and run: 
dotnet ef database update -s ..\CodeBridge.WebAPI\CodeBridge.WebAPI.csproj -c AppDbContext
This will update your database locally and create tables for Dog.
5. Run it and enjoy!)


# Creating/updating the Db schema
## Setup
1. Install dotnet-ef: `dotnet tool install --global dotnet-ef --version 5.0.5` make sure the version matches that of EF Core

## Adding migration

* For `BaseAppDbContext` context, from within the `Knowledge.Migrations` project 
	run: `dotnet ef migrations add <MIGRATION-NAME> --context BaseAppDbContext`
 eg: dotnet ef migrations add initdb --context BaseAppDbContext
	dotnet ef migrations add UpdateTableUser --context BaseAppDbContext
## Updating the Db with the migration
* For `BaseAppDbContext` context, from within the `Knowledge.Migrations` project 
	run: `dotnet ef database update --context BaseAppDbContext`


#Docker steps:
docker build -t my-api .
docker rm my-api-container
docker run -d -p 8080:80 --name my-api-container my-api


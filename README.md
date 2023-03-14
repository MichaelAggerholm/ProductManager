# Product Manager README

## Noter

installeret nugets:

dotnet add package Microsoft.EntityFrameworkCore.Sqlite

dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet add package Microsoft.AspNetCore.Mvc.DataAnnotations

commands:

lav migration:

dotnet ef migrations add ModelRevisions --context ProductContext

opdater databasen:

dotnet ef database update --context ProductContext

# TeamTodo

A simple Todo application. Created with: 

*   ASP.NET Core RC2
*   MVC Core RC2
*   Entity Framework Core RC2
*   AutoMapper
*   angularjs
*   jquery
*   bootstrap

## Instructions

Using the dotnet CLI:

*   navigate to the folder with project.json
*   run 'dotnet restore' to install nuget packages
*   run 'bower install' (optional since libraries are included in the repo)
*   run 'dotnet update database' to create the local sqlite database-file
*   run 'dotnet run'
*   navigate to localhost:5000


## Known issues

AutoMapper unable to map the foreign key-fields from the model to the viewmodel,
so relations are implemented with a workaround where relations work by storing the names of the related model
and doing lookups based on that field. Avoid changing the names of users/types that have related todo-items in the tool.


## Further improvements

*   Authentication / usermodels with Identiy Framework
*   Use factories for each data model to avoid code duplication in angular controllers
*   Drag / drop between categories
*   Cascade deletes

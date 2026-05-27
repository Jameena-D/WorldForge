# World Forge Create a World page
A page where you can create a world by filling in a name, description the worldtype and if you want it public or not.

**Stack**
* C#
* html 
* css
* JavaScript
* .NET
* EF Core

## Design pattern
**MVC Architecture while working towards using a REST API for communication between application, API and database.**
* Model with domeinobjects like `World` to describe the data and datatype that is being used in the application.
* View shows the UI.
* Controller processes the request and validates input.
* ViewModel between View and Controller.
* SeedService for the roles and adminaccount
* EF Core and `AppDbContext` for communication with database.

**Dataflow:**
MVC View -> Controller -> AppDbContext EF Core -> MySQL

# What do you need?
* .NET 10
* VS (or other IDE)
* WorldFroge repository
* MySQL

**Github repo link**
Currently the working version is on the develop branch.
https://github.com/Jameena-D/WorldForge.git

**MySQL**
* Make a datebase in MySQL
* In project RestApi -> appsettings.json change the database name to your database & password

**VS**
* Go to Package manager Console (NuGut)
* Make sure you have chosen RestApi as default project
* Run ```Update-Database```
* Configure start up projects -> RestApi & Worldforge need to start

**World Forge functions**
* Create an account
* Login with the account
* Create a world with name, description, type & chose if you want it public or not (+ save world)
* A world page where you can see the worlds you have saved
* Edit a world with extra input fields and different sections to have information seperated and organized
* A page with the public worlds, you can read them in detail
* Leave comments on a public world
* Report a public world if it has harmfull content

**Branch strategie**
* Main - The end version of the applicatie. When de application is done there will be a pull request with develop so the full working version of the application is on main.
* Develop - This is where all branches get merged into when the feature / fix etc are done. When a feature is finished it will immediately get merged with develop.
* feature/{thefeaturename} - A branch based on develop where a new feature will be developed. When a feature is completly done it will get merged with develop.
* bug/fix - A branch where small typos, weird symbols and bug fixes get resolved and then merged into develop.

# DP 6
# Threat Mitigation
STRIDE: Tampering

* T1 A user cannot send invalid or malicious input.
* T3 Insufficient validation can lead to incorrect data in the database.

Code: CreateWorldView, WorldsApiController
`[Required(ErrorMessage = "Name is required.")]
[StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]`
Server-side validation prevents manipulated or invalid data from being stored.
`if (!ModelState.IsValid)
{
    return ValidationProblem(ModelState);
}`
Validation rules restrict input and protect the integrity of the database



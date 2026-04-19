# World Forge Create a World page
A page where you can create a world by filling in a name, description the worldtype and if you want it public or not.

**Stack**
* C#
* html
* css
* JavaScript
* .NET

## Design pattern
**MVC Architecture while working towards using a REST API for communication between application, API and database.**
* Model with domeinobjects like `World` to describe the data and datatype that is being used in the application.
* View shows the UI.
* Controller processes the request and validates input.
* ViewModel between View and Controller.
* EF Core and `AppDbContext` for communication with database.

**Dataflow:**
MVC View -> Controller -> AppDbContext EF Core -> MySQL

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



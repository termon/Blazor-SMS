README 
======
The solution contains 

Core Library project that defines
1. entity models with associated relationships
2. data transfer objects

Data (EF) project that depends on Core and defines
1. a Repository Layer (EntityFramwork Context)
2. a Service Layer (application business logic)

Test (xUnit) project that depends on Data and Core that
1. Defines a set of unit tests to validate the operation of the service layer 

Rest project that
1. Defines a RESTful Api to access Student data
2. Uses Jwt tokens to secure access to the api
3. Is configured as a Blazor Host and can thus serve the Wasm project to the browser.
   To run the application the rest project should be started as follows within solution folder
   $ dotnet core run -p SMS.Rest

Wasm project that
1. Creates a Blazor Wasm project running in browser
2. Utilises the services provided by the RESTful WebApi
3. Uses custom client side authentication service 
   https://github.com/chrissainty/AuthenticationWithClientSideBlazor
   https://gist.github.com/SteveSandersonMS/60ca3a5f70a7f42fba14981add7e7f79
4. Uses fluentvalidation and a custom fluentvalidation component in the Wasm project
   https://github.com/ryanelian/FluentValidation.Blazor

# MicroElements.Swashbuckle.FluentValidation.WebApi
Use FluentValidation rules instead ComponentModel attributes to define swagger schema.

Note: For AspNetCore see: https://github.com/micro-elements/MicroElements.Swashbuckle.FluentValidation

## Latest Builds, Packages
[![NuGet](https://img.shields.io/nuget/v/MicroElements.Swashbuckle.FluentValidation.WebApi.svg)](https://www.nuget.org/packages/MicroElements.Swashbuckle.FluentValidation.WebApi/)
[![Travis](https://img.shields.io/travis/micro-elements/MicroElements.Swashbuckle.FluentValidation.WebApi/master.svg?label=travis%20build)](https://travis-ci.org/micro-elements/MicroElements.Swashbuckle.FluentValidation.WebApi)

## Usage

### 1. Reference packages in your Web Api project:

- Swashbuckle
- FluentValidation
- FluentValidation.WebApi
- MicroElements.Swashbuckle.FluentValidation.WebApi


### 2. Modify SwaggerConfig.cs

After you add Swashbuckle package you can find generated SwaggerConfig.cs

1. Comment [assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")] (we need to register swagger after FluentValidation in WebApiConfig)
2. Add registration c.AddFluentValidationRules();

```csharp
// Commented because we need manual registration in right order
//[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        ...
                        // Adds FluentValidationRules to swagger
                        c.AddFluentValidationRules();
                        ...
                    }
                    ...

```

### 3. Modify WebApiConfig.cs

1. Add `FluentValidationModelValidatorProvider.Configure(config);`
2. Add `SwaggerConfig.Register();`

```csharp
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Adds Fluent validation to WebApi
            FluentValidationModelValidatorProvider.Configure(config);

            // Registers swagger for WebApi 
            SwaggerConfig.Register();
        }
    }
```

## Sample application
See sample project: https://github.com/micro-elements/MicroElements.Swashbuckle.FluentValidation/tree/master/src/AspNetWebApiOld

## Credits

Initial version of this project was based on
[Mujahid Daud Khan](https://stackoverflow.com/users/1735196/mujahid-daud-khan) answer on StackOwerflow:
https://stackoverflow.com/questions/44638195/fluent-validation-with-swagger-in-asp-net-core/49477995#49477995

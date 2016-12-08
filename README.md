[![espalier-validate-webapi2 MyGet Build Status](https://www.myget.org/BuildSource/Badge/espalier-validate-webapi2?identifier=833ea536-b32d-4d46-91e2-317c284fc0bd)](https://www.myget.org/)

# Middleware for ASP.NET Core MVC using Espalier.Validate

Useful integrations for [Espalier.Validate](//github.com/jeremeevans/Espalier.Validate) in ASP.NET Core MVC projects.

## How to install Espalier.Validate.ASPNETCore

You can install Espalier.Validate.ASPNETCore from Nuget:

```
PM> Install-Package Espalier.Validate.ASPNETCore
```

There are currently versions targeting .NETFramework 4.5.2, 4.6, and 4.6.1. Need more? [Let me know](mailto:jereme@jeremeevans) and I'll create them if they make sense.

## Goals of this project

* Create a ASP.NET Core exception filter for Espalier.Validate ValidationException that sends a 400 error with a list of errors in JSON format.

## How to use this library

This library exposes an exception handler for .NET Web API 2 named **Espalier.Validate.ASPNETCore.EspalierExceptionFilter**

To use this exception handler, set your exception handler in the Web API config:

```csharp
public static void Register(HttpConfiguration config)
{
    config.EnableCors();
    config.MapHttpAttributeRoutes();
    config.Services.Replace(typeof(IExceptionHandler), new EspalierExceptionHandler());
}
```

Then when an exception of type **Espalier.Validate.EspalierValidationException** is thrown, the exception handler will take over and ouput an HTTP 400 response with the following JSON body:

```json
{
  "errors": [
    {
      "param": "Zip",
      "messages": [
        "Zip is not a valid postal code."
      ]
    }
  ]
}
```
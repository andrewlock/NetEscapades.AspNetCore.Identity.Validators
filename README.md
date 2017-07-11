# NetEscapades.AspNetCore.Identity.Validators

[![Build status](https://ci.appveyor.com/api/projects/status/mqlpvis18ll4rj6f/branch/master?svg=true)](https://ci.appveyor.com/project/andrewlock/NetEscapades.AspNetCore.Identity.Validators/branch/master)
<!--[![Travis](https://img.shields.io/travis/andrewlock/NetEscapades.AspNetCore.Identity.Validators.svg?maxAge=3600&label=travis)](https://travis-ci.org/andrewlock/NetEscapades.AspNetCore.Identity.Validators)-->
[![NuGet](https://img.shields.io/nuget/v/NetEscapades.AspNetCore.Identity.Validators.svg)](https://www.nuget.org/packages/NetEscapades.AspNetCore.Identity.Validators/)
[![MyGet CI](https://img.shields.io/myget/andrewlock-ci/v/NetEscapades.AspNetCore.Identity.Validators.svg)](http://myget.org/gallery/acndrewlock-ci)

A collection of [ASP.NET Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity) `IPasswordValidators` for use with [Microsoft.AspNetCore.Identity.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.AspNetCore.Identity.EntityFrameworkCore).

## Usage 

Includes the following validators:

* `EmailAsPasswordValidator` - Verify the user has not used their email as their password

* `UsernameAsPasswordValidator` - Verify the user has not used their email as their password

* `InvalidPhraseValidator` - Ensure the user hasn't used specific phrases, such as the url or domain of your website

You can add the validators to your project using the `AddPasswordValidator<TValidator>` method exposed by `IdentityBuilder`. Alternatively, use the extension methods on `IdentityBuilder` included in the package:

```csharp
services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddEmailAsPasswordValidator() // Add the email as password validator
    .AddUsernameAsPasswordValidator() // Add the username as password validator
    .AddInvalidPhraseValidator<ApplicationUser>(new []{"MyDomainName.com"}); // Add the invalid phrase validator
```

>*NOTE* This package is currently for ASP.NET Core Identity 2.0-preview-2, so requires .NET Core 2.0-preview2 is installed.

## Installing 

Install using the [NetEscapades.AspNetCore.Identity.Validators NuGet package](https://www.nuget.org/packages/NetEscapades.AspNetCore.Identity.Validators):

```
PM> Install-Package NetEscapades.AspNetCore.Identity.Validators
```

or 

```
dotnet add package NetEscapades.AspNetCore.Identity.Validators
```

## Additional Resources
* [Introduction to ASP.NET Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity)

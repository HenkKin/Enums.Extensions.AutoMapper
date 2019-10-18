Enums.Extensions.AutoMapper
===========
[![Build Status](https://ci.appveyor.com/api/projects/status/github/HenkKin/Enums.Extensions.AutoMapper?branch=master&svg=true)](https://ci.appveyor.com/project/HenkKin/Enums-Extensions-AutoMapper) 
[![NuGet](https://img.shields.io/nuget/dt/Enums.Extensions.AutoMapper.svg)](https://www.nuget.org/packages/Enums.Extensions.AutoMapper) 
[![NuGet](https://img.shields.io/nuget/vpre/Enums.Extensions.AutoMapper.svg)](https://www.nuget.org/packages/Enums.Extensions.AutoMapper)
[![BCH compliance](https://bettercodehub.com/edge/badge/HenkKin/Enums.Extensions.AutoMapper?branch=master)](https://bettercodehub.com/)

### Summary

The Enums.Extensions.AutoMapper library solves the problem of mapping enum values. Normally enums are automatically mapped bij AutoMapper, but you have no control about custom mapping. It is possible to create a custom type converter for every enum.

This library supports mapping enums values like properties.

This library is Cross-platform, supporting `netstandard2.1`.

### Dependencies

- [AutoMapper](https://www.nuget.org/packages/AutoMapper/)

### Installing Enums.Extensions.AutoMapper

You should install [Enums.Extensions.AutoMapper with NuGet](https://www.nuget.org/packages/Enums.Extensions.AutoMapper):

    Install-Package Enums.Extensions.AutoMapper

Or via the .NET Core command line interface:

    dotnet add package Enums.Extensions.AutoMapper

Either commands, from Package Manager Console or .NET Core CLI, will download and install Enums.Extensions.AutoMapper. Enums.Extensions.AutoMapper has no dependencies. 

### Usage
Install via NuGet first:
`Install-Package Enums.Extensions.AutoMapper`

To use it:

For method `CreateMap` this library provide a `AsEnumMap` method. This methods add all default mappings from source to destination enum values.

If you want to change some mappings, then you can use `MapFromEnumValue` method. This is a chainable method.

```csharp
using Enums.Extensions.AutoMapper;

public enum Source
{
    Default = 0,
    First = 1,
    Second = 2
}

public enum Destination
{
    Default = 0,
    Second = 2
}

internal class YourProfile : Profile
{
    public YourProfile()
    {
         CreateMap<Source, Destination>()
            .AsEnumMap(EnumMappingType.Value) // OR EnumMappingType.Name
            .MapFromEnumValue(Source.First, Destination.Default); 
    }
}
    ...
```

### Testing

[AutoMapper](https://www.nuget.org/packages/AutoMapper/) provides a nice tooling for validating typemaps. This library adds an extra `IConfigurationProvider.AssertEnumConfigurationIsValid` extension method.

```csharp

public class MappingConfigurationsTests
{
    [Fact]
    public void WhenProfilesAreConfigured_ItShouldNotThrowException()
    {
        // Arrange
        var config = new MapperConfiguration(configuration =>
        {
            configuration.AddMaps(typeof(AssemblyInfo).GetTypeInfo().Assembly);
        });

        // default automapper assertions
        config.AssertConfigurationIsValid();

        // Assert
        config.AssertEnumConfigurationIsValid(); // this line asserts all enum value mapping configs
    }
}
```

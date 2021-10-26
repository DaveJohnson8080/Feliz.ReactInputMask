<!-- [![Build status](https://ci.appveyor.com/api/projects/status/wryhq70j3cfxca8v?svg=true)](https://ci.appveyor.com/project/DaveJohnson8080/feliz-msal) [![NuGet](https://img.shields.io/nuget/v/Feliz.MSAL.svg?style=flat-square)](https://www.nuget.org/packages/Feliz.MSAL/) -->

# Feliz.ReactInputMask

Feliz/Fable bindings for [react-input-mask](https://github.com/sanniassin/react-input-mask)

## Installation

### npm

```npm install react-input-mask```

```dotnet paket add Feliz.ReactInputMask --project <path to your proj>```

### femto

(From the target project folder)
```dotnet femto install Feliz.ReactInputMask```

## Contributing

This project uses `fake`, `paket`, and `femto` as .NET Core local tools. Therefore, run `dotnet tool restore` to restore the necessary CLI tools before doing anything else.

To run targets using Fake: `dotnet fake build -t TargetName`

### Regular maintenance

1. Run the `CiBuild` target to check that everything compiles
2. Commit and tag the commit (this is what triggers deployment from  AppVeyor). For consistency, the tag should be identical to the version (e.g. `1.2.3`).
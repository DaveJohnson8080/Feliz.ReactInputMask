[![Build status](https://ci.appveyor.com/api/projects/status/j5jtme743k2lhar7?svg=true)](https://ci.appveyor.com/project/DaveJohnson8080/feliz-reactinputmask) [![NuGet](https://img.shields.io/nuget/v/Feliz.ReactInputMask.svg?style=flat-square)](https://www.nuget.org/packages/Feliz.ReactInputMask/)

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
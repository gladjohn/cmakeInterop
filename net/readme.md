# Overview

The aim of this project is to provide a way to create a nuget package that contains: 

- the native msalruntime binaries for various cpu architectures (windows x86 and x64 are currently supported)
- the interop C# -> C layer 

# Buid

## Dev Build 

1. Build msalcpp for both x64 (default) and for x86 (via `py build.py --arch x86`)
2. Build this project

You can now use MsalRuntime.DebugApp console app to debug the msalruntime. This just copies the msalruntime.dll (x64).

## CI Build

TBD

## Package

1. Update the version in Microsoft.Identity.Client.NativeInterop.csproj
2. Package the `Microsoft.Identity.Client.NativeInterop` project from Visual Studio or via `dotnet pack` command line.
3. Observe the package structure in ~\microsoft-authentication-library-for-cpp\_install\msalruntime_dotnet\Debug\

### Test the package

1. Update the reference to the packge in the MsalRuntime.PackageTestApp.NetFx.csproj and MsalRuntime.PackageTestApp.NetCore.csproj
2. You may have to manually perform the package restore via command line `dotnet restore`

You can automate this by executing the `build-and-pack.ps1` script in the test foolder.



#### Test the pinvoke layer

You can use `MsalRuntime.PackageTestApp.NetCore` and `MsalRuntime.PackageTestApp.NetFx` to test that the pinvoke works. 

Test matrix: 

Fwk: NetCore, NetFx, UWP
CPU Architecture: `x64`, `x86` and `AnyCpu`

#### Test the .NET publishing layer

Using the command `dotnet publish` and `dotnet publish --arch x64`, test the following: 

Fwk: NetCore, NetFx, UWP
Publish with: `dotnet publish`, `dotnet publish --arch x64`, `dotnet publish --arch x86` 

# CI

TBD

## Next steps

	1. Read version file instead of hardcoding it
	2. Figure out how to package native PDBs (we may have to use nuspec for this?)
	3. Add package metadata like copyright etc.
	4. Add unit tests for pinvoke layer
	5. Integrate with msalruntime CI and release builds
	
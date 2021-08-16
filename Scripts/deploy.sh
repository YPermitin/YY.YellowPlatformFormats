#!/bin/sh
API_KEY = $1

dotnet nuget push ./Libs/YY.YellowPlatformFormats.Core/bin/Release/YY.YellowPlatformFormats.Core.*.nupkg -k $1 -s https://api.nuget.org/v3/index.json --skip-duplicate
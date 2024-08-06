$scriptPath = $MyInvocation.MyCommand.Path
$projectPath = Join-Path (Split-Path $scriptPath) "build\build.fsproj"
dotnet run --project $projectPath $args
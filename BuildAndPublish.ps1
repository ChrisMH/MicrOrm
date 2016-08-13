#
# Flags
#
# -Build 
#   Build the projects
#
# -Pack 
#   Package the projects 
#
# -Publish
#   Publish the packages to the repository
#
# No flags specified is equivalent to calling with all flags (-Build -Pack -Publish)
#
if($args.Length -eq 0)
{
    $args += "-Build"
    $args += "-Pack"
    $args += "-Publish"
}
$ErrorActionPreference = "Stop"

$solution = "MicrOrm.sln"         #relative to $srcRoot

[string[]] $projects =          #relative to $srcRoot
  "MicrOrm40\MicrOrm40.csproj",
  "MicrOrm45\MicrOrm45.csproj",
  "MicrOrm451\MicrOrm451.csproj",
  "MicrOrm452\MicrOrm452.csproj",
  "MicrOrm46\MicrOrm46.csproj",
  "MicrOrm461\MicrOrm461.csproj"

[string[]] $packages = 
  "MicrOrm"

$srcRoot = ".\src"                      # relative to script directory
$outputPath = ".\out"                   # relative to script directory
$nuspecPath = ".\nuspec"                # relative to script directory
$versionFile = 'SharedAssemblyInfo.cs'  # relative to $srcRoot

$repository = "http://nuget.hogancode.com:81/Hogan/nuget"
$apiKey = "Chris051010"


$buildCmd = "$env:windir\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"
$nugetCmd = "${Env:ProgramFiles(x86)}\nuget\nuget.exe"


# Create the output path if it does not exist
if((Test-Path $outputPath) -eq $False) { New-Item -Path $outputPath -ItemType directory | Out-Null }

# Get version information from AssemblyInformationalVersion key in the assembly info file
$matchPattern = "^\[\s*assembly:\s*AssemblyInformationalVersion\s*\(\s*\""(?<version>.*)\""\s*\)\s*\]"
Get-Content (Join-Path $srcRoot $versionFile -Resolve) | Where-Object { $_ -match $matchPattern } | out-null
$version = $matches.version

# Build all projects
if($args -contains "-Build")
{
    $solutionFile = Join-Path $srcRoot $solution
    if((Test-Path $solutionFile) -eq $False) { throw "Could not find solution file for '$solution'" }
    
    #restore nuget packages before building
    Write-Host "`nRestoring packages for $solutionFile...`n" -ForegroundColor Green
    &$nugetCmd restore $solutionFile

    Write-Host "`nBuilding all projects...`n" -ForegroundColor Green

    foreach($project in $projects)
    {
        $projectFile = Join-Path $srcRoot $project
        if((Test-Path $projectFile) -eq $False) { throw "Could not find project file for '$project'" }

        Write-Host "`nBuilding $project from $projectFile`n" -ForegroundColor Green

        &$buildCmd $projectFile /p:Configuration=Release /t:clean 
        if($LastExitCode) {throw "Build Failed"}
        &$buildCmd $projectFile /p:Configuration=Release /t:rebuild 
        if($LastExitCode) {throw "Build Failed"}
    }
}


# Package all projects
if($args -contains "-Pack")
{   
    Write-Host "`nPackaging to $outputPath...`n" -ForegroundColor Green

    foreach($package in $packages)
    {
        $nuspecFile = Join-Path $nuspecPath "$package.nuspec"
        if((Test-Path $nuspecFile) -eq $False) { throw "Could not find nuspec file for '$package'" }
        
        Write-Host "`nPackaging $nuspecFile`n" -ForegroundColor Green
        
        &$nugetCmd pack $nuspecFile -Version $version -OutputDirectory $outputPath
    }
}


# Publish to repository
if($args -contains "-Publish")
{
    Write-Host "`nPublishing to $repository...`n" -ForegroundColor Green
    
    &$nugetCmd setApiKey $apiKey -Source $repository

    foreach($package in $packages)
    {
        $packageFile = Join-Path $outputPath "$package.$version.nupkg"
        if((Test-Path $packageFile) -eq $False) { throw "Could not find package file for '$package'" }

        Write-Host "`nPublishing $packageFile...`n" -ForegroundColor Green

        &$nugetCmd push $packageFile -Source $repository
    }
}



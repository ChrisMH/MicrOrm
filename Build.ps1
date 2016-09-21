#
# Flags
#
# -Build 
#   Build the projects
#
# -Clean 
#   Cleans output files
#
# -Pack 
#   Package the projects 
#
# -Push
#   Push the packages to the repository
#
# No flags specified is equivalent to calling with all flags (-Build -Pack -Publish)
#
if($args.Length -eq 0)
{
    $args += "-Build"
    $args += "-Pack"
    $args += "-Push"
}
$ErrorActionPreference = "Stop"

$solution = "MicrOrm.sln"         #relative to $srcRoot

[string[]] $projects =          #relative to $srcRoot
  "MicrOrm45\MicrOrm45.csproj",
  "MicrOrm451\MicrOrm451.csproj",
  "MicrOrm452\MicrOrm452.csproj",
  "MicrOrm46\MicrOrm46.csproj",
  "MicrOrm461\MicrOrm461.csproj"
  
[string[]] $testProjects =          #relative to $srcRoot
  "MicrOrm45.Test\MicrOrm45.Test.csproj"

[string[]] $packages = 
  "MicrOrm"

$srcRoot = "."                          # relative to script directory
$outputPath = ".\out"                   # relative to script directory
$packagesPath = ".\packages"            # relative to script directory
$nuspecPath = ".\nuspec"                # relative to script directory
$versionFile = 'SharedAssemblyInfo.cs'  # relative to $srcRoot

$repository = "https://nuget.org/api/v2/package"
$apiKey = "f1d4a9f9-fceb-43ca-a972-538a4f7accdd"


$buildCmd = "$env:windir\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"
$nugetCmd = "${Env:ProgramFiles(x86)}\nuget\nuget.exe"

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
        if($LastExitCode) {throw "Clean Failed"}
        &$buildCmd $projectFile /p:Configuration=Release /t:rebuild 
        if($LastExitCode) {throw "Build Failed"}
    }
}

# Clean all output files
if($args -contains "-Clean")
{    
    Write-Host "`Removing output directories...`n" -ForegroundColor Green

    foreach($project in $projects + $testProjects)
    {
        $projectFile = Join-Path $srcRoot $project
        if((Test-Path $projectFile) -eq $False) { throw "Could not find project file for '$project'" }

		$projectDir = Split-Path -Path $projectFile -Parent

		$binDir = Join-Path $projectDir "bin"
		if((Test-Path $binDir) -eq $True) { Remove-Item -Recurse -Force $binDir }
		
		$objDir = Join-Path $projectDir "obj"
		if((Test-Path $objDir) -eq $True) { Remove-Item -Recurse -Force $objDir }
    }
	

    if((Test-Path $outputPath) -eq $True) { Remove-Item -Recurse -Force $outputPath }
    if((Test-Path $packagesPath) -eq $True) { Remove-Item -Recurse -Force $packagesPath }
}


# Pack all projects
if($args -contains "-Pack")
{   
    # Create the output path if it does not exist
    if((Test-Path $outputPath) -eq $False) { New-Item -Path $outputPath -ItemType directory | Out-Null }

    Write-Host "`nPacking to $outputPath...`n" -ForegroundColor Green

    foreach($package in $packages)
    {
        $nuspecFile = Join-Path $nuspecPath "$package.nuspec"
        if((Test-Path $nuspecFile) -eq $False) { throw "Could not find nuspec file for '$package'" }
        
        Write-Host "`nPacking $nuspecFile`n" -ForegroundColor Green
        
        &$nugetCmd pack $nuspecFile -Version $version -OutputDirectory $outputPath
    }
}


# Push to repository
if($args -contains "-Push")
{
    Write-Host "`nPushing to $repository...`n" -ForegroundColor Green
    
    foreach($package in $packages)
    {
        $packageFile = Join-Path $outputPath "$package.$version.nupkg"
        if((Test-Path $packageFile) -eq $False) { throw "Could not find package file for '$package'" }

        Write-Host "`nPushing $packageFile...`n" -ForegroundColor Green

        &$nugetCmd push $packageFile $apiKey -Source $repository
    }
}



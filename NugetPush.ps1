$srcRoot = '.\src'                     # relative to script directory
$versionFile = 'SharedAssemblyInfo.cs' # relative to $srcRoot
$outputPath = "$home\Dropbox\Packages" 

Import-Module NugetUtilities

$version = Get-Version (Join-Path $srcRoot $versionFile -Resolve)

Push-Project MicrOrm $srcRoot $version $outputPath

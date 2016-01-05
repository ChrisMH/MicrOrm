[string[]] $buildFiles = 
  '.\src\MicrOrm\MicrOrm.csproj'
[string[]] $nuspecFiles = 
  '.\nuspec\MicrOrm.nuspec'
  
$versionFile = '.\src\SharedAssemblyInfo.cs'

$buildConfiguration = 'Release'
$outputPath = "$home\Dropbox\Packages"

Import-Module BuildUtilities

$version = Get-Version (Resolve-Path $versionFile)
  
New-Path $outputPath

#foreach($buildFile in $buildFiles)
#{
#  Invoke-Build (Resolve-Path $buildFile) $buildConfiguration
#}

foreach($nuspecFile in $nuspecFiles)
{
  New-Package (Resolve-Path $nuspecFile) $version $outputPath
}

Remove-Module BuildUtilities


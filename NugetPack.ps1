[string[]] $buildFiles = 
  '.\src\MicrOrm\MicrOrm40.csproj',
  '.\src\MicrOrm\MicrOrm45.csproj',
  '.\src\MicrOrm\MicrOrm451.csproj',
  '.\src\MicrOrm\MicrOrm452.csproj'
[string[]] $nuspecFiles = 
  '.\nuspec\MicrOrm.nuspec'
  
$versionFile = '.\src\SharedAssemblyInfo.cs'

$buildConfiguration = 'Release'
$outputPath = Join-Path $HOME "Dropbox\Packages"

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


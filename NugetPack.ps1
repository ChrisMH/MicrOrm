[string[]] $buildFiles = 
  '.\src\MicrOrm40\MicrOrm40.csproj',
  '.\src\MicrOrm45\MicrOrm45.csproj',
  '.\src\MicrOrm451\MicrOrm451.csproj',
  '.\src\MicrOrm452\MicrOrm452.csproj'
[string[]] $nuspecFiles = 
  '.\nuspec\MicrOrm.nuspec'
  
$versionFile = '.\src\SharedAssemblyInfo.cs'

$buildConfiguration = 'Release'
$outputPath = Join-Path $HOME "Dropbox\Packages"

Import-Module BuildUtilities

$version = Get-Version (Resolve-Path $versionFile)
  
New-Path $outputPath

foreach($buildFile in $buildFiles)
{
  Invoke-Build (Resolve-Path $buildFile) 'Debug'
  Invoke-Build (Resolve-Path $buildFile) 'Release'
}

New-Package (Resolve-Path '.\nuspec\MicrOrm.nuspec') $version $outputPath
New-Package (Resolve-Path '.\nuspec\MicrOrm.Debug.nuspec') "$version-debug" $outputPath

Remove-Module BuildUtilities


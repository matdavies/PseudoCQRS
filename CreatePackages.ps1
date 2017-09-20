$Version = Read-Host -Prompt 'PseudoCQRS version to pack'
$PackMvc = Read-Host -Prompt 'Pack MVC y/n'
if($PackMvc -eq 'y'){
    $MvcCoreVersion = Read-Host -Prompt 'PseudoCQRS MvcCore version to pack'
}

$pseudoCQRSCsprojPath = 'PseudoCQRS\PseudoCQRS.csproj'
$pseudoCQRSNuspecPath = 'PseudoCQRS.nuspec'

$pseudoCQRSCsproj = [xml](Get-Content $pseudoCQRSCsprojPath)
$pseudoCQRSCsproj.Project.PropertyGroup.VersionPrefix = $Version.ToString()
$pseudoCQRSCsproj.Save($pseudoCQRSCsprojPath)

$pseudoCQRSNuspec = [xml](Get-Content $pseudoCQRSNuspecPath)
$pseudoCQRSNuspec.package.metadata.version = $Version.ToString()
$pseudoCQRSNuspec.Save($pseudoCQRSNuspecPath)

dotnet build -c Release $pseudoCQRSCsprojPath
dotnet build -c Release PseudoCQRS.Analyzers\PseudoCQRS.Analyzers.csproj
nuget pack $pseudoCQRSNuspecPath

if($PackMvc -eq 'y'){
    $pseudoCQRSCMvcCsprojPath = 'PseudoCQRS.Mvc\PseudoCQRS.Mvc.csproj'

    $pseudoCQRSCMvcCsproj = [xml](Get-Content $pseudoCQRSCMvcCsprojPath)
    ($pseudoCQRSCMvcCsproj.Project.PropertyGroup | where {$_.PackageId -eq 'PseudoCQRS.Mvc5'} | select -First 1).VersionPrefix = $Version.ToString()
    ($pseudoCQRSCMvcCsproj.Project.PropertyGroup | where {$_.PackageId -eq 'PseudoCQRS.Core.Mvc'} | select -First 1).VersionPrefix = $MvcCoreVersion.ToString()

    $pseudoCQRSCMvcCsproj.Save($pseudoCQRSCMvcCsprojPath)

    echo $pseudoCQRSCMvcCsproj

    dotnet pack -c MvcCore $pseudoCQRSCMvcCsprojPath
    dotnet pack -c MVC5 $pseudoCQRSCMvcCsprojPath

    Move-Item -Path .\PseudoCQRS.Mvc\bin\MvcCore\PseudoCQRS.Core.Mvc.$MvcCoreVersion.nupkg -Destination .
    Move-Item -Path .\PseudoCQRS.Mvc\bin\MVC5\PseudoCQRS.Mvc5.$Version.nupkg -Destination .
}
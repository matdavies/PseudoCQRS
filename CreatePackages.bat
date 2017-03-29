@echo off
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "PseudoCQRS.Mvc\PseudoCQRS.Mvc.csproj" /v:normal /t:Rebuild /p:Configuration=Mvc3
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "PseudoCQRS.Mvc\PseudoCQRS.Mvc.csproj" /v:normal /t:Rebuild /p:Configuration=Mvc4
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "PseudoCQRS.Mvc\PseudoCQRS.Mvc.csproj" /v:normal /t:Rebuild /p:Configuration=Mvc5

if ERRORLEVEL 1 goto eof

nuget pack PseudoCQRS.nuspec
nuget pack PseudoCQRS.Mvc3.nuspec
nuget pack PseudoCQRS.Mvc4.nuspec
nuget pack PseudoCQRS.Mvc5.nuspec

:eof
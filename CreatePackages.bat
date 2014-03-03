@echo off
"%systemroot%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe" "PseudoCQRS\PseudoCQRS.csproj" /v:normal /t:Rebuild /p:Configuration=Mvc3
"%systemroot%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe" "PseudoCQRS\PseudoCQRS.csproj" /v:normal /t:Rebuild /p:Configuration=Mvc4
"%systemroot%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe" "PseudoCQRS\PseudoCQRS.csproj" /v:normal /t:Rebuild /p:Configuration=Mvc51

if ERRORLEVEL 1 goto eof

nuget pack PseudoCQRS.nuspec
nuget pack PseudoCQRS.Mvc4.nuspec
nuget pack PseudoCQRS.Mvc5.1.nuspec

:eof
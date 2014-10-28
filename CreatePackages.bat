@echo off
"%systemroot%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe" "PseudoCQRS.Mvc\PseudoCQRS.Mvc.csproj" /v:normal /t:Rebuild /p:Configuration=Mvc3
"%systemroot%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe" "PseudoCQRS.Mvc\PseudoCQRS.Mvc.csproj" /v:normal /t:Rebuild /p:Configuration=Mvc4
"%systemroot%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe" "PseudoCQRS.Mvc\PseudoCQRS.Mvc.csproj" /v:normal /t:Rebuild /p:Configuration=Mvc5

if ERRORLEVEL 1 goto eof

nuget pack PseudoCQRS.nuspec
nuget pack PseudoCQRS.Mvc3.nuspec
nuget pack PseudoCQRS.Mvc4.nuspec
nuget pack PseudoCQRS.Mvc5.nuspec

:eof
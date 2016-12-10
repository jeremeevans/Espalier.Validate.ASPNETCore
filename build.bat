@echo Off

dotnet restore .\Espalier.Validate.ASPNETCore\project.json

dotnet build .\Espalier.Validate.ASPNETCore\project.json --configuration Release
if not "%errorlevel%"=="0" goto failure

dotnet pack .\Espalier.Validate.ASPNETCore\project.json --configuration release

:success
exit 0

:failure
exit -1
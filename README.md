# Boyner.Product

1. Docker image run
"docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=yourStrong(!)Password" -e "MSSQL_PID=Express" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest "

2. Database Migration Process
run command on cli "dotnet ef Add-Migration"
run command on cli "dotnet ef Update-Database"

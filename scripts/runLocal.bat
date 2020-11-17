
dotnet publish -c release -o ./app
dotnet ./app/WebGoatCore.dll --urls=http://localhost:5000

# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR ./sources

# copy everything else and build app
COPY WebGoat.NET/. ./sources/WebGoat.NET/
WORKDIR ./sources/WebGoat.NET
RUN dotnet publish -c release -o /app 

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "WebGoat.NET.dll"] 
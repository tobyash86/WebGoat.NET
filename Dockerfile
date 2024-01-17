# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR ./sources

# copy everything else and build app
COPY WebGoat.NET/. ./sources/WebGoat.NET/
WORKDIR ./sources/WebGoat.NET
RUN dotnet publish -c release -o /app 

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

LABEL org.opencontainers.image.source=https://github.com/tobyash86/WebGoat.NET
LABEL org.opencontainers.image.description="WebGoat.NET - port of original WebGoat.NET (.NET Framework) to .NET"

ENTRYPOINT ["dotnet", "WebGoat.NET.dll"] 

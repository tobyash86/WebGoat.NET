# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR ./sources

# copy everything else and build app
COPY WebGoatCore/. ./sources/WebGoatCore/
WORKDIR ./sources/WebGoatCore
RUN dotnet publish -c release -o /app 

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "WebGoatCore.dll"] 
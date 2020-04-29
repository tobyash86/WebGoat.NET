# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR .
run MKDIR sources
WORKDIR ./sources

# copy csproj and restore as distinct layers
COPY *.sln .
COPY WebGoatCore/*.csproj ./WebGoatCore/
RUN dotnet restore

# copy everything else and build app
COPY WebGoatCore/. ./sources/WebGoatCore/
WORKDIR ./sources/WebGoatCore
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "WebGoatCore.dll"]
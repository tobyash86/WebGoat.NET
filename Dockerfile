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
RUN apt-get update -y && apt-get upgrade -y && apt-get -y install wget && apt-get -y install unzip
RUN wget https://download.immun.io/internal/dotnet/TrendAppProtect-x64-Linux-gnu-4.1.1.zip
RUN unzip TrendAppProtect-x64-Linux-gnu-4.1.1.zip
RUN rm TrendAppProtect-x64-Linux-gnu-4.1.1.zip

COPY --from=build /app ./
ENTRYPOINT ["dotnet", "WebGoatCore.dll"] 
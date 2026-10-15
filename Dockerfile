# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:9.0@sha256:35048e3a81e6a07c316e7bbbd80d80d2ba705fe5f23a8ed42b6638c8f4c20d30 AS build
WORKDIR ./sources

# copy everything else and build app
COPY WebGoat.NET/. ./sources/WebGoat.NET/
WORKDIR ./sources/WebGoat.NET
RUN dotnet publish -c release -o /app 

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:9.0@sha256:4e376dd15bbc8437d4892367ab0ea06a3ac9fea482d10f92f3c493fe1a2219ad
WORKDIR /app
COPY --from=build /app ./

LABEL org.opencontainers.image.source=https://github.com/tobyash86/WebGoat.NET
LABEL org.opencontainers.image.description="WebGoat.NET - port of original WebGoat.NET (.NET Framework) to .NET"

ENTRYPOINT ["dotnet", "WebGoat.NET.dll"] 

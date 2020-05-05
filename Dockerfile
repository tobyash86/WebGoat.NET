FROM mcr.microsoft.com/windows:1909 AS os

WORKDIR .

COPY ./SqlLocalDB.msi .
RUN SqlLocalDB.msi /qn IACCEPTSQLLOCALDBLICENSETERMS=YES


FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

# copy csproj and restore as distinct layers
COPY *.sln .
COPY WebGoatCore/*.csproj ./WebGoatCore/
RUN dotnet restore

# copy everything else and build app
COPY WebGoatCore/. ./WebGoatCore/
WORKDIR ./WebGoatCore
RUN dotnet publish -c release -o /app --no-restore

WORKDIR /app

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
FROM os as final

COPY --from=runtime ["/Program Files/dotnet/", "./Program Files/dotnet/"]
COPY --from=build /app ./app

WORKDIR C:/app
RUN SETX ASPNETCORE_URLS "http://0.0.0.0:80/" /m
ENTRYPOINT ["C:/Program Files/dotnet/dotnet", "webgoatcore.dll"]
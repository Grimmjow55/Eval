# Utiliser une image base .NET (adaptée à votre application)
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

# Construire l'application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ClientConsoleAnimalApi/ClientConsoleAnimalApi.csproj", "ClientConsoleAnimalApi/"]
RUN dotnet restore "ClientConsoleAnimalApi/ClientConsoleAnimalApi.csproj"
COPY . .
WORKDIR "/src/ClientConsoleAnimalApi"
RUN dotnet build "ClientConsoleAnimalApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClientConsoleAnimalApi.csproj" -c Release -o /app/publish

# Utiliser l'image base pour exécuter l'application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClientConsoleAnimalApi.dll"]

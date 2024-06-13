# Utiliser une image base .NET (adaptée à votre application)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Construire l'application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AnimalApi/AnimalApi.csproj", "AnimalApi/"]
RUN dotnet restore "AnimalApi/AnimalApi.csproj"
COPY . .
WORKDIR "/src/AnimalApi"
RUN dotnet build "AnimalApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AnimalApi.csproj" -c Release -o /app/publish

# Utiliser l'image base pour exécuter l'application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnimalApi.dll"]

# Utiliser une image .NET SDK pour la construction
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier les fichiers csproj et restaurer les dépendances
COPY AnimalApi/AnimalApi.csproj AnimalApi/
COPY ClientConsoleAnimalApi/ClientConsoleAnimalApi.csproj ClientConsoleAnimalApi/
RUN dotnet restore AnimalApi/AnimalApi.csproj
RUN dotnet restore ClientConsoleAnimalApi/ClientConsoleAnimalApi.csproj

# Copier le reste du projet et compiler
COPY AnimalApi/ AnimalApi/
COPY ClientConsoleAnimalApi/ ClientConsoleAnimalApi/
WORKDIR /src/AnimalApi
RUN dotnet publish AnimalApi.csproj -c Release -o /app/out

WORKDIR /src/ClientConsoleAnimalApi
RUN dotnet publish ClientConsoleAnimalApi.csproj -c Release -o /app/out

# Utiliser une image runtime pour l'exécution
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copier les sorties des deux projets
COPY --from=build /app/out .

# Exposer le port pour l'API
EXPOSE 80

# Définir le point d'entrée pour l'API
ENTRYPOINT ["dotnet", "AnimalApi.dll"]

# Stage 1: Build dell'applicazione
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia i file del progetto e ripristina le dipendenze
COPY *.csproj ./
RUN dotnet restore

# Copia il resto dei sorgenti e fai la build
COPY . ./
RUN dotnet publish -c Release -o /app/out

# Stage 2: Runtime leggero
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Crea la cartella per il DB SQLite
RUN mkdir -p /app/data

COPY --from=build /app/out .

# Configura Render per usare la porta corretta
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Sostituisci "NomeTuoProgetto.dll" con il nome reale del file generato
ENTRYPOINT ["dotnet", "crotto-plinius.dll"]
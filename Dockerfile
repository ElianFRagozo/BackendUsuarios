FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copiar el archivo JSON al contenedor
COPY ["fb-notas-firebase-adminsdk-zbhby-826ba65d54.json", "/app/"]

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BackendUsuario.csproj", "."]
RUN dotnet restore "./BackendUsuario.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./BackendUsuario.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BackendUsuario.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackendUsuario.dll"]

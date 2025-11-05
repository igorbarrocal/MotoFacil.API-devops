# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo de solução e o csproj
COPY MotoFacil.API-devops.sln ./
COPY MotoFacil.API-devops/*.csproj ./MotoFacil.API-devops/

# Restaura as dependências
RUN dotnet restore

# Copia todos os arquivos do projeto
COPY MotoFacil.API-devops/ ./MotoFacil.API-devops/

WORKDIR /src/MotoFacil.API-devops

# Publica o projeto
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MotoFacil.API-devops.dll"]
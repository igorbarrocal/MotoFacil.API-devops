# ================================
# Etapa 1: Build com .NET SDK 8.0
# ================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo de solução e o projeto .csproj principal
COPY MotoFacil.API-devops.sln ./
COPY MotoFacil.API-devops.csproj ./

# Restaura as dependências
RUN dotnet restore MotoFacil.API-devops.csproj

# Copia todos os arquivos restantes do projeto
COPY . .

# Compila e publica a aplicação em modo Release
RUN dotnet publish MotoFacil.API-devops.csproj -c Release -o /app/publish --no-restore

# ================================
# Etapa 2: Runtime (imagem leve)
# ================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80

COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "MotoFacil.API-devops.dll"]
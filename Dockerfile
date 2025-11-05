# Etapa 1: Build com SDK do .NET 8
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo de solução e os projetos (ajuste os nomes conforme seu projeto)
COPY MotoFacil.API.sln ./
COPY MotoFacil.API/MotoFacil.API.csproj MotoFacil.API/
COPY MotoFacil.Domain/MotoFacil.Domain.csproj MotoFacil.Domain/
COPY MotoFacil.Infra/MotoFacil.Infra.csproj MotoFacil.Infra/
COPY MotoFacil.Application/MotoFacil.Application.csproj MotoFacil.Application/

# Restaura dependências
RUN dotnet restore MotoFacil.API/MotoFacil.API.csproj

# Copia todo o restante do código
COPY . .

# Publica a aplicação
WORKDIR /src/MotoFacil.API
RUN dotnet publish -c Release -o /app/publish --no-restore

# Etapa 2: Runtime (imagem menor)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80

# Copia os artefatos publicados
COPY --from=build /app/publish .

# Expõe a porta da aplicação
EXPOSE 80

# Define o ponto de entrada
ENTRYPOINT ["dotnet", "MotoFacil.API.dll"]

# ================================
# Etapa 1: Build com .NET SDK 8.0
# ================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo de solução
COPY MotoFacil.API-devops.sln ./

# Copia cada projeto (.csproj) individualmente — isso ajuda no cache do Docker
COPY MotoFacil.API/MotoFacil.API.csproj MotoFacil.API/
COPY MotoFacil.Domain/MotoFacil.Domain.csproj MotoFacil.Domain/
COPY MotoFacil.Application/MotoFacil.Application.csproj MotoFacil.Application/

# Restaura as dependências
RUN dotnet restore MotoFacil.API/MotoFacil.API.csproj

# Copia o restante do código-fonte
COPY . .

# Compila e publica a aplicação em modo Release
WORKDIR /src/MotoFacil.API
RUN dotnet publish -c Release -o /app/publish --no-restore

# ================================
# Etapa 2: Runtime (imagem leve)
# ================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80

# Copia os arquivos publicados do estágio anterior
COPY --from=build /app/publish .

# Expõe a porta 80 (padrão para Web Apps no Azure)
EXPOSE 80

# Define o ponto de entrada
ENTRYPOINT ["dotnet", "MotoFacil.API.dll"]

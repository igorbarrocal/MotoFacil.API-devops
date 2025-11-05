# Build stage (SDK) - ajustado para .NET 8
ARG DOTNET_VERSION=8.0
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src

# Copia apenas arquivos de projeto/solução primeiro para aproveitar cache do restore
COPY *.sln ./
# Se seus csproj estiverem em subpastas, ajuste o COPY abaixo:
COPY **/*.csproj ./
RUN dotnet restore

# Copia o resto do código e publica
COPY . .
RUN dotnet publish -c Release -o /app/publish --no-restore

# Runtime stage (menor)
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80

COPY --from=build /app/publish .

EXPOSE 80

# Ajuste o nome do DLL conforme o assembly publicado pelo seu projeto
ENTRYPOINT ["dotnet", "MotoFacil.API.dll"]
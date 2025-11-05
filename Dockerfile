# Use the .NET SDK to build and publish, then a smaller ASP.NET runtime image to run
# Ajuste DOTNET_VERSION se necessário (ex: 6.0, 7.0, 8.0)
ARG DOTNET_VERSION=7.0
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src

# Copia todos os arquivos de código para o container e restaura dependências
COPY . .
RUN dotnet restore

# Publica em Release para a pasta /app/publish
RUN dotnet publish -c Release -o /app/publish --no-restore

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app

# Permite ouvir na porta 80 (mapeie outra porta no docker run se quiser)
ENV ASPNETCORE_URLS=http://+:80

# Copia o resultado publicado
COPY --from=build /app/publish .

# Defina o nome do assembly principal abaixo. 
# Substitua 'MotoFacil.API.dll' pelo nome do seu .dll se for diferente.
ENV APP_DLL=MotoFacil.API.dll

EXPOSE 80

ENTRYPOINT ["sh", "-c", "dotnet ${APP_DLL}"]
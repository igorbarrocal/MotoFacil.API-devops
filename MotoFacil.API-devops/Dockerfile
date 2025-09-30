# Use imagem oficial .NET 8
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Crie um usuário não-root
RUN useradd -m appuser
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "MotoFacil.API-devops.csproj"
RUN dotnet publish "MotoFacil.API-devops.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MotoFacil.API-devops.dll"]
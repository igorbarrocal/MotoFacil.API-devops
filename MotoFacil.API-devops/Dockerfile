FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY . .
RUN ls
RUN dotnet build "MotoFacil.API-devops/MotoFacil.API-devops.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MotoFacil.API-devops/MotoFacil.API-devops.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MotoFacil.API-devops.dll"]
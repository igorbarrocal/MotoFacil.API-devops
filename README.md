# MotoFácil API - Deploy no Azure (App Service + Azure SQL)

## Integrantes

- Igor Dias Barrocal RM555217
- Cauan da Cruz – RM558238

---
## Links

- https://youtu.be/bx0uO0XeixY

---

## Descrição da Solução

Esta aplicação .NET expõe uma API RESTful para o gerenciamento de motos e usuários, permitindo operações CRUD (Create, Read, Update, Delete) em ambas as entidades. O sistema utiliza Azure SQL como banco de dados em nuvem, garantindo alta disponibilidade e escalabilidade. O deploy é realizado na plataforma Azure App Service, simplificando o gerenciamento e a publicação contínua do backend.

## Benefícios para o Negócio

- Centralização e segurança dos dados na nuvem (Azure SQL)
- Facilidade de publicação e atualização da API (Azure App Service)
- Operações CRUD completas para gestão eficiente de motos e usuários
- Pronto para escalabilidade e integração com outros sistemas

---

## Passo a Passo para Deploy no Azure

### 1. Clone o repositório

```sh
git clone https://github.com/igorbarrocal/MotoFacil.API-devops.git
cd MotoFacil.API-devops
```

---

### 2. Crie os recursos na Azure via CLI

#### a) Grupo de Recursos

```sh
az group create --name motofacil-rg --location brazilSouth
```

#### b) Servidor SQL Azure

```sh
az sql server create --name motofacilserver --resource-group motofacil-rg --location brazilSouth --admin-user motofaciladmin --admin-password "Motofacil#2025"
```

#### c) Banco de Dados SQL

```sh
az sql db create --resource-group motofacil-rg --server motofacilserver --name motofacil-db --service-objective S0
```

#### d) Exibir string de conexão (Ado.Net)

```sh
az sql db show-connection-string --server motofacilserver --name motofacil-db --client ado.net
```

#### e) Liberar acesso da Azure e do seu IP ao SQL

```sh
az sql server firewall-rule create --resource-group motofacil-rg --server motofacilserver --name AllowAzureServices --start-ip-address 0.0.0.0 --end-ip-address 0.0.0.0
az sql server firewall-rule create --resource-group motofacil-rg --server motofacilserver --name AllowLocal --start-ip-address <SEU IP> --end-ip-address <SEU IP>
```

---

### 3. Criar o ambiente de hospedagem

#### a) Plano de App Service

```sh
az appservice plan create --name motofacil-plan --resource-group motofacil-rg --location brazilSouth --sku B1
```

#### b) App Service (.NET 8)

```sh
az webapp create --resource-group motofacil-rg --plan motofacil-plan --name motofacil-app --runtime "DOTNET:8"
```

#### c) Configurar a string de conexão no App Service

```sh
az webapp config connection-string set --resource-group motofacil-rg --name motofacil-app --connection-string-type SQLAzure --settings DefaultConnection="Server=tcp:motofacilserver.database.windows.net,1433;Database=motofacil-db;User ID=motofaciladmin;Password=Motofacil#2025;Encrypt=true;TrustServerCertificate=false;Connection Timeout=30;"
```

---

### 4. Publicar a aplicação

#### a) Compilar e preparar publicação

```sh
dotnet publish -c Release -o ./publish
```

#### b) Compactar arquivos publicados

```sh
Compress-Archive -Path ./publish/* -DestinationPath ./app.zip
```

#### c) Fazer deploy do ZIP para o App Service

```sh
az webapp deployment source config-zip --resource-group motofacil-rg --name motofacil-app --src ./app.zip
```

---

### 5. Acessar o Swagger da API

Abra no navegador:

```
https://motofacil-app.azurewebsites.net/swagger
```

---

## Outros Recursos

- **Script de criação do banco**: [scripts/script_bd.sql](scripts/script_bd.sql)
- **Exemplos de requisições**: ver arquivos `*.http` ou exemplos no Swagger

---


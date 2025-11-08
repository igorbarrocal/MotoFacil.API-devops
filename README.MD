
# MotoFacil.API-devops

Este projeto é uma API RESTful em ASP.NET Core para gestão de motos e usuários, utilizando SQL Server na Azure, Docker e DevOps CI/CD.  
Este README detalha **como funciona**, **como rodar local**, **como publicar no Azure**, **estrutura do projeto**, **dependências** e **solução de problemas** para o deploy.

---

## Sumário

1. [Visão Geral do Projeto](#visão-geral-do-projeto)
2. [Pré-requisitos](#pré-requisitos)
3. [Estrutura do Projeto](#estrutura-do-projeto)
4. [Como Rodar Localmente](#como-rodar-localmente)
5. [Como Realizar o Deploy com Docker & Azure](#como-realizar-o-deploy-com-docker--azure)
6. [Scripts Úteis](#scripts-úteis)
7. [Endpoints da API](#endpoints-da-api)
8. [Migrações e Banco de Dados](#migrações-e-banco-de-dados)
9. [Pipeline DevOps & CI/CD](#pipeline-devops--cicd)
10. [Solução de Problemas Comuns](#solução-de-problemas-comuns)
11. [Referências](#referências)

---

## 1. Visão Geral do Projeto

Este sistema cadastra motos e usuários, expondo endpoints para consulta, inclusão, edição e exclusão, totalmente integrado ao Azure para infraestrutura e DevOps. Utiliza AutoMapper, Entity Framework Core e Swashbuckle (Swagger).

---

## 2. Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/products/docker-desktop)
- [Azure CLI](https://docs.microsoft.com/pt-br/cli/azure/install-azure-cli)
- Conta no [Azure Portal](https://portal.azure.com)
- Acesso à subscrição, recursos e permissões suficientes no Azure

---

## 3. Estrutura do Projeto

```
MotoFacil.API-devops/
├── AutoMapper/               # Perfis para mapeamento de DTOs
├── Controllers/              # API Controllers (Usuario/Moto)
├── Domain/                   # Enums e entidades de domínio
├── DTOs/                     # Data Transfer Objects (Moto, Usuario, Hateoas)
├── Examples/                 # Exemplos para Swagger
├── Helpers/                  # Helpers (ex: Hateoas)
├── Infrastructure/           # Contextos, Mappings, Repositories
├── Migrations/               # Migrations do EF Core
├── Service/                  # Serviços de negócio (Moto, Usuario)
├── scripts/                  # Scripts de banco de dados e deploy
├── Dockerfile                # Build de container para Azure App Service
├── MotoFacil.API-devops.csproj
├── appsettings.json          # Configuração de conexão, logging etc
├── azure-pipelines.yml       # Pipeline CI/CD para DevOps
```

---

## 4. Como Rodar Localmente

1. **Configurar banco SQL local ou Azure**
   - Configure `appsettings.json` com a string de conexão:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=<SQLServer>;Database=<DB>;User ID=<user>;Password=<pass>;"
     }
     ```
2. **Subir migrations no banco**
   ```bash
   dotnet ef database update
   ```
3. **Executar a API**
   ```bash
   dotnet run
   ```
4. **Acessar o Swagger**
   - http://localhost:5129/swagger (ver `launchSettings.json`)

---

## 5. Como Realizar o Deploy com Docker & Azure

### **Fluxo resumido:**
1. **Crie recursos no Azure:** Grupo de recursos, SQL Server, banco, firewall, ACR, plano de App Service e App Service.
2. **Configure connection string no App Service.**
3. **Compile e faça publish da aplicação.**
4. **Build e push Docker para Azure Container Registry.**
5. **Deploy do container no App Service.**
6. **Garanta que variável `ASPNETCORE_URLS` esteja setada para `http://+:80` no App Service.**
7. **Libere firewall para o App Service e seu IP para acessar o banco.**
8. **Suba as migrations no banco.**
9. **Teste o endpoint:**
     [https://<appname>.azurewebsites.net/swagger](https://<appname>.azurewebsites.net/swagger)

### **Script resumido (ver scripts/deploy.commands.txt):**

```
az group create --name <grupo> --location <região>
az sql server create --name <servidor> --resource-group <grupo> --location <região> --admin-user <usuario> --admin-password <senha>
az sql db create --resource-group <grupo> --server <servidor> --name <database>
az sql server firewall-rule create --resource-group <grupo> --server <servidor> --name AllowAzure --start-ip-address 0.0.0.0 --end-ip-address 0.0.0.0
az sql server firewall-rule create --resource-group <grupo> --server <servidor> --name AllowLocal --start-ip-address <SEU_IP> --end-ip-address <SEU_IP>
# Build/push Docker para o ACR e configure o App Service (ver azure-pipelines.yml)
# Configure 'ConnectionStrings__DefaultConnection' e 'ASPNETCORE_URLS=http://+:80' em Variáveis de ambiente do App Service
dotnet ef database update --connection "<string completa Azure SQL>"
```

---

## 6. Scripts Úteis

- `scripts/deploy.commands.txt`: Roteiro para criar e configurar todos recursos Azure
- `scripts/script_bd.sql`: Criação das tabelas no banco (caso queira subir SQL manualmente, sem migrations)

---

## 7. Endpoints da API

Exemplos (ver Controllers):

### **Motos**
- `GET    /api/moto`         — Lista todas motos
- `GET    /api/moto/{id}`    — Detalhe de uma moto
- `POST   /api/moto`         — Cadastra nova moto
- `PUT    /api/moto/{id}`    — Atualiza moto
- `DELETE /api/moto/{id}`    — Remove moto

### **Usuarios**
- `GET    /api/usuario`      — Lista todos usuários
- `GET    /api/usuario/{id}` — Detalhe de um usuário
- `POST   /api/usuario`      — Cadastra novo usuário
- `PUT    /api/usuario/{id}` — Atualiza usuário
- `DELETE /api/usuario/{id}` — Remove usuário

Veja exemplos prontos em `/Examples`.

---

## 8. Migrações e Banco de Dados

- Use **Entity Framework Core** para criar e atualizar o banco via migrations (`dotnet ef database update`).
- Ou execute o `scripts/script_bd.sql` diretamente no portal/do editor de banco.
- Tabelas: `USUARIO`, `MOTO`.

---

## 9. Pipeline DevOps & CI/CD

- O arquivo `azure-pipelines.yml` gera imagem docker, faz push no ACR e deploy no App Service de Containers.
- Personalize variáveis, nomes e URLs conforme seu recurso e registro.

---

## 10. Solução de Problemas Comuns

### **a. Application Error no Azure**
- Cheque logs do container (portal)
- Certifique-se que o container expõe e escuta na porta 80
- Configure `ASPNETCORE_URLS` = `http://+:80`
- Connection string `ConnectionStrings__DefaultConnection` nas variáveis do App Service

### **b. Banco não está acessível**
- Libere IP do App Service e seu IP local no firewall Azure SQL
- Verifique usuário/senha/caso de senha especial

### **c. Deploy ZIP falha**
- Arquivo ZIP muito grande ou com dependências faltando
- Compile e publish via `dotnet publish -c Release`

### **d. Migrations não sobem**
- Connection string incorreta
- Libere IP local no firewall do SQL Server
- Confirme database e usuário corretos

### **e. Docker não responde**
- Verifique logs no portal Azure
- Teste local via `docker run -p 80:80 <imagem>`

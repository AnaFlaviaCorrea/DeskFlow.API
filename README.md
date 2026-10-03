# 🎧 DeskFlow API — Gestão de Chamados e Helpdesk de TI

## 🎯 Sobre o Projeto

A **DeskFlow API** é uma Web API RESTful construída em **.NET 10**, utilizando **Entity Framework Core 10** e **SQL Server**.

O sistema realiza o gerenciamento de chamados de suporte técnico, histórico de interações e acompanhamento do status do atendimento.

## 🛠️ Tecnologias Utilizadas

- .NET 10 / Web API
- C#
- Entity Framework Core 10
- SQL Server Express
- OpenAPI
- Git e GitHub

## 🚀 Como Executar a Aplicação

### Pré-requisitos

- .NET SDK 10
- SQL Server ou SQL Server Express em execução
- Git

### Passo a Passo

1. Clone este repositório:

```bash
git clone https://github.com/AnaFlaviaCorrea/DeskFlow.API.git
```

2. Acesse a pasta do projeto:

```bash
cd DeskFlow.API
```

3. Configure a Connection String no arquivo `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=DeskFlowDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

4. Restaure as dependências:

```bash
dotnet restore
```

5. Execute as Migrations para criar ou atualizar o banco de dados:

```bash
dotnet ef database update
```

6. Execute a API:

```bash
dotnet run
```

7. O endereço da aplicação será exibido no terminal, por exemplo:

```text
http://localhost:5285
```

A documentação OpenAPI pode ser acessada em ambiente de desenvolvimento por:

```text
/openapi/v1.json
```

Exemplo:

```text
http://localhost:5285/openapi/v1.json
```

## 🧠 Ciclo de Vida do Chamado

- **Aberto**: chamado registrado pelo solicitante.
- **EmAndamento**: chamado que já está sendo atendido pelo suporte.
- **Fechado**: chamado encerrado com solução e data de conclusão.

O fluxo permitido é:

```text
Aberto → EmAndamento → Fechado
```

## 🧱 Arquitetura em Camadas

- **Controllers**: recebem as requisições HTTP e retornam os Status Codes.
- **Services**: contêm as regras de negócio e validações.
- **Repositories**: realizam o acesso ao banco de dados por meio do Entity Framework Core.
- **Models**: representam as entidades, enums e modelos utilizados pela aplicação.
- **Data**: contém o `AppDbContext` e a configuração de persistência.
- **Middlewares**: realizam o tratamento global e a padronização dos erros da API.

Fluxo principal:

```text
Controller → Service → Repository → AppDbContext → SQL Server
```

## 📌 Principais Funcionalidades

- CRUD de categorias;
- abertura de chamados;
- definição automática de status e data de abertura;
- início do atendimento;
- encerramento com solução obrigatória;
- histórico de interações;
- bloqueio de interação em chamado fechado;
- consulta de chamados com categoria e interações;
- filtros por status, prioridade e categoria;
- tratamento global de exceções.

## 🗄️ Banco de Dados

O banco de dados é gerenciado pelo **Entity Framework Core Migrations**.

Para aplicar as migrations:

```bash
dotnet ef database update
```

O repositório também contém o arquivo:

```text
DeskFlowDb.sql
```

com o script SQL da estrutura do banco.

## 🧪 Testes

O arquivo:

```text
DeskFlow.API.http
```

contém requisições utilizadas para testar os principais endpoints da aplicação.

## 🎥 Vídeo de Apresentação

[Link do vídeo de demonstração — adicionar após a gravação]

# 📄 Licença

Projeto desenvolvido para fins educacionais como parte da avaliação do curso de desenvolvimento Back-End .NET.
# Challenge 2025 - Advanced Business Development with .NET

## Grupo

* Nome: Diego Santos Cardoso
* RM: 558711

* Nome: Pablo Lopes Doria de Andrade
* RM: 556834

* Nome: Vinicius Leopoldino de Oliveira
* RM: 557047

---

## Descrição do Projeto

API RESTful desenvolvida em ASP.NET Core com Entity Framework Core e Oracle, contendo 12 entidades relacionadas ao gerenciamento logístico da empresa Mottu. O projeto foi desenvolvido com foco em boas práticas de arquitetura em camadas, uso de DTOs, validação de dados e tratamento de erros.

---

## Estrutura do Projeto

```
Challenge_1/
├── Application
│   ├── Interfaces
│   └── Services
├── Domain
│   └── Entities
├── Infrastructure
│   └── Data
├── Presentation
│   ├── Controllers
│   └── DTOs
├── Program.cs
└── AppDbContext.cs
```

---

## Tecnologias Utilizadas

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core 7
* Oracle Database (via SQL Developer)
* Swagger (OpenAPI)

---

## Entidades Implementadas (12)

* Endereco
* NotaFiscal
* StatusMoto
* StatusOperacao
* TipoMoto
* Filial
* Patio
* ZonaPatio
* Moto
* Motociclista
* LocalizacaoMoto
* HistoricoLocalizacao

Cada entidade possui:

* Interface com métodos assíncronos
* Classe de serviço com tratamento de exceções
* Controller com métodos GET, GET por ID, GET com filtro, POST, PUT, DELETE

---

## Instruções de Execução Local

### Requisitos

* .NET 8 SDK
* Oracle XE ou acesso ao banco via Oracle SQL Developer

### Configurar string de conexão

No arquivo `appsettings.json`, configure a conexão Oracle:

```json
"ConnectionStrings": {
  "OracleConnection": "User Id=USUARIO;Password=SENHA;Data Source=..."
}
```

### Executar API localmente

```bash
dotnet restore
dotnet build
dotnet run
```

Acesse: `https://localhost:5001/swagger`

---

## Link para o GitHub

[https://github.com/usuario/nome-do-repositorio](https://github.com/usuario/nome-do-repositorio)

---

## Observação Final

O projeto foi desenvolvido e testado localmente com o banco Oracle acessado via SQL Developer. As evidências de execução estão incluídas no PDF final da entrega.

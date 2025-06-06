# Projeto Micro-ondas Digital - API RESTful

Este repositório contém uma API desenvolvida em **ASP.NET Core 8** que simula as funcionalidades de um micro-ondas digital. A API permite o gerenciamento de programas de aquecimento e descongelamento com tempo, potência e instruções personalizadas.

---

## 🧭 Visão Geral

A API "Micro-ondas Digital" é um serviço RESTful criado para registrar e manipular opções de aquecimento, como se estivéssemos interagindo com um micro-ondas digital moderno. É possível cadastrar, alterar, remover e consultar programas com configurações específicas.

---

## ⚙️ Tecnologias Utilizadas

- **Linguagens e Frameworks:**
  - ASP.NET Core 8
  - C#
- **Banco de Dados:**
  - SQL Server
- **Arquitetura:**
  - RESTful API
  - DTO (Data Transfer Objects)
  - Response Wrapper
- **Principais Pacotes (NuGet):**
  - `Microsoft.Data.SqlClient`
  - `System.Data.SqlClient`
  - `Microsoft.Extensions.ApiDescription.Server`
  - `Microsoft.OpenApi`
  - `Swashbuckle.AspNetCore`
    - `Swashbuckle.AspNetCore.Swagger`
    - `Swashbuckle.AspNetCore.SwaggerGen`
    - `Swashbuckle.AspNetCore.SwaggerUI`

---

## 🔧 Funcionalidades da API

- Adicionar nova opção de micro-ondas
- Editar uma opção existente
- Listar todas as opções
- Buscar opção por ID
- Remover opção por ID

---

## 🔗 Endpoints da API


| Método | Endpoint                          | Descrição                                    |
|--------|-----------------------------------|----------------------------------------------|
| POST   | `/api/microondas/programas`       | Adiciona um novo programa                    |
| PUT    | `/api/microondas/programas/{id}`  | Atualiza um programa existente               |
| GET    | `/api/microondas/programas`       | Lista todos os programas disponíveis         |
| GET    | `/api/microondas/programas/{id}`  | Busca os detalhes de um programa específico  |
| DELETE | `/api/microondas/programas/{id}`  | Remove um programa do banco de dados         |

---

### 1. Criar Nova Opção

- **Endpoint:** `POST /api/MicroondasControllers`
- **Request Body:**
```json
{
  "nomePrograma": "string",
  "alimento": "string",
  "tempo": 0,
  "potencia": 0,
  "instrucoes": "string",
  "preDefinido": true
}
```
- **Sucesso:**
```json
{
  "data": int,
  "message": "string",
  "success": bool
}
```
- **Erro:**
```json
{
  "data": null,
  "message": "string",
  "success": bool
}
```

### 2. Atualizar Opção

- **Endpoint:** `PUT /api/MicroondasControllers/{id}`
- **Request Body:**
```json
{
  "nomePrograma": "string",
  "alimento": "string",
  "tempo": 0,
  "potencia": 0,
  "instrucoes": "string",
  "preDefinido": true
}
```
- **Sucesso:**
```json
{
  "data": {
      "id": int,
      "nomePrograma": "string",
      "alimento": "string",
      "tempo": int,
      "potencia": int,
      "instrucoes": "string",
      "preDefinido": bool
    },
  "message": "string",
  "success": boll
}
```
- **Erro:**
```json
{
  "data": null,
  "message": "string",
  "success": bool
}
```

### 3. Buscar Opção por ID

- **Endpoint:** `GET /api/MicroondasControllers/{id}`
- **Sucesso:**
```json
{
  "data": {
      "id": int,
      "nomePrograma": "string",
      "alimento": "string",
      "tempo": int,
      "potencia": int,
      "instrucoes": "string",
      "preDefinido": bool
    },
  "message": "string",
  "success": boll
}
```
- **Erro:**
```json
{
  "data": null,
  "message": "string",
  "success": bool
}
```

### 4. Listar Todas as Opções

- **Endpoint:** `GET /api/MicroondasControllers`
- **Sucesso:**
```json
{
  "data": [
    {
      "id": int,
      "nomePrograma": "string",
      "alimento": "string",
      "tempo": int,
      "potencia": int,
      "instrucoes": "string",
      "preDefinido": bool
    },
    {
      "id": int,
      "nomePrograma": "string",
      "alimento": "string",
      "tempo": int,
      "potencia": int,
      "instrucoes": "string",
      "preDefinido": bool
    }
  ],
  "message": "string",
  "success": bool
}
```

### 5. Remover Opção

- **Endpoint:** `DELETE /api/MicroondasControllers/{id}`
- **Sucesso:**
```json
{
  "data": "string",
  "message": null,
  "success": true
}
```
- **Erro:**
```json
{
  "data": null,
  "message": "string",
  "success": false
}
```

---

## 🚀 Como Executar o Projeto Localmente

1. **Abra a Solução:** `Microondas.sln` no Visual Studio.
2. **Configure a Conexão com o Banco:** No `appsettings.json`, edite `"ConnectionStrings"` com sua string de conexão do SQL Server.
3. **Ajuste as URLs de Execução:** No `Properties\launchSettings.json`, configure `applicationUrl` para:
   ```json
   "https://localhost:7093;http://localhost:5091"
   ```
4. **Execute o Projeto:** Pressione `F5` ou `Ctrl + F5`.

---

## 📑 Swagger (Documentação Interativa)

A API já inclui integração com o Swagger, permitindo explorar os endpoints visualmente.

- **Acesse via navegador:**  
  [https://localhost:7093/swagger](https://localhost:7093/swagger)  
  *(ajuste a porta caso necessário)*

---
# Banco de Dados - db_micronda

Este banco de dados foi criado para armazenar as opções de programas pré-definidos utilizados por um micro-ondas digital.

## 🎯 Nome do Banco de Dados
`db_micronda`

## 🗂️ Tabela Principal
### `opcoes_pre_definidas`

Tabela responsável por armazenar os programas personalizados ou padrões de aquecimento do micro-ondas.

| Campo           | Tipo           | Restrições                      |
|----------------|----------------|---------------------------------|
| `id`           | INT            | Chave primária, auto incremento |
| `nome_programa`| NVARCHAR(100)  | Não nulo                        |
| `alimento`     | NVARCHAR(100)  | Não nulo                        |
| `tempo`        | INT            | Não nulo                        |
| `potencia`     | INT            | Não nulo                        |
| `instrucoes`   | NVARCHAR(MAX)  | Opcional                        |
| `pre_definido` | BIT            | valor padrão=false, 1 = true    |

## 🛠️ Script de Criação

```sql
CREATE DATABASE db_micronda;
GO

USE db_micronda;
GO

CREATE TABLE opcoes_pre_definidas (
    id INT PRIMARY KEY IDENTITY(1,1),
    nome_programa NVARCHAR(100) NOT NULL,
    alimento NVARCHAR(100) NOT NULL,
    tempo INT NOT NULL,
    potencia INT NOT NULL,
    instrucoes NVARCHAR(MAX),
    pre_definido BIT NOT NULL DEFAULT(0)
);
GO


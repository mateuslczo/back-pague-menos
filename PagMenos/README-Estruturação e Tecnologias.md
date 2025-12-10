# Estrutura do Projeto ASP.NET Core - Baseado nos PackageReferences

O projeto é um **projeto Web ASP.NET Core** com target framework `.NET 8`, configurado com autenticação, persistência de dados, validação, mapeamento de objetos, documentação de API e suporte a containers Docker.

---

## Configurações Principais

- **Target Framework:** `net8.0`  
- **Nullable:** `enable` (ativado para segurança de nulidade)  
- **Implicit Usings:** `enable` (usings implícitos habilitados)  
- **User Secrets ID:** `9ab53f95-2196-4cd9-91c8-65c53e2ec28e`  
- **Docker:** configurado para Linux, com contexto de build `.`  

---

## Pacotes e Organização Funcional

O projeto utiliza diversos pacotes NuGet, organizados por sua funcionalidade principal:

### 1. Autenticação e Segurança

| Pacote | Versão | Propósito |
|--------|--------|-----------|
| `AspNetCore.Authentication.Basic` | 8.0.0 | Suporte a autenticação Basic HTTP |
| `Microsoft.AspNetCore.Authentication.JwtBearer` | 8.0.10 | Autenticação via JWT para APIs |

> Esses pacotes indicam que o projeto suporta múltiplos esquemas de autenticação, incluindo tokens JWT e Basic Auth.

---

### 2. Persistência de Dados

| Pacote | Versão | Propósito |
|--------|--------|-----------|
| `Microsoft.EntityFrameworkCore` | 8.0.0 | ORM principal para acesso a banco de dados relacional |
| `Microsoft.EntityFrameworkCore.InMemory` | 8.0.0 | Banco de dados em memória para testes e desenvolvimento |

> O uso de `InMemory` sugere suporte a testes unitários ou desenvolvimento sem um banco físico.

---

### 3. Mapeamento de Objetos

| Pacote | Versão | Propósito |
|--------|--------|-----------|
| `AutoMapper` | 15.1.0 | Mapeamento entre DTOs e entidades do domínio |

---

### 4. Validação de Dados

| Pacote | Versão | Propósito |
|--------|--------|-----------|
| `FluentValidation` | 12.1.0 | Criação de regras de validação para modelos de entrada |

---

### 5. Documentação e Geração de Código

| Pacote | Versão | Propósito |
|--------|--------|-----------|
| `Swashbuckle.AspNetCore` | 6.4.0 | Geração de documentação Swagger para APIs |
| `Microsoft.VisualStudio.Web.CodeGeneration.Design` | 8.0.7 | Suporte a scaffolding de controllers e views |

---

### 6. Suporte a Containers e Hosting

| Pacote | Versão | Propósito |
|--------|--------|-----------|
| `Microsoft.Extensions.Hosting` | 8.0.0 | Suporte ao modelo de hospedagem genérico do .NET |
| `Microsoft.VisualStudio.Azure.Containers.Tools.Targets` | 1.21.0 | Integração com Docker e Azure Containers |

---

### 7. IA

- Foram realizadas consulta para IA para esclarecimento de dúvidas sobre as autenticação e autorização Azure AD B2C de forma global na Program.cs
- Resolução de conflitos de versões de bibliotecas envolvidas no projeto

## Conclusão

A estrutura do projeto está organizada para suportar:

- **APIs seguras**, com autenticação JWT e Basic.  
- **Persistência de dados** via Entity Framework Core, incluindo suporte a testes com banco em memória.  
- **Mapeamento entre objetos e DTOs** com AutoMapper.  
- **Validação robusta de entrada de dados** com FluentValidation.  
- **Documentação automática** de endpoints com Swagger.  
- **Facilidade de desenvolvimento e integração com containers**, permitindo deploy em Docker/Linux e Azure.  

> Essa organização demonstra uma arquitetura moderna para aplicações Web ASP.NET Core, seguindo boas práticas de modularidade, teste e deploy.

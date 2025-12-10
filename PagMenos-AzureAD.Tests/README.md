# Classe de Teste - PagMenos_AzureAD.Tests

O projeto `PagMenos_AzureAD.Tests` é um **projeto de teste** em .NET 8, configurado para usar **xUnit** como framework de testes e **Moq** para mocks de dependências. Ele referencia o projeto principal `PagMenos` para testar suas classes e serviços.

---

## Configurações do Projeto

- **Target Framework:** `net8.0`  
- **Namespace Raiz:** `PagMenos_AzureAD.Tests`  
- **É um projeto de teste:** `true`  
- **Não empacotável:** `false`  

---

## Pacotes NuGet Utilizados

| Pacote | Versão | Propósito |
|--------|--------|-----------|
| `coverlet.collector` | 6.0.0 | Coleta de cobertura de testes |
| `Microsoft.AspNetCore.Mvc.Testing` | 8.0.0 | Testes de integração para APIs ASP.NET Core |
| `Microsoft.NET.Test.Sdk` | 17.8.0 | SDK de teste para execução no Visual Studio e CLI |
| `Moq` | 4.20.72 | Mock de dependências para testes unitários |
| `xunit` | 2.5.3 | Framework de testes unitários |
| `xunit.runner.visualstudio` | 2.5.3 | Runner do xUnit para Visual Studio |

---

## Referências do Projeto

- Referência ao projeto principal: `..\PagMenos\PagMenos.csproj`  
- Permite testar serviços, controllers e outras classes internas do projeto principal.

---


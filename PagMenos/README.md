# PagMenos - Executando com Docker

Este guia explica como construir e executar a aplicação **PagMenos** usando Docker, com base no Dockerfile fornecido.

---

## Pré-requisitos

- Docker instalado (Windows, Linux ou macOS)  
- .NET 8 SDK (opcional, apenas se desejar build local sem Docker)

---

## Estrutura do Dockerfile

O Dockerfile possui quatro estágios principais:

1. **base**  
   - Imagem: `mcr.microsoft.com/dotnet/aspnet:8.0`  
   - Configura o ambiente de execução da aplicação  
   - Define usuário `app` e diretório `/app`  
   - Expõe as portas **8080** e **8081**

2. **build**  
   - Imagem: `mcr.microsoft.com/dotnet/sdk:8.0`  
   - Restaura pacotes, compila o projeto (`dotnet build`) e prepara o diretório `/app/build`

3. **publish**  
   - Publica a aplicação pronta para produção no diretório `/app/publish`  
   - Remove dependência do host (`/p:UseAppHost=false`)

4. **final**  
   - Imagem final baseada na imagem `base`  
   - Copia os arquivos publicados do estágio `publish`  
   - Define o ponto de entrada: `dotnet PagMenos.dll`

---

## Como construir a imagem Docker

No terminal, estando na raiz do projeto (onde está o `Dockerfile`), execute:

```bash
docker build -t pagmenos:latest .

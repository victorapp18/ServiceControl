
# ServiceControl - Projeto para Construtora Caiapó

Olá!  
Seja bem-vindo ao projeto **ServiceControl**. Essa aplicação foi desenvolvida como parte de um processo seletivo para a **Construtora Caiapó**. Aqui você vai encontrar tudo o que precisa para entender, rodar e testar o projeto localmente.

---

## O que é o ServiceControl?

O ServiceControl é um microserviço em .NET 8 que registra dados de obras (como nome, localização e clima atual). Ele se comunica com a API pública do OpenWeatherMap para obter as informações climáticas, e armazena os dados em memória (não precisa de banco de dados para testes locais).

---

## Principais tecnologias usadas

- .NET 8
- ASP.NET Core (Web API)
- Injeção de dependência (nativa do .NET)
- Arquitetura em camadas
- SOLID, boas práticas e clean code
- API REST com endpoints organizados
- Swagger para documentação interativa
- Testes unitários com xUnit
- Requisições externas com `HttpClient`

---

## Estrutura do projeto

```
ServiceControl
│
├── ServiceControl.API              → Camada de apresentação (controllers, configurações)
├── ServiceControl.Application      → Lógica de aplicação (serviços, interfaces)
├── ServiceControl.Domain           → Entidades e interfaces de domínio
├── ServiceControl.Infrastructure   → Implementações de repositórios, chamadas HTTP
└── ServiceControl.Tests            → Testes unitários com xUnit
```

---

## Como rodar o projeto localmente

### 1. Clonar o repositório

```bash
git clone https://github.com/victorapp18/ServiceControl.git
cd ServiceControl
```

### 2. Abrir no Visual Studio

> Certifique-se de ter o SDK do **.NET 8** instalado.

### 3. Configurar a chave do OpenWeatherMap

No arquivo `appsettings.json`:

```json
"OpenWeatherMap": {
  "ApiKey": "363c8ee76db578dd2d6bb358e90d94e4",
  "BaseUrl": "https://api.openweathermap.org/data/2.5/weather"
}
```

### 4. Rodar a aplicação

Execute o projeto `ServiceControl.API` no Visual Studio com `F5` ou `Ctrl + F5`.

---

## Executando com Docker

### Pré-requisitos

- Docker instalado na máquina

### Passos

1. Criar a imagem Docker:

```bash
docker build -t servicecontrol-api -f ServiceControl.API/Dockerfile .
```

2. Rodar o container:

```bash
docker run -d -p 5000:80 --name servicecontrol   -e "OpenWeatherMap__ApiKey=SUA_CHAVE_AQUI"   -e "OpenWeatherMap__BaseUrl=https://api.openweathermap.org/data/2.5/weather"   servicecontrol-api
```

3. Acessar a API:

```
http://localhost:5000/swagger
```

---

## Rodando os testes

```bash
dotnet test
```

---

## Autor

Projeto desenvolvido por Victor Arthur para o processo seletivo da Construtora Caiapó.

---

## Contato

- Email: victorapp18@gmail.com
- LinkedIn: https://www.linkedin.com/in/victor-arthur-b9ba57a9?utm_source=share&utm_campaign=share_via&utm_content=profile&utm_medium=ios_app

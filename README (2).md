# 🛠️ ServiceControl - Microserviço em .NET 8

Este projeto é um microserviço desenvolvido como parte de um desafio técnico para a **Construtora Caiapó**. Ele é responsável por processar registros de obras, consultar o clima atual via API externa (OpenWeatherMap) e armazenar os dados em memória.

## 📋 Funcionalidades

- 📡 Consulta de clima atual com base em uma cidade via OpenWeatherMap
- 🧠 Armazenamento em memória dos registros
- 🔁 API RESTful desenvolvida com ASP.NET Core 8
- ✅ Aplicação estruturada com boas práticas (SOLID, Injeção de Dependência)
- 🧪 Pronta para testes unitários

---

## 🏁 Como executar o projeto localmente

### ✅ Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) ou outro editor compatível
- (Opcional) Docker instalado para publicação futura

### 🔧 Passos para executar:

1. **Clone o repositório:**

```bash
git clone https://github.com/seu-usuario/ServiceControl.git
cd ServiceControl
```

2. **Configure o `appsettings.json`:**

Vá até o projeto `ServiceControl.API` e verifique se o arquivo `appsettings.json` contém:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "OpenWeatherMap": {
    "ApiKey": "SUA_CHAVE_AQUI",
    "BaseUrl": "https://api.openweathermap.org/data/2.5/weather"
  }
}
```

> 🔐 Substitua `"SUA_CHAVE_AQUI"` pela sua chave real do [OpenWeatherMap](https://openweathermap.org/api).

3. **Rode a aplicação:**

No terminal:

```bash
cd ServiceControl.API
dotnet run
```

Ou pelo Visual Studio, defina `ServiceControl.API` como projeto de inicialização e pressione **F5**.

---

## 🧪 Testando a API

Com a aplicação rodando, acesse:

- **Swagger UI:** `http://localhost:5000/swagger` ou `https://localhost:5001/swagger`

Endpoints disponíveis:

| Método | Rota                 | Descrição                         |
|--------|----------------------|-----------------------------------|
| `POST` | `/api/clima`         | Consulta clima e registra         |
| `GET`  | `/api/registros`     | Retorna todos os registros salvos |

---

## 🗂️ Estrutura do Projeto

```bash
ServiceControl/
│
├── ServiceControl.API/          # API principal (.NET 8)
├── ServiceControl.Application/  # Serviços da aplicação
├── ServiceControl.Domain/       # Entidades e interfaces
└── ServiceControl.Infrastructure/ # Implementações, repositórios e configurações
```

---

## 📦 Tecnologias Utilizadas

- ASP.NET Core 8
- Injeção de Dependência
- OpenWeatherMap API
- Armazenamento em memória com `ConcurrentDictionary`
- Swagger para documentação
- Boas práticas (SOLID, Clean Architecture)

---

## ✍️ Autor

Desenvolvido por **Victor Arthur de Moraes da Cunha**  
📍 Belém - PA | 💼 Engenheiro de Software Sênior

---

## 📄 Licença

Este projeto é de uso exclusivo para fins de avaliação técnica e não possui licença de uso comercial.
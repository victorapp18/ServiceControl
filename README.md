
# ServiceControl API

Projeto API em .NET 8 para controle de registros de obra, integração com a API OpenWeatherMap e envio dos dados processados.

---

## Como rodar o projeto localmente com Docker

Siga os passos abaixo para rodar a aplicação usando Docker e garantir que tudo funcione certinho.

### Passo 1: Clone o repositório

```bash
git clone https://github.com/victorapp18/ServiceControl.git
cd ServiceControl
```

### Passo 2: Verifique a configuração do Docker Compose

No arquivo `docker-compose.yml`, a aplicação está configurada para expor a porta 80 do container na porta 5000 da sua máquina local. Ou seja, para acessar a API, use a porta 5000.

### Passo 3: Ajuste do `appsettings.json`

Confirme que o arquivo `appsettings.json` está dentro da pasta `ServiceControl.WebAPI` e contém a configuração do OpenWeatherMap com sua chave API e a base URL.

Exemplo mínimo:

```json
{
  "OpenWeatherMapSettings": {
    "ApiKey": "363c8ee76db578dd2d6bb358e90d94e4",
    "BaseUrl": "https://api.openweathermap.org/data/2.5/"
  }
}
```

### Passo 4: Build e execução do container

No terminal, dentro da pasta raiz do projeto, rode:

```bash
docker-compose up --build
```

Isso vai construir a imagem e iniciar o container da API.

### Passo 5: Acesse a API

Abra o navegador ou uma ferramenta como Postman e acesse:

```
http://localhost:5000/swagger
```

Você poderá visualizar a documentação automática da API e testar os endpoints.

---

## Observações importantes

- A aplicação escuta na porta 80 dentro do container, e mapeamos para a porta 5000 da máquina local.
- Para alterar a porta local, ajuste o campo `ports` no `docker-compose.yml`.
- Garanta que nenhuma outra aplicação esteja usando a porta local escolhida.
- Para parar o container, use `docker-compose down`.

---

## Links públicos

- Código-fonte e documentação no GitHub:  
  https://github.com/victorapp18/ServiceControl

- Imagem Docker publicada no Docker Hub:  
  victorapp18/servicecontrol

---

Se precisar de ajuda para rodar ou testar, estou à disposição!  

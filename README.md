# ServiceControl API

Projeto API em .NET 8 para controle de registros e integração com a API OpenWeatherMap.

---

## Como rodar o projeto localmente com Docker

Siga os passos abaixo para rodar a aplicação usando Docker, garantindo que tudo vai funcionar certinho.

### Passo 1: Clone o repositório

```bash
git clone https://seu-repositorio.git
cd ServiceControl
```

### Passo 2: Verifique a configuração do Docker Compose

No arquivo `docker-compose.yml`, a aplicação está configurada para expor a porta 80 do container na porta 5000 da sua máquina local. Ou seja, para acessar a API, você vai usar a porta 5000.

### Passo 3: Ajuste do appsettings.json

Certifique-se que o arquivo `appsettings.json` está na pasta correta (`ServiceControl.WebAPI`) e contém a configuração do OpenWeatherMap com a chave API e base URL.

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

Aqui você poderá visualizar a documentação automática da API e testar os endpoints.

---

## Observações importantes

- O projeto está configurado para escutar na porta 80 dentro do container, por isso fazemos o mapeamento da porta 5000 da máquina para a porta 80 do container.
- Caso precise mudar a porta local, ajuste o arquivo `docker-compose.yml` no campo `ports`.
- Garanta que nenhuma outra aplicação esteja usando a porta local que escolher.
- Para parar o container, use `docker-compose down`.


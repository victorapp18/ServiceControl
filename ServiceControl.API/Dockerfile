# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos da solução
COPY *.sln ./
COPY ServiceControl.API/ServiceControl.API.csproj ServiceControl.API/
COPY ServiceControl.Application/ServiceControl.Application.csproj ServiceControl.Application/
COPY ServiceControl.Domain/ServiceControl.Domain.csproj ServiceControl.Domain/
COPY ServiceControl.Infrastructure/ServiceControl.Infrastructure.csproj ServiceControl.Infrastructure/
COPY ServiceControl.Tests/ServiceControl.Tests.csproj ServiceControl.Tests/

# Restaura dependências
RUN dotnet restore

# Copia o restante do código
COPY . .

# Publica o projeto principal
WORKDIR /src/ServiceControl.API
RUN dotnet publish -c Release -o /app

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ServiceControl.API.dll"]

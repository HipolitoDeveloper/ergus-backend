FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env
WORKDIR /app
EXPOSE 80
EXPOSE 44320

# Cria o diretório src e copiando todo o código-fonte para ele
WORKDIR /src
COPY . ./

# Acessa o diretório src, publicando para o diretório "publish"
WORKDIR /src
RUN dotnet publish "Ergus.Backend.WebApi.Catalogo/Ergus.Backend.WebApi.Catalogo.csproj" -c Release -o publish

# Define a imagem do runtime
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

RUN ["apt-get", "update"]
RUN apt-get install vim -y

# Adicionando bibliotecas para a cultura PT-BR
RUN apt-get install -y locales
RUN sed -i -e 's/# pt_BR.UTF-8 UTF-8/pt_BR.UTF-8 UTF-8/' /etc/locale.gen && \
    locale-gen

WORKDIR /app

# Copia todos os publicáveis para o diretório app
COPY --from=build-env /src/publish .

RUN rm -rf publish cs de es fr it ja ko pl ru tr zh-* *.bat

# Define o environment
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS http://*:44320
ENV PORT = 44320;
ENV HOST = '0.0.0.0';

# Configuração para utilização do horário padrão PT-BR
ENV TZ=America/Sao_Paulo
ENV LANG pt_BR.UTF-8
ENV LANGUAGE ${LANG}
ENV LC_ALL ${LANG}

ENTRYPOINT ["dotnet", "Ergus.Backend.WebApi.Catalogo.dll"]
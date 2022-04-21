# Ergus
## _API de autorização e API de catálogo_

### Docker
1- Acessar a pasta Docker via prompt de comando e criar a imagem da api de autorização
``` docker build -f auth.dockerfile -t ergus/backend.webapi:auth ../src ```

2- Criar a imagem da api de catálogo
``` docker build -f catalogo.dockerfile -t ergus/backend.webapi:catalogo ../src ```

3- Criar o container de autorização (apontando para a porta 44319)
``` docker run -d -p 44319:44319 --name ergus_auth ergus/backend.webapi:auth ```

3- Criar o container de catálogo (apontando para a porta 44320)
``` docker run -d -p 44320:44320 --name ergus_catalogo ergus/backend.webapi:catalogo ```


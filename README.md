# Building and running the docker images manually

VIKTIG! husk å slå av ZScaler!!!! Dersom Zscaler er på så vil f.eks BulletinDotnetApi ikke klare å kontakte Auth0 og da vil kall som skal være autentisterte mot containeren feile.

```bash
# --env POSTGRES_USER=postgres
docker run -itd --name postgres-db -p 5432:5432 --env POSTGRES_PASSWORD=password postgres:14.5
# Kjøre opp pgadmin i docker om man vil
# docker run -d --name pgadmin --env PGADMIN_DEFAULT_PASSWORD=password --env PGADMIN_DEFAULT_EMAIL=post@gres.no -p 81:80 --network bnet dpage/pgadmin4


cd .\BulletinJavaApi\
docker build -t java-api-image .
# vil gi feil pga nettverk
docker run -d --name java-api -p 8080:8080 --env DB_HOST=postgres-db java-api-image
docker logs java-api
# fikse nettverk
docker network create bnet
docker network connect bnet postgres-db
docker run -d --name java-api -p 8080:8080 --env DB_HOST=postgres-db --network bnet java-api-image
docker logs -f java-api

cd AzureFunction
docker image build -t az-func-image .
docker run -d -p 82:80 --name az-func --network bnet az-func-image

cd BulletinDotnetApi
docker build -t dotnet-api-image -f .\BulletinDotnetApi\Dockerfile .
# Trenger ikke gå mot eksternt eksponert port innad i nettverket, se az-func:80 som er eksponert på 82 utenfor nettverket
docker run -d --name dotnet-api --env JavaApiBaseUrl=http://java-api:8080 --env AzureFunctionApiBasePath=http://az-func:80 -p 5232:80 --network bnet dotnet-api-image

cd BulletinFrontend
docker image build -t frontend-image .
# For frontend så må man faktisk bruke http://localhost:5232 fordi kallene gjøres fra docker hostmaskinens nettleser, ikke fra internt i docker-containeren
docker run -d -p 5173:80 --name frontend --network bnet frontend-image
```

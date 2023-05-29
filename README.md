# Building and running the docker images manually

VIKTIG! husk å slå av ZScaler!!!! Dersom Zscaler er på så vil f.eks BulletinDotnetApi ikke klare å kontakte Auth0 og da vil kall som skal være autentisterte mot containeren feile.

```bash
# https://hub.docker.com/_/postgres
docker run -it --name java-db -p 5432:5432 --env POSTGRES_PASSWORD=password --env POSTGRES_USER=postgres  --network bulletin-network postgres:14.5
docker run -d --name pgadmin --env PGADMIN_DEFAULT_PASSWORD=password --env PGADMIN_DEFAULT_EMAIL=post@gres.no -p 81:80 --network bulletin-network dpage/pgadmin4

cd .\BulletinJavaApi\
docker build -t javademo .

cd BulletinDotnetApi
docker build -t dotnetdemo -f .\BulletinDotnetApi\Dockerfile .

cd BulletinFrontend
docker image build -t frontenddemo .

cd AzureFunctiono
docker image build -t az-func .

docker network create bulletin-network
>> 149b39efad870152e87b27173dd2ef6f8c8605a61a8aa1107f226836cd28f749

docker run -d --name java-api -p 8080:8080 --env DB_HOST=java-db --network bulletin-network javademo
docker run -d --name dotnet-api --env JavaApiBaseUrl=http://javaapi:8080 -p 5232:80 --network bulletin-network dotnetdemo
docker run -d -p 5173:5173 --name frontend --env VITE_API_URL=http://localhost:5232 --network bulletin-network frontenddemo
for fronend så må man faktisk bruke http://localhost:5232 fordi kallene gjøres fra docker hostmaskinens nettleser, ikke fra internt i docker-containeren
docker run -d -p 82:80 --name az-func --network bulletin-network az-func
```
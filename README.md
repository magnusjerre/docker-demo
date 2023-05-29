# Building and running the docker images manually

VIKTIG! husk å slå av ZScaler!!!! Dersom Zscaler er på så vil f.eks BulletinDotnetApi ikke klare å kontakte Auth0 og da vil kall som skal være autentisterte mot containeren feile.

```bash
cd .\BulletinJavaApi\
docker build -t javademo .

cd BulletinDotnetApi
docker build -t dotnetdemo -f .\BulletinDotnetApi\Dockerfile .

cd BulletinFrontend
docker image build -t frontenddemo .

docker network create bulletin-network
>> 149b39efad870152e87b27173dd2ef6f8c8605a61a8aa1107f226836cd28f749

docker run --name javaapi -p 8080:8080 --network bulletin-network javademo
docker run --name dotnetapi --env JavaApiBaseUrl=http://javaapi:8080 -p 5232:80 --network bulletin-network dotnetdemo
docker run  -p 5173:5173 --name frontenddemo --env VITE_API_URL=http://localhost:5232 --network bulletin-network frontenddemo
for fronend så må man faktisk bruke http://localhost:5232 fordi kallene gjøres fra docker hostmaskinens nettleser, ikke fra internt i docker-containeren

```
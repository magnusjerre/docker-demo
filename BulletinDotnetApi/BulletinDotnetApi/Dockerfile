# Autocreated by rider
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BulletinDotnetApi/BulletinDotnetApi.csproj", "BulletinDotnetApi/"]
RUN dotnet restore "BulletinDotnetApi/BulletinDotnetApi.csproj"
COPY . .
WORKDIR "/src/BulletinDotnetApi"
RUN dotnet build "BulletinDotnetApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BulletinDotnetApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BulletinDotnetApi.dll"]

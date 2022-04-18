#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["boomoseries-IMDB-api/boomoseries-IMDB-api.csproj", "boomoseries-IMDB-api/"]
RUN dotnet restore "boomoseries-IMDB-api/boomoseries-IMDB-api.csproj"
COPY . .
WORKDIR "/src/boomoseries-IMDB-api"
RUN dotnet build "boomoseries-IMDB-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "boomoseries-IMDB-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "boomoseries-IMDB-api.dll"]
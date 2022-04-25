#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["boomoseries-Movies-api/boomoseries-Movies-api.csproj", "boomoseries-Movies-api/"]
RUN dotnet restore "boomoseries-Movies-api/boomoseries-Movies-api.csproj"
COPY . .
WORKDIR "/src/boomoseries-Movies-api"
RUN dotnet build "boomoseries-Movies-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "boomoseries-Movies-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV NETFLIX_HOST=http://host.docker.internal:5000/api/v1/Netflix/movies
ENV DISNEY_HOST=http://host.docker.internal:5004/api/v1/Disney/movies
ENV AMAZON_HOST=http://host.docker.internal:5006/api/v1/Amazon/movies
ENV IMDB_HOST=http://host.docker.internal:5002/api/v1/IMDB/movies
ENTRYPOINT ["dotnet", "boomoseries-Movies-api.dll"]
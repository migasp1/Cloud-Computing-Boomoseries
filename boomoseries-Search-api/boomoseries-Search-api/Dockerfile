#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["boomoseries-Search-api/boomoseries-Search-api.csproj", "boomoseries-Search-api/"]
RUN dotnet restore "boomoseries-Search-api/boomoseries-Search-api.csproj"
COPY . .
WORKDIR "/src/boomoseries-Search-api"
RUN dotnet build "boomoseries-Search-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "boomoseries-Search-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV MOVIES_HOST=http://host.docker.internal:5008/api/v1/Movies
ENV SERIES_HOST=http://host.docker.internal:5016/api/v1/Series
ENV BOOKS_HOST=http://host.docker.internal:5014/api/v1/Books
ENTRYPOINT ["dotnet", "boomoseries-Search-api.dll"]
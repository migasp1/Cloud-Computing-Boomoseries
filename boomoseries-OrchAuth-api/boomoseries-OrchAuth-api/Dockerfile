#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["boomoseries-OrchAuth-api/boomoseries-OrchAuth-api.csproj", "boomoseries-OrchAuth-api/"]
RUN dotnet restore "boomoseries-OrchAuth-api/boomoseries-OrchAuth-api.csproj"
COPY . .
WORKDIR "/src/boomoseries-OrchAuth-api"
RUN dotnet build "boomoseries-OrchAuth-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "boomoseries-OrchAuth-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV USERS_HOST=http://host.docker.internal:5020/Users
ENV PREFS_HOST=http://host.docker.internal:5024/UserPreferences/Favorites
ENV SEARCH_HOST=http://host.docker.internal:5018/api/v1/Search
ENTRYPOINT ["dotnet", "boomoseries-OrchAuth-api.dll"]
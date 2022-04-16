#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["boomoseries-Netflix-api/boomoseries-Netflix-api.csproj", "boomoseries-Netflix-api/"]
RUN dotnet restore "boomoseries-Netflix-api/boomoseries-Netflix-api.csproj"
COPY . .
WORKDIR "/src/boomoseries-Netflix-api"
RUN dotnet build "boomoseries-Netflix-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "boomoseries-Netflix-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "boomoseries-Netflix-api.dll"]
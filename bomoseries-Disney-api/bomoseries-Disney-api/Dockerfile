#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["bomoseries-Disney-api/bomoseries-Disney-api.csproj", "bomoseries-Disney-api/"]
RUN dotnet restore "bomoseries-Disney-api/bomoseries-Disney-api.csproj"
COPY . .
WORKDIR "/src/bomoseries-Disney-api"
RUN dotnet build "bomoseries-Disney-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "bomoseries-Disney-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "bomoseries-Disney-api.dll"]
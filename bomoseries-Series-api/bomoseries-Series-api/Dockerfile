#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["bomoseries-Series-api/bomoseries-Series-api.csproj", "bomoseries-Series-api/"]
RUN dotnet restore "bomoseries-Series-api/bomoseries-Series-api.csproj"
COPY . .
WORKDIR "/src/bomoseries-Series-api"
RUN dotnet build "bomoseries-Series-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "bomoseries-Series-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV NETFLIX_HOST=http://host.docker.internal:5000/api/v1/Netflix/series
ENV DISNEY_HOST=http://host.docker.internal:5004/api/v1/Disney/series
ENV AMAZON_HOST=http://host.docker.internal:5006/api/v1/Amazon/series
ENV IMDB_HOST=http://host.docker.internal:5002/api/v1/IMDB/series
ENTRYPOINT ["dotnet", "bomoseries-Series-api.dll"]
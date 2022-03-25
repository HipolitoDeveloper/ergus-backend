#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Ergus.Backend.WebApi.Auth/Ergus.Backend.WebApi.Auth.csproj", "src/Ergus.Backend.WebApi.Auth/"]
RUN dotnet restore "src/Ergus.Backend.WebApi.Auth/Ergus.Backend.WebApi.Auth.csproj"
COPY . .
WORKDIR "/src/src/Ergus.Backend.WebApi.Auth"
RUN dotnet build "Ergus.Backend.WebApi.Auth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ergus.Backend.WebApi.Auth.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ergus.Backend.WebApi.Auth.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/GripItemTrade.Api/GripItemTrade.Api.csproj", "src/GripItemTrade.Api/"]
COPY ["src/GripItemTrade.Core/GripItemTrade.Core.csproj", "src/GripItemTrade.Core/"]
COPY ["src/GripItemTrade.Infrastructure/GripItemTrade.Infrastructure.csproj", "src/GripItemTrade.Infrastructure/"]
COPY ["src/GripItemTrade.Domain/GripItemTrade.Domain.csproj", "src/GripItemTrade.Domain/"]
COPY ["src/GripItemTrade.Application/GripItemTrade.Application.csproj", "src/GripItemTrade.Application/"]
RUN dotnet restore "src/GripItemTrade.Api/GripItemTrade.Api.csproj"
COPY . .
WORKDIR "/src/src/GripItemTrade.Api"
RUN dotnet build "GripItemTrade.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GripItemTrade.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GripItemTrade.Api.dll"]

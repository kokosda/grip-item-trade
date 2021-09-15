#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["tests/GripItemTrade.Infrastructure.Tests/GripItemTrade.Infrastructure.Tests.csproj", "tests/GripItemTrade.Infrastructure.Tests/"]
COPY ["src/GripItemTrade.Infrastructure/GripItemTrade.Infrastructure.csproj", "src/GripItemTrade.Infrastructure/"]
COPY ["src/GripItemTrade.Core/GripItemTrade.Core.csproj", "src/GripItemTrade.Core/"]
COPY ["src/GripItemTrade.Domain/GripItemTrade.Domain.csproj", "src/GripItemTrade.Domain/"]
RUN dotnet restore "tests/GripItemTrade.Infrastructure.Tests/GripItemTrade.Infrastructure.Tests.csproj"
COPY . .
WORKDIR "/src/tests/GripItemTrade.Infrastructure.Tests"
RUN dotnet build "GripItemTrade.Infrastructure.Tests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GripItemTrade.Infrastructure.Tests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GripItemTrade.Infrastructure.Tests.dll"]
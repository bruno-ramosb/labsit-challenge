FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Labsit.Tests/Labsit.Tests.csproj", "src/Labsit.Tests/"]
COPY ["src/Labsit.Application/Labsit.Application.csproj", "src/Labsit.Application/"]
COPY ["src/Labsit.Domain/Labsit.Domain.csproj", "src/Labsit.Domain/"]
COPY ["src/Labsit.Infrastructure/Labsit.Infrastructure.csproj", "src/Labsit.Infrastructure/"]
RUN dotnet restore "./src/Labsit.Tests/./Labsit.Tests.csproj"
COPY . .
WORKDIR "/src/src/Labsit.Tests"
RUN dotnet build "./Labsit.Tests.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS test
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Labsit.Tests.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Labsit.Tests.dll"]
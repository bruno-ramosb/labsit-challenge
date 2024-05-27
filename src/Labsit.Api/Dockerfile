FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Labsit.Api/Labsit.Api.csproj", "src/Labsit.Api/"]
COPY ["src/Labsit.Application/Labsit.Application.csproj", "src/Labsit.Application/"]
COPY ["src/Labsit.Domain/Labsit.Domain.csproj", "src/Labsit.Domain/"]
COPY ["src/Labsit.Infrastructure/Labsit.Infrastructure.csproj", "src/Labsit.Infrastructure/"]
RUN dotnet restore "src/Labsit.Api/Labsit.Api.csproj"
COPY . .
WORKDIR "/src/src/Labsit.Api"
RUN dotnet build "Labsit.Api.csproj" --configuration Release --no-restore --nologo --output /app/build 

FROM build AS publish
RUN dotnet publish "Labsit.Api.csproj" --configuration Release --no-restore --nologo --output /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Labsit.Api.dll"]
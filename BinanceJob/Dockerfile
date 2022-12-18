FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./BinanceJob/BinanceJob.csproj", "BinanceJob/"]
RUN dotnet restore "BinanceJob/BinanceJob.csproj"
COPY . .
WORKDIR "/src/BinanceJob"
RUN dotnet build "BinanceJob.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BinanceJob.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BinanceJob.dll"]
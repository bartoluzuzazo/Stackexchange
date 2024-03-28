FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Stackexchange.API/Stackexchange.API.csproj", "Stackexchange.API/"]
RUN dotnet restore "Stackexchange.API/Stackexchange.API.csproj"
COPY . .
WORKDIR "/src/Stackexchange.API"
RUN dotnet build "Stackexchange.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Stackexchange.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stackexchange.API.dll"]

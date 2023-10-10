FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5100

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Bp.Common/src/Bp.Common.Service/Bp.Common.Service.csproj", "Bp.Common/src/Bp.Common.Service/"]
COPY ["Bp.Client/src/Bp.Client.Service/Bp.Client.Service.csproj", "Bp.Client/src/Bp.Client.Service/"]
RUN dotnet restore "Bp.Client/src/Bp.Client.Service/Bp.Client.Service.csproj"
COPY . .
RUN dotnet build "Bp.Client/src/Bp.Client.Service/Bp.Client.Service.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR /src/Bp.Client/src/Bp.Client.Service
RUN dotnet publish "Bp.Client.Service.csproj" -c Release -o /app/publish/client

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish/client .
ENTRYPOINT ["dotnet", "Bp.Client.Service.dll"]

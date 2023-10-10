FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5100

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Bp.Common/src/Bp.Common.Service/Bp.Common.Service.csproj", "Bp.Common/src/Bp.Common.Service/"]
COPY ["Bp.Transaction/src/Bp.Transaction.Service/Bp.Transaction.Service.csproj", "Bp.Transaction/src/Bp.Transaction.Service/"]
RUN dotnet restore "Bp.Transaction/src/Bp.Transaction.Service/Bp.Transaction.Service.csproj"
COPY . .
RUN dotnet build "Bp.Transaction/src/Bp.Transaction.Service/Bp.Transaction.Service.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR /src/Bp.Transaction/src/Bp.Transaction.Service
RUN dotnet publish "Bp.Transaction.Service.csproj" -c Release -o /app/publish/transaction

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish/transaction .
ENTRYPOINT ["dotnet", "Bp.Transaction.Service.dll"]

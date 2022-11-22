FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY ./src/ .

RUN dotnet publish ./ProductManager.Api/ProductManager.Api.csproj -c Release --framework net7.0 --runtime linux-x64 --self-contained true -o bin

FROM mcr.microsoft.com/dotnet/runtime:7.0
WORKDIR /app
EXPOSE 80

COPY --from=build /app/bin .
ENTRYPOINT ["dotnet", "ProductManager.Api.dll"]
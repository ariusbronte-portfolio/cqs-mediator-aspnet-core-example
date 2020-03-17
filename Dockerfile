FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
COPY [".", "./"]
RUN dotnet build "./src/TodoApi.WebApi/TodoApi.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./src/TodoApi.WebApi/TodoApi.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoApi.WebApi.dll"]
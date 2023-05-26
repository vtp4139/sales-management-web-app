#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SalesManagementWebsite.API/SalesManagementWebsite.API.csproj", "SalesManagementWebsite.API/"]
COPY ["SalesManagementWebsite.Contracts/SalesManagementWebsite.Contracts.csproj", "SalesManagementWebsite.Contracts/"]
COPY ["SalesManagementWebsite.Domain/SalesManagementWebsite.Domain.csproj", "SalesManagementWebsite.Domain/"]
COPY ["SalesManagementWebsite.Infrastructure/SalesManagementWebsite.Infrastructure.csproj", "SalesManagementWebsite.Infrastructure/"]
RUN dotnet restore "SalesManagementWebsite.API/SalesManagementWebsite.API.csproj"
COPY . .
WORKDIR "/src/SalesManagementWebsite.API"
RUN dotnet build "SalesManagementWebsite.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SalesManagementWebsite.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SalesManagementWebsite.API.dll"]
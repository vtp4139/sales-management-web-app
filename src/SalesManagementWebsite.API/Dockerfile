#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SalesManagementWebsite.Core/SalesManagementWebsite.Core.csproj", "SalesManagementWebsite.Core/"]
COPY ["SalesManagementWebsite.Contracts/SalesManagementWebsite.Contracts.csproj", "SalesManagementWebsite.Contracts/"]
COPY ["SalesManagementWebsite.Domain/SalesManagementWebsite.Domain.csproj", "SalesManagementWebsite.Domain/"]
COPY ["SalesManagementWebsite.Infrastructure/SalesManagementWebsite.Infrastructure.csproj", "SalesManagementWebsite.Infrastructure/"]
RUN dotnet restore "SalesManagementWebsite.Core/SalesManagementWebsite.Core.csproj"
COPY . .
WORKDIR "/src/SalesManagementWebsite.Core"
RUN dotnet build "SalesManagementWebsite.Core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SalesManagementWebsite.Core.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SalesManagementWebsite.Core.dll"]
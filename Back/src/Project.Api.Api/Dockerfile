# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Back/src/Project.Api.Api/Project.Api.Api.csproj", "Back/src/Project.Api.Api/"]
COPY ["Back/src/Project.Api.Application/Project.Api.Application.csproj", "Back/src/Project.Api.Application/"]
COPY ["Back/src/Project.Api.Domain/Project.Api.Domain.csproj", "Back/src/Project.Api.Domain/"]
COPY ["Back/src/Project.Api.Shared/Project.Api.Shared.csproj", "Back/src/Project.Api.Shared/"]
COPY ["Back/src/Project.Api.Infra/Project.Api.Infra.csproj", "Back/src/Project.Api.Infra/"]
RUN dotnet restore "./Back/src/Project.Api.Api/Project.Api.Api.csproj"
COPY . .
WORKDIR "/src/Back/src/Project.Api.Api"
RUN dotnet build "./Project.Api.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Project.Api.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Project.Api.Api.dll"]
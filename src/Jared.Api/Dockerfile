#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Jared.Api/Jared.Api.csproj", "src/Jared.Api/"]
COPY ["src/Infrastructure/Jared.Infrastructure/Jared.Infrastructure.csproj", "src/Infrastructure/Jared.Infrastructure/"]
COPY ["src/Application/Jared.Application/Jared.Application.csproj", "src/Application/Jared.Application/"]
COPY ["src/Domain/Jared.Domain/Jared.Domain.csproj", "src/Domain/Jared.Domain/"]
COPY ["src/Shared/Jared.Shared/Jared.Shared.csproj", "src/Shared/Jared.Shared/"]
RUN dotnet restore "./src/Jared.Api/Jared.Api.csproj"
COPY . .
WORKDIR "/src/src/Jared.Api"
RUN dotnet build "./Jared.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Jared.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Jared.Api.dll"]
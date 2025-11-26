#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Directory.Build.props", "./"]
COPY ["src/Jared.Api/Jared.Api.csproj", "src/Jared.Api/"]
COPY ["src/api/Jared.Presentation/Jared.Presentation.csproj", "src/api/Jared.Presentation/"]
COPY ["src/api/Jared.Infrastructure/Jared.Infrastructure.csproj", "src/api/Jared.Infrastructure/"]
COPY ["src/api/Jared.Application/Jared.Application.csproj", "src/api/Jared.Application/"]
COPY ["src/api/Jared.Domain/Jared.Domain.csproj", "src/api/Jared.Domain/"]
COPY ["src/shared/Jared.Shared/Jared.Shared.csproj", "src/shared/Jared.Shared/"]
COPY ["src/shared/Jared.Dtos/Jared.Dtos.csproj", "src/shared/Jared.Dtos/"]
COPY ["src/shared/Jared.Validators/Jared.Validators.csproj", "src/shared/Jared.Validators/"]
COPY ["src/shared/Jared.Contracts/Jared.Contracts.csproj", "src/shared/Jared.Contracts/"]
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
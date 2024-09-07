#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Jared.App/Jared.App.csproj", "src/Jared.App/"]
COPY ["src/Presentation/Jared.Presentation/Jared.Presentation.csproj", "src/Presentation/Jared.Presentation/"]
COPY ["src/Shared/Jared.Shared/Jared.Shared.csproj", "src/Shared/Jared.Shared/"]
RUN dotnet restore "./src/Jared.App/Jared.App.csproj"
COPY . .
WORKDIR "/src/src/Jared.App"
RUN dotnet build "./Jared.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Jared.App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Jared.App.dll"]
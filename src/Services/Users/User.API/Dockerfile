FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Generic/GenericContracts/GenericContracts.csproj", "Generic/GenericContracts/"]
COPY ["Generic/GenericTools/GenericTools.csproj", "Generic/GenericTools/"]

COPY ["Services/Users/User.API/User.API.csproj", "Services/Users/User.API/"]
COPY ["Services/Users/User.Application/User.Application.csproj", "Services/Users/User.Application/"]
COPY ["Services/Users/User.Core/User.Core.csproj", "Services/Users/User.Core/"]
COPY ["Services/Users/User.Repository/User.Repository.csproj", "Services/Users/User.Repository/"]

RUN dotnet restore "Services/Users/User.API/User.API.csproj"
COPY ["Generic/", "Generic/"]
COPY ["Services/Users/", "Services/Users/"]
WORKDIR /src/Services/Users/User.API
RUN dotnet build "User.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "User.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.API.dll"]

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
COPY ["Services/Tasks/Task.Api/Task.Api.csproj", "Services/Tasks/Task.Api/"]
COPY ["Services/Tasks/Task.Application/Task.Application.csproj", "Services/Tasks/Task.Application/"]
COPY ["Services/Tasks/Task.Core/Task.Core.csproj", "Services/Tasks/Task.Core/"]
COPY ["Services/Tasks/Task.Repository/Task.Repository.csproj", "Services/Tasks/Task.Repository/"]

COPY ["Generic/GenericContracts/GenericContracts.csproj", "Generic/GenericContracts/"]
COPY ["Generic/GenericTools/GenericTools.csproj", "Generic/GenericTools/"]

RUN dotnet restore "Services/Tasks/Task.Api/Task.Api.csproj"
COPY ["Generic/", "Generic/"]
COPY ["Services/Tasks/", "Services/Tasks/"]
WORKDIR "/src/Services/Tasks/Task.Api"
RUN dotnet build "Task.Api.csproj" -c "$configuration" -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Task.Api.csproj" -c "$configuration" -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Task.Api.dll"]

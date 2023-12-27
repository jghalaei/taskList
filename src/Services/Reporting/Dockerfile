FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Services/Reporting/Reporting.csproj", "Services/Reporting/"]
COPY ["../../Generic/GenericTools/GenericTools.csproj", "Generic/GenericTools/"]
COPY ["../../Generic/GenericContracts/GenericContracts.csproj", "Generic/GenericContracts/"]
RUN dotnet restore "Services/Reporting/Reporting.csproj"

COPY ["Services/Reporting", "Services/Reporting/"]
COPY ["../../Generic/GenericTools", "Generic/GenericTools/"]
COPY ["../../Generic/GenericContracts", "Generic/GenericContracts/"]

WORKDIR "/src/Services/Reporting""
RUN dotnet build "Reporting.csproj" -c "$configuration" -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Reporting.csproj" -c "$configuration" -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Reporting.dll"]

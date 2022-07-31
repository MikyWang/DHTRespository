﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DHTRespository/DHTRespository.csproj", "DHTRespository/"]
RUN dotnet restore "DHTRespository/DHTRespository.csproj"
COPY . .
WORKDIR "/src/DHTRespository"
RUN dotnet build "DHTRespository.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DHTRespository.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DHTRespository.dll"]

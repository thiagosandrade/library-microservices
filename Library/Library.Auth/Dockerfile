#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 59584/tcp
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

COPY . .
WORKDIR "/src/Library.Auth.Api"
RUN dotnet build "Library.Auth.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Library.Auth.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:59584
ENTRYPOINT ["dotnet", "Library.Auth.Api.dll","http://*:59584"]
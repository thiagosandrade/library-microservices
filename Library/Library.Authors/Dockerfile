#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 59581/tcp
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

COPY . .
WORKDIR "/src/Library.Authors.Api"
RUN dotnet build "Library.Authors.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Library.Authors.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:59581
ENTRYPOINT ["dotnet", "Library.Authors.Api.dll","http://*:59581"]
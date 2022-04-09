#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.


FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 59584/tcp
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src

COPY . .
WORKDIR "/src/Library.Auth.Api"

ARG ASPNETCORE_ENVIRONMENT

RUN dotnet build "Library.Auth.Api.csproj" -c ${ASPNETCORE_ENVIRONMENT} -o /app/build

FROM build AS publish
RUN dotnet publish "Library.Auth.Api.csproj" -c ${ASPNETCORE_ENVIRONMENT} -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:59584
ENTRYPOINT ["dotnet", "Library.Auth.Api.dll","http://*:59584"]
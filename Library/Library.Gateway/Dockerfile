#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.


FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 59580/tcp
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src

COPY . .
WORKDIR "/src"

ARG ASPNETCORE_ENVIRONMENT

RUN dotnet build "Library.Gateway.csproj" -c ${ASPNETCORE_ENVIRONMENT} -o /app/build

FROM build AS publish
RUN dotnet publish "Library.Gateway.csproj" -c ${ASPNETCORE_ENVIRONMENT} -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:59580
ENTRYPOINT ["dotnet", "Library.Gateway.dll","http://*:59580"]
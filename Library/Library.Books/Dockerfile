#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.


FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
ARG PORT
EXPOSE ${PORT}/tcp

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src

COPY . .
WORKDIR "/src/Library.Books.Api"

ARG CONFIG_PROFILE

RUN dotnet restore

FROM build AS publish
RUN dotnet publish "Library.Books.Api.csproj" -c ${CONFIG_PROFILE} -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS http://*:${PORT}
ENTRYPOINT ["dotnet", "Library.Books.Api.dll","http://*:${PORT}"]



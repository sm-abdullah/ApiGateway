#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001
FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine as build
WORKDIR /src
ADD  .  /src/
RUN echo $(ls -1 /src)
RUN dotnet restore "/src/ApiGateway.sln"

COPY . .
WORKDIR "/src/Gateway"
RUN dotnet build "ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:5000;
ENTRYPOINT ["dotnet", "ApiGateway.dll"]
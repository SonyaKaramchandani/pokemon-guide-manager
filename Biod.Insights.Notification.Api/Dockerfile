#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Biod.Insights.Notification.Api/Biod.Insights.Notification.Api.csproj", "Biod.Insights.Notification.Api/"]
RUN dotnet restore "Biod.Insights.Notification.Api/Biod.Insights.Notification.Api.csproj"
COPY . .
WORKDIR "/src/Biod.Insights.Notification.Api"
RUN dotnet build "Biod.Insights.Notification.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Biod.Insights.Notification.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Biod.Insights.Notification.Api.dll"]
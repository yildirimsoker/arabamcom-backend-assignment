#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Arabam.Com.Api/Arabam.Com.Api.csproj", "Arabam.Com.Api/"]
COPY ["Arabam.Com.Infrastructure/Arabam.Com.Infrastructure.csproj", "Arabam.Com.Infrastructure/"]
COPY ["Arabam.Com.Application/Arabam.Com.Application.csproj", "Arabam.Com.Application/"]
COPY ["Arabam.Com.Domain/Arabam.Com.Domain.csproj", "Arabam.Com.Domain/"]
RUN dotnet restore "Arabam.Com.Api/Arabam.Com.Api.csproj"
COPY . .
WORKDIR "/src/Arabam.Com.Api"
RUN dotnet build "Arabam.Com.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Arabam.Com.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Arabam.Com.Api.dll"]




#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Arabam.Com.WorkerService/Arabam.Com.WorkerService.csproj", "Arabam.Com.WorkerService/"]
RUN dotnet restore "Arabam.Com.WorkerService/Arabam.Com.WorkerService.csproj"
COPY . .
WORKDIR "/src/Arabam.Com.WorkerService"
RUN dotnet build "Arabam.Com.WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Arabam.Com.WorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Arabam.Com.WorkerService.dll"]
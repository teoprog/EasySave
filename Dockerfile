FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EasySave/EasySave.csproj", "EasySave/"]
RUN dotnet restore "EasySave/EasySave.csproj"
COPY . .
WORKDIR "/src/EasySave"
RUN dotnet build "EasySave.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EasySave.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EasySave.dll"]

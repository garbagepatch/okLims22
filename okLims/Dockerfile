FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ["okLims/okLims.csproj", "okLims/"]

RUN dotnet restore "okLims/okLims.csproj"
COPY . .
WORKDIR "/src/okLims"
RUN dotnet build "okLims.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "okLims.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "okLims.dll"]
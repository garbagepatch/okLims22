FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build-env
WORKDIR /okLims

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./         
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.1
WORKDIR /okLims
COPY /okLims/bin/Release/netcoreapp2.1/Publish /okLims .
ENTRYPOINT ["dotnet", "okLims/okLims.dll"
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything and restore
COPY . . 
WORKDIR /app/DigitalLibrary.Server
RUN dotnet restore
RUN dotnet publish -c Release -o /publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /publish .

# Expose port
EXPOSE 80
ENTRYPOINT ["dotnet", "DigitalLibrary.Server.dll"]

# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BookStore1.csproj", ""]
RUN dotnet restore "./BookStore1.csproj"
COPY . .
RUN dotnet build "BookStore1.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "BookStore1.csproj" -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookStore1.dll"]

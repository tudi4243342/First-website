# 1. Use the official Microsoft .NET 9 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["First website.csproj", "."]
RUN dotnet restore "./First website.csproj"

# Copy the remaining source code and build it
COPY . .
RUN dotnet publish "First website.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 2. Use the runtime-only image to run the app (keeps the file size small)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Set the port configuration so Render can communicate with the container
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "First website.csproj.dll"]
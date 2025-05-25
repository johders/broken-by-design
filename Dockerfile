# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# restore
COPY ["src/BrokenByDesign.Api/BrokenByDesign.Api.csproj", "BrokenByDesign.Api/"]
RUN dotnet restore "BrokenByDesign.Api/BrokenByDesign.Api.csproj"

# build
COPY ["src/BrokenByDesign.Api", "BrokenByDesign.Api/"]
WORKDIR /src/BrokenByDesign.Api
RUN dotnet build "BrokenByDesign.Api.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "BrokenByDesign.Api.csproj" -c Release -o /app/publish

# Stage 3: Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=5001
EXPOSE 5001
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "BrokenByDesign.Api.dll" ]
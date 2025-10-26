# Stage 1: Build.
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy project files.
COPY *.sln .
COPY src/CleanGo.API/*.csproj ./src/CleanGo.API/
COPY src/CleanGo.Application/*.csproj ./src/CleanGo.Application/
COPY src/CleanGo.Domain/*.csproj ./src/CleanGo.Domain/
COPY src/CleanGo.Infrastructure/*.csproj ./src/CleanGo.Infrastructure/

RUN dotnet restore ./src/CleanGo.API/CleanGo.API.csproj

# Copy the rest and compile.
COPY . .
WORKDIR /app/src/CleanGo.API
RUN dotnet publish -c Release -o /out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /out ./
ENTRYPOINT ["dotnet", "CleanGo.API.dll"]
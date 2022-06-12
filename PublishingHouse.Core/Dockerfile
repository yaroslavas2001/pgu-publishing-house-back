#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# WORKDIR /app
# EXPOSE 80
# EXPOSE 443

# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# WORKDIR /src
# COPY ["rio-psu/rio-psu.csproj", "rio-psu/"]
# RUN dotnet restore "rio-psu/rio-psu.csproj"
# COPY . .
# WORKDIR "/src/rio-psu"
# RUN dotnet build "rio-psu.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "rio-psu.csproj" -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "rio-psu.dll"]
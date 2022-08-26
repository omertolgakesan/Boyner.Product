#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Boyner.Product.API/Boyner.Product.API.csproj", "Boyner.Product.API/"]
RUN dotnet restore "Boyner.Product.API/Boyner.Product.API.csproj"
COPY . .
WORKDIR "/src/Boyner.Product.API"
RUN dotnet build "Boyner.Product.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Boyner.Product.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Boyner.Product.API.dll"]
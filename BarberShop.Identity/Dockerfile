FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BarberShop.Identity/BarberShop.Identity.csproj", "BarberShop.Identity/"]
COPY ["BarberShop/BarberShop.csproj", "BarberShop/"]
RUN dotnet restore "BarberShop.Identity/BarberShop.Identity.csproj"
COPY . .
WORKDIR "/src/BarberShop.Identity"
RUN dotnet build "BarberShop.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BarberShop.Identity.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BarberShop.Identity.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BarberShop.Appointments/BarberShop.Appointments.csproj", "BarberShop.Appointments/"]
COPY ["BarberShop/BarberShop.csproj", "BarberShop/"]
RUN dotnet restore "BarberShop.Appointments/BarberShop.Appointments.csproj"
COPY . .
WORKDIR "/src/BarberShop.Appointments"
RUN dotnet build "BarberShop.Appointments.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BarberShop.Appointments.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BarberShop.Appointments.dll"]

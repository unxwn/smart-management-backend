# --- Build Stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копіюємо всі проєкти
COPY . .

# Встановлюємо залежності
WORKDIR /src/Clinic.Api
RUN dotnet restore

# Публікуємо додаток
RUN dotnet publish -c Release -o /app/publish

# --- Runtime Stage ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Вхідна точка
ENTRYPOINT ["dotnet", "Clinic.Api.dll"]

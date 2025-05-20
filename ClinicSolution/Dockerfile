# --- Build Stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копіюємо csproj окремо, щоб кешувалося restore
COPY Clinic.Api/Clinic.Api.csproj ./Clinic.Api/

# Встановлюємо залежності
RUN dotnet restore ./Clinic.Api/Clinic.Api.csproj

# Копіюємо всі інші файли
COPY . .

# Публікуємо додаток
WORKDIR /src/Clinic.Api
RUN dotnet publish -c Release -o /app/publish

# --- Runtime Stage ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Вхідна точка
ENTRYPOINT ["dotnet", "Clinic.Api.dll"]

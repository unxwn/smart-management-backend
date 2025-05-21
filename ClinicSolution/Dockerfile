# Етап 1: Побудова
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копіюємо файли проекту
COPY ClinicSolution.sln .
COPY Clinic.Api/Clinic.Api.csproj Clinic.Api/
COPY Clinic.Infrastructure/Clinic.Infrastructure.csproj Clinic.Infrastructure/
COPY Clinic.Domain/Clinic.Domain.csproj Clinic.Domain/

# Відновлюємо залежності
RUN dotnet restore

# Копіюємо решту файлів
COPY . .

# Публікуємо додаток
WORKDIR /src/Clinic.Api
RUN dotnet publish -c Release -o /app/publish

# Етап 2: Запуск
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Встановлюємо змінну середовища для порту
ENV ASPNETCORE_URLS=http://+:8080

# Відкриваємо порт
EXPOSE 8080

# Запускаємо додаток
ENTRYPOINT ["dotnet", "Clinic.Api.dll"]

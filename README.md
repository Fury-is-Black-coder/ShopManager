# ShopManager

ShopManager to prosta aplikacja typu **Web API + frontend**, umożliwiająca zarządzanie produktami w systemie sklepów internetowych.  
Projekt został przygotowany jako **rozszerzalna baza pod dalszy rozwój i integracje**.

## 🧩 Zakres funkcjonalny

### Backend (ASP.NET Core Web API)
- CRUD produktów
- Aktywacja / dezaktywacja produktów (soft state)
- Walidacja danych wejściowych
- Poprawne statusy HTTP
- Globalna obsługa błędów
- Testy jednostkowe (xUnit)
- Swagger (OpenAPI)

### Frontend
- Prosty panel administracyjny (HTML/CSS/JS)
- Lista produktów
- Filtrowanie po statusie (aktywne / nieaktywne)
- Dodawanie, edycja, aktywacja, dezaktywacja, usuwanie produktów

Dodatkowo przygotowano **alternatywny frontend w Next.js**, który może działać:
- jako build statyczny serwowany z `wwwroot` (https://localhost:7270/next/)

### Wymagania
- .NET 8 SDK
- (opcjonalnie) Docker

Po uruchomieniu:
Frontend (HTTPS)
HTML, CSS, JavaScript: https://localhost:7270/index.html
Next.js: https://localhost:7270/next/

Backend
API: https://localhost:7270/api/products
Swagger: https://localhost:7270/swagger/index.html


🧪 Testy jednostkowe
Testy z poziomu Visual Studio: Test → Run All Tests (ProductServiceTests.cs)
Testy obejmują kluczową logikę biznesową (duplikacja SKU, dezaktywacja produktu)

🐳 Docker
Projekt zawiera:
Dockerfile (multi-stage, .NET 8)
opcjonalny docker-compose.yml

Uruchomienie:
docker compose up --build
Aplikacja będzie dostępna pod adresem http://localhost:8080


Decyzje projektowe:
Jeden projekt backendowy (ASP.NET Core Web API) – uproszczenie struktury zgodnie z zakresem zadania, przy zachowaniu logicznego podziału na foldery (Controllers, Models, DTOs, Services).
Entity Framework Core + SQLite (Code First) – szybki start bez zewnętrznych zależności, łatwe migracje.
DTO zamiast bezpośredniego zwracania encji – separacja warstwy API od modelu danych.
Soft state (IsActive) – możliwość dezaktywacji produktów bez ich fizycznego usuwania.
Globalny middleware błędów – jedno, spójne miejsce obsługi wyjątków.
Swagger (OpenAPI) – automatyczna dokumentacja API.
Prosty frontend (HTML/CSS/JS) – szybka weryfikacja działania API.
Dodatkowy frontend Next.js – pokazanie, że API może obsługiwać wielu klientów.
Docker / Docker Compose – uproszczone uruchamianie i przygotowanie pod wdrożenie.


Obszary do dalszego rozwoju / refaktoryzacji:
Wydzielenie warstw do osobnych projektów (Domain / Application / Infrastructure)
Dodanie paginacji i sortowania
Autoryzacja i role użytkowników
Logowanie zdarzeń (audyt)
Cache (np. Redis)
Pełne testy integracyjne
CI/CD (GitHub Actions)
Migracja na PostgreSQL / SQL Server

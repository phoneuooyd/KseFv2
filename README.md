# 📱 KSeF Invoice App
##For English - below ⬇️

# Polish - 🇵🇱

**Aplikacja mobilna do zarządzania fakturami elektronicznymi w systemie KSeF (Krajowy System e-Faktur)**

[![.NET MAUI](https://img.shields.io/badge/.NET-MAUI-512BD4?style=flat-square)](https://dotnet.microsoft.com/apps/maui)
[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![SQLite](https://img.shields.io/badge/Database-SQLite-003B57?style=flat-square)](https://www.sqlite.org/)
[![Android](https://img.shields.io/badge/Platform-Android-green?style=flat-square)](https://developer.android.com/)
[![Polish Government](https://img.shields.io/badge/KSeF-Compatible-red?style=flat-square)](https://www.podatki.gov.pl/ksef/)

## 📋 Opis projektu

KSeF Invoice App to kompleksowa aplikacja mobilna stworzona w technologii .NET MAUI, która umożliwia przedsiębiorcom i firmom:
- 🏢 Zarządzanie danymi własnej firmy (firm)  
- 👥 Prowadzenie bazy klientów
- 📦 Katalogowanie produktów i usług
- 📄 Tworzenie i wysyłanie faktur elektronicznych do systemu KSeF
- 📊 Monitorowanie statusu wysłanych faktur
- 🔄 Integrację z testowym i produkcyjnym środowiskiem KSeF

**Autorstwa Przemysława Przybyszewskiego** - Praca inżynierska

## ⚠️ Ważne informacje prawne

**UWAGA**: Dane wprowadzane do aplikacji **muszą** być prawidłowe. Użycie nieprawidłowych danych może prowadzić do błędów oraz (używając serwisów produkcyjnych KSeF) konsekwencji prawnych zgodnie z przepisami podatkowymi RP.

## ✨ Główne funkcjonalności

### 🏢 **Zarządzanie firmą**
- Dodawanie i edycja danych własnej firmy
- Przechowywanie tokenów autoryzacyjnych KSeF
- Konfiguracja danych kontaktowych i adresowych

### 👥 **Baza klientów**
- Dodawanie nowych klientów (osoby fizyczne i podmioty prawne)
- Edytowanie danych klientów
- Zarządzanie danymi kontaktowymi i adresowymi
- Usuwanie niepotrzebnych rekordów

### 📦 **Katalog produktów/usług**
- Tworzenie bazy produktów i usług
- Określanie cen i stawek VAT
- Kategoryzacja według rodzaju (towar/usługa)
- Ustawienie jednostek miary

### 📄 **Tworzenie faktur**
- Intuicyjny kreator faktury z dynamicznym dodawaniem pozycji
- Automatyczne obliczanie wartości netto, brutto i VAT
- Obsługa różnych stawek podatkowych (23%, 8%, 5%, 0%, zw, np, oo)
- Walidacja danych przed wysyłką

### 🚀 **Integracja z KSeF**
- Wysyłka faktur do testowego i produkcyjnego środowiska KSeF
- Monitorowanie statusu przetwarzania faktur
- Pobieranie numerów referencyjnych KSeF
- Generowanie linków do weryfikacji faktur

### 📊 **Historia faktur**
- Przeglądanie wysłanych faktur
- Sprawdzanie statusów w czasie rzeczywistym
- Przechowywanie XML-i wysłanych dokumentów

## 🏗️ Architektura aplikacji

### **Technologie**
- **Framework**: .NET MAUI (Multi-platform App UI)
- **Wersja .NET**: 8.0
- **Baza danych**: SQLite z sqlite-net-pcl
- **Wzorce**: MVVM (Model-View-ViewModel)
- **DI**: Wbudowany Dependency Injection
- **UI**: XAML z Material Design
- **Messaging**: CommunityToolkit.Mvvm

### **Wzorzec MVVM**
Aplikacja wykorzystuje wzorzec MVVM z:
- **Models**: Klasy reprezentujące dane (BaseFaktura, ClientEntities, Product, MyBusinessEntities)
- **Views**: Widoki XAML definiujące interfejs użytkownika
- **ViewModels**: Logika prezentacji i wiązanie danych z obsługą komunikatów
- **Services**: Warstwa usług (LocalDbService, XmlCreationService, KsefApiService)

### **Baza danych**
- **SQLite**: Lokalna baza danych na urządzeniu
- **sqlite-net-pcl**: ORM do zarządzania danymi
- **Automatyczne migracje**: Tworzenie i aktualizacja schematu
- **Seed Data**: Wypełnianie przykładowymi danymi w trybie DEBUG

## 🚀 Rozpoczęcie pracy

**Dla użytkowników:**
- Telefon z systemem Android 5.0+ (API 21+)
- Minimum 50 MB wolnego miejsca
- Połączenie internetowe (do wysyłki faktur)

1. **Środowisko testowe** (domyślne):
   - URL: `https://ksef-test.mf.gov.pl`
   - Klucz publiczny: automatycznie konfigurowany
   - Wymagany token autoryzacyjny

2. **Środowisko produkcyjne** (wymagana zmiana w kodzie):
   - URL: `https://ksef.mf.gov.pl`
   - Klucz publiczny produkcyjny
   - Ważny token produkcyjny

## 📱 Instrukcja użytkowania

### **Pierwsze uruchomienie**
1. Przy pierwszym uruchomieniu aplikacja utworzy lokalną bazę danych
2. W trybie DEBUG zostaną załadowane przykładowe dane
3. Należy skonfigurować dane własnej firmy w pierwszej kolejności

### **Dodawanie firmy**
1. Przejdź do sekcji "Moja firma"
2. Naciśnij "Dodaj firmę"
3. Wypełnij wszystkie wymagane pola:
   - NIP (format: 1234567890)
   - Nazwa pełna
   - Adres (ulica, numer, kod pocztowy, miejscowość)
   - Dane kontaktowe (telefon, email)
   - Token KSeF
4. Zapisz dane

![Dodanie własnej firmy](https://github.com/user-attachments/assets/1ce7d2ae-a189-483b-b9db-df3fe2a75e78)

### **Zarządzanie klientami**
1. Przejdź do sekcji "Klienci"
2. Naciśnij "Dodaj klienta"
3. Wypełnij dane klienta:
   - Typ: osoba fizyczna lub podmiot prawny
   - NIP (opcjonalny dla osób fizycznych)
   - Dane osobowe/firmowe
   - Adres i kontakt
4. Zapisz klienta

![Dodanie klientów](https://github.com/user-attachments/assets/a855d926-d4c4-409f-8ec0-d2f97ad364ce)

### **Dodawanie produktów/usług**
1. Przejdź do sekcji "Produkty"
2. Naciśnij "Dodaj produkt"
3. Wprowadź dane:
   - Nazwa i opis
   - Cena netto
   - Stawka VAT
   - Rodzaj (towar/usługa)
   - Jednostka miary
4. Zapisz produkt

![Dodanie produktów](https://github.com/user-attachments/assets/b0c6e209-a642-4a5a-b66e-794ada5591fe)

### **Tworzenie i wysyłka faktury**
1. Przejdź do sekcji "Nowa faktura"
2. Wybierz klienta z listy
3. Dodaj pozycje faktury:
   - Wybierz produkt z listy
   - Ustaw ilość przyciskami +/-
   - Sprawdź automatycznie obliczone wartości
   - Dodaj kolejne pozycje przyciskiem "+"
4. Sprawdź podsumowanie
5. Naciśnij "Wyślij do KSeF"

![Wysyłka faktury](https://github.com/user-attachments/assets/6a37f28a-296f-4754-afa9-85748340df36)

### **Weryfikacja wysłanych faktur**
1. W aplikacji: sekcja "Historia faktur"
2. Online: 
   - Wejdź na [ksef-test.mf.gov.pl](https://ksef-test.mf.gov.pl/)
   - Zaloguj się przy użyciu NIP
   - Przejdź do "Lista faktur" lub "Historia sesji"

![Podgląd w systemie KSeF](https://github.com/user-attachments/assets/2e85676f-e2cb-4a33-b084-eb6d96d0e6a9)

## 📦 Główne pakiety NuGet
<PackageReference Include="CommunityToolkit.Mvvm" Version="8.2.2" />
<PackageReference Include="Microsoft.Maui.Controls" Version="8.0.90" />
<PackageReference Include="Microsoft.Maui.Controls.Compatibility" Version="8.0.90" />
<PackageReference Include="Microsoft.Extensions.Logging.Debug" Version="8.0.0" />
<PackageReference Include="sqlite-net-pcl" Version="1.9.172" />
<PackageReference Include="SQLitePCLRaw.bundle_green" Version="2.1.10" />
## 🔧 Konfiguracja środowisk

### **Środowisko testowe** (domyślne)ksefApiUrl = "https://ksef-test.mf.gov.pl";
ksefApiKey = "-----BEGIN PUBLIC KEY-----\r\n[TEST_KEY]\r\n-----END PUBLIC KEY-----";
### **Środowisko produkcyjne**ksefApiUrl = "https://ksef.mf.gov.pl";
ksefApiKey = "-----BEGIN PUBLIC KEY-----\r\n[PRODUCTION_KEY]\r\n-----END PUBLIC KEY-----";
## 📊 Statusy faktur KSeF

Aplikacja obsługuje wszystkie statusy określone w [specyfikacji KSeF](https://www.podatki.gov.pl/media/9876/specyfikacja-oprogramowania-interfejsowego.pdf):

- **200-299**: Faktura zaakceptowana
- **300-399**: Faktura w trakcie przetwarzania  
- **400+**: Błędy walidacji lub przetwarzania

![Statusy KSeF](https://github.com/user-attachments/assets/9d3dafea-328c-41f2-9a9d-36153653ece2)

## 🛠️ Rozwój i wsparcie

### **Plany rozwoju**
- [ ] Obsługa faktur korygujących
- [ ] Export do PDF
- [ ] Backup danych do chmury
- [ ] Obsługa wielu firm
- [ ] Wersja iOS

### **Zgłaszanie błędów**
Jeśli napotkasz problemy, utwórz issue w repozytorium GitHub z:
- Opisem problemu
- Krokami do reprodukcji
- Logami błędów (jeśli dostępne)
- Informacjami o środowisku

## 🔗 Przydatne linki

- [Krajowy System e-Faktur (KSeF)](https://www.podatki.gov.pl/ksef/)
- [Środowisko testowe KSeF](https://ksef-test.mf.gov.pl/)
- [Specyfikacja API KSeF](https://www.podatki.gov.pl/media/9876/specyfikacja-oprogramowania-interfejsowego.pdf)
- [Dokumentacja .NET MAUI](https://docs.microsoft.com/dotnet/maui/)

---

**⚠️ DISCLAIMER**: Aplikacja została stworzona w celach edukacyjnych. Przed użyciem w środowisku produkcyjnym zaleca się dokładne przetestowanie i konsultację z doradcą podatkowym.

# English 🇬🇧

## 📋 Project Description

**KSeF Invoice App** is a comprehensive mobile application built using .NET MAUI technology that enables entrepreneurs and companies to:
- 🏢 Manage their own company data (or multiple companies)
- 👥 Maintain a customer database
- 📦 Catalog products and services
- 📄 Create and send electronic invoices to the KSeF system
- 📊 Monitor the status of sent invoices
- 🔄 Integrate with both the test and production KSeF environments

**Created by Przemysław Przybyszewski** – Engineering thesis project

## ⚠️ Legal Disclaimer

**WARNING**: All data entered into the application **must** be correct. Using invalid data may result in errors and (when using the production KSeF services) legal consequences under Polish tax law.

## ✨ Main Features

### 🏢 Company Management
- Add and edit your company's details
- Store KSeF authorization tokens
- Configure contact and address data

### 👥 Customer Database
- Add new customers (individuals and legal entities)
- Edit customer data
- Manage contact and address information
- Delete unnecessary records

### 📦 Product/Service Catalog
- Create a database of products and services
- Define prices and VAT rates
- Categorize by type (product/service)
- Set units of measure

### 📄 Invoice Creation
- Intuitive invoice wizard with dynamic item addition
- Automatic calculation of net, gross, and VAT values
- Support for various tax rates (23%, 8%, 5%, 0%, zw, np, oo)
- Data validation before sending

### 🚀 KSeF Integration
- Send invoices to KSeF test and production environments
- Monitor processing status
- Retrieve KSeF reference numbers
- Generate verification links for invoices

### 📊 Invoice History
- View sent invoices
- Check real-time statuses
- Store XMLs of sent documents

## 🏗️ Application Architecture

### Technologies
- Framework: .NET MAUI (Multi-platform App UI)
- .NET Version: 8.0
- Database: SQLite with sqlite-net-pcl
- Pattern: MVVM (Model-View-ViewModel)
- DI: Built-in Dependency Injection
- UI: XAML with Material Design
- Messaging: CommunityToolkit.Mvvm

### MVVM Pattern
The app uses MVVM with:
- Models: Data classes (e.g., BaseFaktura, ClientEntities, Product, MyBusinessEntities)
- Views: XAML views defining the UI
- ViewModels: Presentation logic and data binding with message handling
- Services: Logic layer (e.g., LocalDbService, XmlCreationService, KsefApiService)

### Database
- SQLite: Local on-device database
- sqlite-net-pcl: ORM for data management
- Auto migrations: Schema creation and updates
- Seed Data: Populated with sample data in DEBUG mode

## 🚀 Getting Started

### System Requirements

**For Users:**
- Android phone with Android 5.0+ (API 21+)
- Minimum 50 MB free space
- Internet connection (for invoice sending)

### KSeF Environment Configuration

1. Test environment (default):
   - URL: https://ksef-test.mf.gov.pl
   - Public key: auto-configured
   - Requires authorization token

2. Production environment (requires code change):
   - URL: https://ksef.mf.gov.pl
   - Production public key
   - Valid production token

## 📦 Key NuGet Packages

<PackageReference Include="CommunityToolkit.Mvvm" Version="8.2.2" />
<PackageReference Include="Microsoft.Maui.Controls" Version="8.0.90" />
<PackageReference Include="Microsoft.Maui.Controls.Compatibility" Version="8.0.90" />
<PackageReference Include="Microsoft.Extensions.Logging.Debug" Version="8.0.0" />
<PackageReference Include="sqlite-net-pcl" Version="1.9.172" />
<PackageReference Include="SQLitePCLRaw.bundle_green" Version="2.1.10" />

## 🔧 Environment Configuration

ksefApiUrl = "https://ksef-test.mf.gov.pl";  
ksefApiKey = "-----BEGIN PUBLIC KEY-----\\n[TEST_KEY]\\n-----END PUBLIC KEY-----";

ksefApiUrl = "https://ksef.mf.gov.pl";  
ksefApiKey = "-----BEGIN PUBLIC KEY-----\\n[PRODUCTION_KEY]\\n-----END PUBLIC KEY-----";

## 📊 KSeF Invoice Statuses

- 200–299: Invoice accepted  
- 300–399: Invoice in processing  
- 400+: Validation or processing errors

## ⚡ Advanced Features

- Numeric Control with validation and responsive design
- Dynamic item addition with auto-calculations
- Error handling with validation and logging

## 🛠️ Development & Support

Planned Features:
- [ ] Corrective invoice support
- [ ] PDF export
- [ ] Cloud data backup
- [ ] Multi-company support
- [ ] iOS version

Report issues on GitHub with steps to reproduce and error logs.

## 🔗 Useful Links

- https://www.podatki.gov.pl/ksef/
- https://ksef-test.mf.gov.pl/
- https://www.podatki.gov.pl/media/9876/specyfikacja-oprogramowania-interfejsowego.pdf
- https://docs.microsoft.com/dotnet/maui/

DISCLAIMER: This app is for educational purposes. Always test before production use.

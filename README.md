# 📱 KSeF Invoice App

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

### **Struktura projektu**KseF/
├── Models/                  # Modele danych i ViewModels
│   ├── BusinessEntities.cs  # Encje firm i klientów
│   ├── BaseFaktura.cs      # Model faktury
│   ├── Product.cs          # Model produktu
│   └── ViewModels/         # ViewModels dla MVVM
├── Pages/                  # Strony XAML
│   ├── MainPage.xaml       # Strona główna
│   ├── SendInvoiceToKsef.xaml # Kreator faktury
│   ├── MyClientsPage.xaml  # Zarządzanie klientami
│   └── MyProductsPage.xaml # Zarządzanie produktami
├── Services/               # Usługi aplikacji
│   ├── LocalDbService.cs   # Obsługa bazy danych
│   ├── XmlCreationService.cs # Tworzenie XML faktury
│   └── KsefApiService.cs   # Komunikacja z API KSeF
└── Controls/               # Własne kontrolki
    └── KseFNumericUpDown.cs # Kontrolka numeryczna
### **Baza danych**
- **SQLite**: Lokalna baza danych na urządzeniu
- **sqlite-net-pcl**: ORM do zarządzania danymi
- **Automatyczne migracje**: Tworzenie i aktualizacja schematu
- **Seed Data**: Wypełnianie przykładowymi danymi w trybie DEBUG

## 🚀 Rozpoczęcie pracy

### **Wymagania systemowe**

**Dla deweloperów:**
- Visual Studio 2022 (17.4+)
- .NET 8.0 SDK
- .NET MAUI Workload
- Android SDK (API 21+)
- Procesor z obsługą Hyper-V (dla emulatora)

**Dla użytkowników:**
- Telefon z systemem Android 5.0+ (API 21+)
- Minimum 50 MB wolnego miejsca
- Połączenie internetowe (do wysyłki faktur)

### **Instalacja deweloperska**

1. **Sklonuj repozytorium**git clone https://github.com/[twoja-nazwa]/KseFv2.git
cd KseFv2
2. **Przywróć pakiety NuGet**dotnet restore
3. **Zbuduj projekt**dotnet build
4. **Uruchom aplikację**# Android
dotnet run --framework net8.0-android

# Windows (jeśli dostępne)
dotnet run --framework net8.0-windows10.0.19041.0
### **Konfiguracja środowiska KSeF**

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

## ⚡ Funkcje zaawansowane

### **Kontrolka numeryczna**
- Własna kontrolka `KseFNumericUpDown` do precyzyjnego wprowadzania ilości
- Walidacja minimalnej wartości (≥1)
- Responsive design dostosowany do różnych rozmiarów ekranów

### **Dynamiczne dodawanie pozycji**
- Nieograniczona liczba pozycji na fakturze
- Przycisk usuwania dla każdej pozycji (oprócz pierwszej)
- Automatyczne przeliczanie sum

### **Obsługa błędów**
- Walidacja danych przed wysyłką
- Przyjazne komunikaty błędów
- Logowanie problemów komunikacji z API

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

## 📄 Licencja

Projekt został stworzony jako praca inżynierska przez Przemysława Przybyszewskiego.

## 🔗 Przydatne linki

- [Krajowy System e-Faktur (KSeF)](https://www.podatki.gov.pl/ksef/)
- [Środowisko testowe KSeF](https://ksef-test.mf.gov.pl/)
- [Specyfikacja API KSeF](https://www.podatki.gov.pl/media/9876/specyfikacja-oprogramowania-interfejsowego.pdf)
- [Dokumentacja .NET MAUI](https://docs.microsoft.com/dotnet/maui/)

---

**⚠️ DISCLAIMER**: Aplikacja została stworzona w celach edukacyjnych. Przed użyciem w środowisku produkcyjnym zaleca się dokładne przetestowanie i konsultację z doradcą podatkowym.


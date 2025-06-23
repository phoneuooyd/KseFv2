# Aplikacja MAUI do wysyłek faktur KSeF
## Autorstwa Przemysława Przybyszewskiego

### 1. Założenia projektowe

* Aplikacja pozwala z urządzeń mobilnych na:
  - Wprowadzenie danych własnej firmy (firm)
  - Wprowadzenie danych klientów
  - Wprowadzenie listy produktów/usług
* Faktury są tworzone na podstawie danych wprowadzonych do bazy danych. Dane **muszą** być prawidłowe. Użycie nieprawidłowych danych może prowadzić do
  błędów oraz (używając serwisów produkcyjnych KSeF) konsekwencji prawnych
* Aplikacja pozwala na wysyłkę wsadową pojedynczej faktury oraz zobaczenie jej statusu. Statusy możemy sprawdzić tutaj : https://www.podatki.gov.pl/media/9876/specyfikacja-oprogramowania-interfejsowego.pdf - strona 19.
![obraz](https://github.com/user-attachments/assets/9d3dafea-328c-41f2-9a9d-36153653ece2)

### 2. Wymagania systemowe

* Dla deweloperów:
  * Wymagane jest Visual Studio 2022, .NET 8.0, .NET MAUI, Baza danych SQLite, w celach emulacji wymagany jest również procesor wykorzystujący Hyper-V.

    ![obraz](https://github.com/user-attachments/assets/88b7472e-d37a-49a1-b7d6-59aff77eac4e)
  
* Dla użytkowników:
  * Telefon komórkowy z Android z wersją conajmniej 10.

### 3. Instrukcja obsługi

 * Rozpoczęcie pracy:
    * Dodanie własnej firmy:
      ![obraz](https://github.com/user-attachments/assets/1ce7d2ae-a189-483b-b9db-df3fe2a75e78)

    * Dodanie klientów:
      ![obraz](https://github.com/user-attachments/assets/a855d926-d4c4-409f-8ec0-d2f97ad364ce)

    * Dodanie produktów:
      ![obraz](https://github.com/user-attachments/assets/b0c6e209-a642-4a5a-b66e-794ada5591fe)

 * Wysyłka faktury:
    ![obraz](https://github.com/user-attachments/assets/6a37f28a-296f-4754-afa9-85748340df36)



### 4. Podgląd wysłanej faktury 

* Nalezy wejść na stronę testowego środowiska KSeF oraz się zalogować za pomocą NIPu użytego do wysyłki faktury (https://ksef-test.mf.gov.pl/)
* Następnie przejść do panelu Lista faktur lub Historia sesji aby zobaczyć wysłane za pomocą aplikacji faktury.  

    ![obraz](https://github.com/user-attachments/assets/2e85676f-e2cb-4a33-b084-eb6d96d0e6a9)

    ![obraz](https://github.com/user-attachments/assets/25652e9b-678f-459c-867b-e4606d914830)


  


# Dokumentacja projektu: System Wysyłki Emaili

## Spis treści

1. [Opis](#opis)
2. [Technologia](#technologia)
3. [Zastosowane paradygmaty](#zastosowane-paradygmaty)
4. [Wymagania do uruchomienia](#wymagania-do-uruchomienia)
5. [Sposób uruchomienia](#sposób-uruchomienia)

---

## Opis

Projekt jest systemem do asynchronicznej wysyłki emaili z wykorzystaniem wzorca Outbox. Kluczowe elementy:

- **Endpoint do zakolejkowania wysyłania email**:
  Umożliwia dodanie wiadomości email do kolejki wysyłki.

- **Asynchroniczne wysyłanie emaili**:
  Realizowane przez proces Background Job, który przetwarza dane z kolejki i wysyła wiadomości email za pomocą zamockowanego `EmailSender`.

- **Wykorzystanie wzorca Outbox**:
  Zapewnia niezawodność przetwarzania i oddziela logikę tworzenia emaili od ich wysyłki.

- **Baza danych w pamięci**:
  Dane przechowywane są w `ConcurrentDictionary`, co umożliwia szybki dostęp i testowanie bez trwałego przechowywania danych.

- **Endpoint przeglądania wysłanych emaili**:
  Umożliwia filtrowanie wiadomości według statusu za pomocą parametrów zapytania (query params).

---

## Technologia

- **Platforma**: .NET 8
- **Język programowania**: C#

---

## Zastosowane paradygmaty

1. **Obiektowy**:
   Struktura kodu opiera się na klasach i obiektach reprezentujących wiadomości email, procesy, oraz usługi.

2. **Funkcyjny (LINQ)**:
   Operacje na danych, takie jak filtrowanie i transformacje, realizowane są za pomocą wyrażeń LINQ.

3. **Deklaratywny**:
   Definicje przepływu pracy i przetwarzania danych wyrażane są w sposób opisowy, np. w konfiguracjach i implementacji wzorca Outbox.

---

## Wymagania do uruchomienia

1. Przeglądarka internetowa do dostępu do interfejsu API (opcjonalne).
2. Zainstalowane środowisko uruchomieniowe **.NET 8**.

---

## Sposób uruchomienia

1. Sklonuj repozytorium projektu:
   git clone [https://github.com/KopfSzmercen/college-project.git]
2. Przejdź do katalogu z projektem:
   cd college-project
3. Uruchom aplikację:
   dotnet run

using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.Maui.Controls;

namespace KseF
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        bool isOnMainPage = false;

        public override void OnBackPressed()
        {
            // Sprawdź, czy aktualny page to MainPage
            if (Shell.Current?.CurrentPage is Pages.MainPage)
            {
                // Jeśli jesteśmy na MainPage, pytamy użytkownika o wyjście
                if (!isOnMainPage)
                {
                    isOnMainPage = true;
                    ShowExitConfirmation();
                }
                else
                {
                    base.OnBackPressed(); // Użytkownik chce wyjść
                }
            }
            else
            {
                // Jeśli nie jesteśmy na MainPage, cofamy się do poprzedniej strony
                if (Shell.Current.Navigation.NavigationStack.Count > 1)
                {
                    Shell.Current.Navigation.PopAsync(); // Cofnięcie strony
                }
                else
                {
                    Shell.Current.GoToAsync("//MainPage"); // Powrót do MainPage
                }
            }
        }

        private async void ShowExitConfirmation()
        {
            // Wyświetlamy komunikat o wyjściu z aplikacji
            bool exit = await Shell.Current.DisplayAlert("Wyjście", "Czy na pewno chcesz wyjść z aplikacji?", "Tak", "Nie");
            if (exit)
            {
                FinishAffinity(); // Zamyka aplikację
            }
            else
            {
                isOnMainPage = false; // Jeśli nie chce wyjść, resetujemy flagę
            }
        }
    }
}

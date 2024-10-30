using KseF.Pages;
using KseF.Services;
namespace KseF
{
    public partial class App : Application
    {
		public App(AppShell appShell)
        {
            UserAppTheme = Microsoft.Maui.ApplicationModel.AppTheme.Light;
            InitializeComponent();
            MainPage = appShell;
        }

    }
}

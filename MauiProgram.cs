using KseF.Pages;
using KseF.Services;
using KseF.Interfaces;
using Microsoft.Extensions.Logging;

namespace KseF
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

			builder.Services.AddSingleton<ILocalDbService, LocalDbService>();
			builder.Services.AddSingleton<AppShell>(); 
            builder.Services.AddTransient<MainPage>();
			builder.Services.AddTransient<MyClientsPage>();
            builder.Services.AddTransient<MyBusinessEntitiesPage>();
            builder.Services.AddTransient<MyProductsPage>();
            builder.Services.AddTransient<InvoicesSent>();
			builder.Services.AddTransient<SendInvoiceToKsef>();
			builder.Services.AddTransient<AddClientEntityPage>();
			builder.Services.AddTransient<AddProductPage>();
			builder.Services.AddTransient<AddMyBusinessEntityPage>();



#if DEBUG
			builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

using Microsoft.Extensions.Logging;
using MiAgendaUTN.Services;
using MiAgendaUTN.ViewModels;
using MiAgendaUTN.Views;

namespace MiAgendaUTN;

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

        // Inicializar SQLite
        SQLitePCL.Batteries_V2.Init();

        // Servicios
        builder.Services.AddSingleton<DatabaseService>();
        builder.Services.AddSingleton<JsonService>();

        // ViewModels
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<ListaTareasViewModel>();
        builder.Services.AddTransient<CrearEditarTareaViewModel>();

        // Views
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<ListaTareasPage>();
        builder.Services.AddTransient<CrearEditarTareaPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}

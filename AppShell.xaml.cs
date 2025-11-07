namespace MiAgendaUTN;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Registrar rutas de navegación
        Routing.RegisterRoute(nameof(Views.CrearEditarTareaPage), typeof(Views.CrearEditarTareaPage));
    }
}

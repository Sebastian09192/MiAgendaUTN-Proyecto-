using System.Windows.Input;
using Microsoft.Maui.ApplicationModel;

namespace MiAgendaUTN.ViewModels
{
    public class AcercaDeViewModel : BaseViewModel
    {
        public string NombreApp => "Mi Agenda UTN";
        public string Version => "1.0";
        public string Autor => "Desarrollado por Sebastián - UTN";

        public ICommand AbrirSitioCommand { get; }

        public AcercaDeViewModel()
        {
            AbrirSitioCommand = new Command(async () =>
            {
                var url = "https://github.com/tu-repo"; // Puedes personalizarlo
                await Browser.OpenAsync(url);
            });
        }
    }
}

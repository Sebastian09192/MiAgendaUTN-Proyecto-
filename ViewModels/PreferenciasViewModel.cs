using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace MiAgendaUTN.ViewModels
{
    public partial class PreferenciasViewModel : BaseViewModel
    {
        [ObservableProperty]
        private bool temaOscuro;

        [ObservableProperty]
        private string nombreUsuario;

        public PreferenciasViewModel()
        {
            Title = "Preferencias";
            CargarPreferencias();
        }

        private void CargarPreferencias()
        {
            TemaOscuro = Preferences.Default.Get("TemaOscuro", false);
            NombreUsuario = Preferences.Default.Get("NombreUsuario", "Usuario UTN");
        }

        [RelayCommand]
        private async Task GuardarPreferenciasAsync()
        {
            Preferences.Default.Set("TemaOscuro", TemaOscuro);
            Preferences.Default.Set("NombreUsuario", NombreUsuario);

            await Application.Current.MainPage.DisplayAlert("Guardado", "Preferencias actualizadas", "OK");
        }
    }
}

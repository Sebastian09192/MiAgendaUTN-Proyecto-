using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MiAgendaUTN.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiAgendaUTN.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private int totalTareas;

        [ObservableProperty]
        private int tareasCompletadas;

        [ObservableProperty]
        private int tareasPendientes;

        public MainViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            Title = "Mi Agenda UTN";
        }

        public override async void OnAppearing()
        {
            await _databaseService.InicializarAsync();
            await ActualizarEstadisticasAsync();
        }

        [RelayCommand]
        private async Task IrAListaTareasAsync()
        {
            await Shell.Current.GoToAsync("//ListaTareasPage");
        }

        [RelayCommand]
        private async Task ActualizarEstadisticasAsync()
        {
            try
            {
                SetBusy(true);

                var todas = await _databaseService.ObtenerTareasAsync();
                TotalTareas = todas.Count;
                TareasCompletadas = todas.FindAll(t => t.EstaCompletada).Count;
                TareasPendientes = todas.FindAll(t => !t.EstaCompletada).Count;

                LimpiarMensaje();
            }
            catch (System.Exception ex)
            {
                ManejarExcepcion(ex, "cargar estadísticas");
            }
            finally
            {
                SetBusy(false);
            }
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MiAgendaUTN.Models;
using MiAgendaUTN.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MiAgendaUTN.ViewModels
{
    public partial class ListaTareasViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private ObservableCollection<Tarea> tareas;

        public ListaTareasViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            Title = "Lista de Tareas";
            Tareas = new ObservableCollection<Tarea>();
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            _ = CargarTareasAsync();
        }

        [RelayCommand]
        private async Task CargarTareasAsync()
        {
            if (IsBusy) return;

            try
            {
                SetBusy(true);
                var listaTareas = await _databaseService.ObtenerTareasAsync();

                Tareas.Clear();
                foreach (var tarea in listaTareas)
                {
                    Tareas.Add(tarea);
                }

                LimpiarMensaje();
            }
            catch (System.Exception ex)
            {
                ManejarExcepcion(ex, "cargar tareas");
            }
            finally
            {
                SetBusy(false);
            }
        }

        [RelayCommand]
        private async Task CrearTareaAsync()
        {
            await Shell.Current.GoToAsync(nameof(Views.CrearEditarTareaPage));
        }

        [RelayCommand]
        private async Task EditarTareaAsync(Tarea tarea)
        {
            if (tarea == null) return;
            await Shell.Current.GoToAsync($"{nameof(Views.CrearEditarTareaPage)}?TareaId={tarea.Id}");
        }

        [RelayCommand]
        private async Task EliminarTareaAsync(Tarea tarea)
        {
            if (tarea == null) return;

            var page = Application.Current?.Windows[0]?.Page;
            if (page == null) return;

            bool confirmar = await page.DisplayAlert(
                "Confirmar",
                $"¿Eliminar la tarea '{tarea.Titulo}'?",
                "Sí",
                "No");

            if (!confirmar) return;

            try
            {
                SetBusy(true);
                await _databaseService.EliminarTareaAsync(tarea);
                await CargarTareasAsync();
                MostrarMensaje("Tarea eliminada correctamente");
            }
            catch (System.Exception ex)
            {
                ManejarExcepcion(ex, "eliminar tarea");
            }
            finally
            {
                SetBusy(false);
            }
        }

        [RelayCommand]
        private async Task CambiarEstadoAsync(Tarea tarea)
        {
            if (tarea == null) return;

            try
            {
                tarea.EstaCompletada = !tarea.EstaCompletada;
                await _databaseService.GuardarTareaAsync(tarea);
                await CargarTareasAsync();
            }
            catch (System.Exception ex)
            {
                ManejarExcepcion(ex, "cambiar estado");
            }
        }
    }
}
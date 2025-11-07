using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MiAgendaUTN.Models;
using MiAgendaUTN.Services;
using System;
using System.Threading.Tasks;

namespace MiAgendaUTN.ViewModels
{
    [QueryProperty(nameof(TareaId), "TareaId")]
    public partial class DetalleTareaViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private Tarea tarea;

        private int _tareaId;
        public int TareaId
        {
            get => _tareaId;
            set
            {
                _tareaId = value;
                _ = CargarTareaAsync();
            }
        }

        public DetalleTareaViewModel()
        {
            _databaseService = new DatabaseService();
            Title = "Detalle de Tarea";
        }

        private async Task CargarTareaAsync()
        {
            try
            {
                SetBusy(true);
                var tarea = await _databaseService.ObtenerTareaPorIdAsync(TareaId);
                Tarea = tarea ?? new Tarea { Titulo = "No encontrada" };
            }
            catch (Exception ex)
            {
                ManejarExcepcion(ex, "Error al cargar detalle");
            }
            finally
            {
                SetBusy(false);
            }
        }

        [RelayCommand]
        private async Task EliminarTareaAsync()
        {
            bool confirmar = await Application.Current.MainPage.DisplayAlert("Confirmar", "¿Desea eliminar esta tarea?", "Sí", "No");
            if (!confirmar) return;

            await _databaseService.EliminarTareaAsync(Tarea);
            await Shell.Current.GoToAsync("..");
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MiAgendaUTN.Models;
using MiAgendaUTN.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MiAgendaUTN.ViewModels
{
    [QueryProperty(nameof(TareaId), "TareaId")]
    public partial class CrearEditarTareaViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private int tareaId;

        [ObservableProperty]
        private string titulo = string.Empty;

        [ObservableProperty]
        private string descripcion = string.Empty;

        [ObservableProperty]
        private DateTime fechaVencimiento = DateTime.Now.AddDays(7);

        [ObservableProperty]
        private string categoriaSeleccionada = "Tarea";

        [ObservableProperty]
        private string prioridadSeleccionada = "Media";

        [ObservableProperty]
        private bool estaCompletada;

        [ObservableProperty]
        private bool esModoEdicion;

        public ObservableCollection<string> Categorias { get; } = new()
        {
            "Tarea", "Examen", "Proyecto", "Reunión"
        };

        public ObservableCollection<string> Prioridades { get; } = new()
        {
            "Baja", "Media", "Alta"
        };

        public DateTime FechaMinima => DateTime.Today;

        public CrearEditarTareaViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            Title = "Nueva Tarea";
        }

        partial void OnTareaIdChanged(int value)
        {
            if (value > 0)
            {
                EsModoEdicion = true;
                Title = "Editar Tarea";
                _ = CargarTareaAsync();
            }
        }

        private async Task CargarTareaAsync()
        {
            try
            {
                var tarea = await _databaseService.ObtenerTareaPorIdAsync(TareaId);
                if (tarea != null)
                {
                    Titulo = tarea.Titulo;
                    Descripcion = tarea.Descripcion ?? string.Empty;
                    FechaVencimiento = tarea.FechaVencimiento;
                    CategoriaSeleccionada = tarea.Categoria;
                    PrioridadSeleccionada = tarea.Prioridad;
                    EstaCompletada = tarea.EstaCompletada;
                }
            }
            catch (Exception ex)
            {
                ManejarExcepcion(ex, "cargar tarea");
            }
        }

        [RelayCommand]
        private async Task GuardarAsync()
        {
            try
            {
                SetBusy(true);

                var tarea = new Tarea
                {
                    Id = TareaId,
                    Titulo = Titulo,
                    Descripcion = Descripcion,
                    FechaVencimiento = FechaVencimiento,
                    Categoria = CategoriaSeleccionada,
                    Prioridad = PrioridadSeleccionada,
                    EstaCompletada = EstaCompletada,
                    FechaCreacion = DateTime.Now
                };

                var (esValida, mensaje) = tarea.Validar();
                if (!esValida)
                {
                    MostrarMensaje(mensaje, true);
                    return;
                }

                await _databaseService.GuardarTareaAsync(tarea);

                // Mostrar mensaje de éxito
                var page = Application.Current?.Windows[0]?.Page;
                if (page != null)
                {
                    await page.DisplayAlert("Éxito", "Tarea guardada correctamente", "OK");
                }

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                ManejarExcepcion(ex, "guardar tarea");
            }
            finally
            {
                SetBusy(false);
            }
        }

        [RelayCommand]
        private async Task CancelarAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
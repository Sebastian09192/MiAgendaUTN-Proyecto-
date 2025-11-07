using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace MiAgendaUTN.ViewModels
{
    /// <summary>
    /// Clase base para todos los ViewModels
    /// </summary>
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string title = string.Empty;

        [ObservableProperty]
        private string mensaje = string.Empty;

        [ObservableProperty]
        private bool tieneError;

        /// <summary>
        /// Marca el estado de carga
        /// </summary>
        protected void SetBusy(bool value)
        {
            IsBusy = value;
        }

        /// <summary>
        /// Limpia el mensaje mostrado
        /// </summary>
        protected void LimpiarMensaje()
        {
            Mensaje = string.Empty;
            TieneError = false;
        }

        /// <summary>
        /// Muestra un mensaje (de error o informativo)
        /// </summary>
        protected void MostrarMensaje(string texto, bool esError = false)
        {
            Mensaje = texto;
            TieneError = esError;
        }

        /// <summary>
        /// Maneja excepciones con mensaje amigable
        /// </summary>
        protected void ManejarExcepcion(Exception ex, string contexto = "")
        {
            Console.WriteLine($"⚠️ Error en {contexto}: {ex.Message}");
            MostrarMensaje($"Ocurrió un error: {ex.Message}", true);
        }

        /// <summary>
        /// Método virtual para cuando la vista aparece
        /// </summary>
        public virtual void OnAppearing() { }
    }
}
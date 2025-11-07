using SQLite;
using System;

namespace MiAgendaUTN.Models
{
    /// <summary>
    /// Modelo de datos para una Tarea
    /// </summary>
    public class Tarea
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Titulo { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public DateTime FechaVencimiento { get; set; } = DateTime.Now.AddDays(7);

        public string Categoria { get; set; } = "Tarea";

        public bool EstaCompletada { get; set; } = false;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public string Prioridad { get; set; } = "Media";

        /// <summary>
        /// Valida que la tarea tenga los datos correctos
        /// </summary>
        public (bool esValida, string mensaje) Validar()
        {
            if (string.IsNullOrWhiteSpace(Titulo))
                return (false, "El título es obligatorio");

            if (Titulo.Length > 100)
                return (false, "El título es muy largo");

            if (FechaVencimiento < DateTime.Today)
                return (false, "La fecha no puede ser en el pasado");

            return (true, string.Empty);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using MiAgendaUTN.Models;

namespace MiAgendaUTN.Services
{
    /// <summary>
    /// Servicio para manejo de sincronización JSON (exportar/importar)
    /// </summary>
    public class JsonService
    {
        private readonly string _jsonPath;

        public JsonService()
        {
            _jsonPath = Path.Combine(FileSystem.AppDataDirectory, "tareas.json");
        }

        public async Task ExportarAsync(DatabaseService database)
        {
            var tareas = await database.ObtenerTareasAsync();
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(tareas, opciones);
            await File.WriteAllTextAsync(_jsonPath, json);
        }

        public async Task ImportarAsync(DatabaseService database)
        {
            if (!File.Exists(_jsonPath)) return;

            var json = await File.ReadAllTextAsync(_jsonPath);
            var tareas = JsonSerializer.Deserialize<List<Tarea>>(json);

            if (tareas != null)
            {
                foreach (var tarea in tareas)
                {
                    await database.GuardarTareaAsync(tarea);
                }
            }
        }

        public async Task SincronizarDesdeBaseDatosAsync(DatabaseService database)
        {
            await ExportarAsync(database);
        }

        public async Task<List<Tarea>> CargarDesdeJsonAsync()
        {
            if (!File.Exists(_jsonPath))
                return new List<Tarea>();

            var json = await File.ReadAllTextAsync(_jsonPath);
            return JsonSerializer.Deserialize<List<Tarea>>(json) ?? new List<Tarea>();
        }
    }
}

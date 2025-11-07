using SQLite;
using MiAgendaUTN.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MiAgendaUTN.Services
{
    /// <summary>
    /// Servicio para manejar la base de datos SQLite
    /// </summary>
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;
        private static bool _inicializado = false;

        public DatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "MiAgendaUTN.db3");
            _database = new SQLiteAsyncConnection(dbPath);
        }

        /// <summary>
        /// Inicializa la base de datos (crear tabla si no existe)
        /// </summary>
        public async Task InicializarAsync()
        {
            if (!_inicializado)
            {
                await _database.CreateTableAsync<Tarea>();
                _inicializado = true;
            }
        }

        public async Task<int> GuardarTareaAsync(Tarea tarea)
        {
            if (tarea.Id == 0)
                return await _database.InsertAsync(tarea);
            else
                return await _database.UpdateAsync(tarea);
        }

        public async Task<int> EliminarTareaAsync(Tarea tarea)
        {
            return await _database.DeleteAsync(tarea);
        }

        public async Task<Tarea?> ObtenerTareaPorIdAsync(int id)
        {
            return await _database.Table<Tarea>().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Tarea>> ObtenerTareasAsync()
        {
            return await _database.Table<Tarea>()
                                  .OrderByDescending(t => t.FechaCreacion)
                                  .ToListAsync();
        }

        public async Task<List<Tarea>> ObtenerTareasPorEstadoAsync(bool completadas)
        {
            return await _database.Table<Tarea>()
                                  .Where(t => t.EstaCompletada == completadas)
                                  .OrderBy(t => t.FechaVencimiento)
                                  .ToListAsync();
        }

        public async Task<int> LimpiarTareasAsync()
        {
            return await _database.DeleteAllAsync<Tarea>();
        }
    }
}

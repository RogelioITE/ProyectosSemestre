using AgendaPersonal.Modelos;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaPersonal.Datos
{
    public class UsuarioDataBase
    {
        private readonly SQLiteAsyncConnection _dtConnection;
        public UsuarioDataBase(string dbPath)
        {
            _dtConnection = new SQLiteAsyncConnection(dbPath);
            _dtConnection.CreateTableAsync<Usuario>().Wait();
        }
        public Task<int> GuardarUsuarioAsync(Usuario usuario)
        {
            return _dtConnection.InsertAsync(usuario);
        }
        public async Task<Usuario> ObtenerUsuarioPorNombreAsync(string nombreUusuario)
        {       
            return await _dtConnection.Table<Usuario>()
                .Where(u => u.NombreUsuario.ToLower() == nombreUusuario)
                .FirstOrDefaultAsync();
        }
       /* public Task<List<Usuario>> ObtenerTodosLosUsuariosAsync()
        {
            return _dtConnection.Table<Usuario>().ToListAsync();
        }*/
        public Task<int> EliminarUsuarioAsync(Usuario usuario)
        {
            return _dtConnection.DeleteAsync(usuario);
        }
    }
}

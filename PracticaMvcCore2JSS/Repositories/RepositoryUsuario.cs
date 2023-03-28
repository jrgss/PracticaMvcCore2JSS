using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2JSS.Data;
using PracticaMvcCore2JSS.Models;

namespace PracticaMvcCore2JSS.Repositories
{
    public class RepositoryUsuario
    {
        private LibrosContext context;

        public RepositoryUsuario(LibrosContext context)
        {
            this.context = context;
        }

        public async Task<Usuario> FindUsuario(string email)
        {
            Usuario user = new Usuario();
            var consulta = from datos in this.context.Usuarios
                           where datos.Email == email
                           select datos;
            user = await consulta.FirstOrDefaultAsync();
            return user;
        }
        public async Task<Usuario> FindUsuario(int idusuario)
        {
            Usuario user = new Usuario();
            var consulta = from datos in this.context.Usuarios
                           where datos.IdUsuario == idusuario
                           select datos;
            user = await consulta.FirstOrDefaultAsync();
            return user;
        }
        public async Task<List<VistaPedidos>> GetPedidosUser(int idUser)
        {
            var consutla = from datos in this.context.VistaPedidos
                           where datos.IdUsuario == idUser
                           select datos;
            List<VistaPedidos> pedidos = await consutla.ToListAsync();
            return pedidos;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2JSS.Data;
using PracticaMvcCore2JSS.Models;

namespace PracticaMvcCore2JSS.Repositories
{
    public class RepositoryLibros
    {
        private LibrosContext context;
        public RepositoryLibros(LibrosContext context)
        {
            this.context = context;
        }

        public async Task<List<Genero>> GetGenerosAsync(){

            var consulta = from datos in this.context.Generos
                           select datos;
            List<Genero> generos = await consulta.ToListAsync();
            return generos;

        }
        public async Task<List<Libro>> GetLibrosAsync()
        {

            var consulta = from datos in this.context.Libros
                           select datos;
            List<Libro> libros = await consulta.ToListAsync();
            return libros;

        }
        public async Task<List<Libro>> GetLibrosPorGeneroAsync(int idgenero)
        {

            var consulta = from datos in this.context.Libros
                           where datos.IdGenero == idgenero
                           select datos;
            List<Libro> libros = await consulta.ToListAsync();
            return libros;

        }
        public async Task<Libro>GetLibroId(int idlibro)
        {
            var consulta = from datos in this.context.Libros
                           where datos.IdLibro == idlibro
                           select datos;
            Libro libro = await consulta.FirstOrDefaultAsync();
            return libro;
        }

        public async Task HacerPedido(int IdUser, int idFactura, DateTime Fecha, int idLibro, int cantidad)
        {
            int idpedido = GetMaximoIdPedido();
            Pedido pedido = new Pedido();
            pedido.IdPedido = idpedido;
            pedido.Cantidad = cantidad;
            pedido.IdFactura = idFactura;
            pedido.Fecha = Fecha;
            pedido.IdUsuario = IdUser;
            pedido.IdLibro = idLibro;
             this.context.Pedidos.Add(pedido);
            await this.context.SaveChangesAsync();
        }

        public int GetMaximoIdPedido()
        {
            if (this.context.Pedidos.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Pedidos.Max(z => z.IdPedido) + 1;
            }

        }

        public async Task<LibrosPag> GetLibrosPagAsync(int pos,int cantidad) 
        {
            int totalLibros = this.context.Libros.Count();
            List<Libro> misLibros = await GetLibrosAsync();
            List<Libro> misLibrosPagin = misLibros.Skip(pos).Take(cantidad).ToList();

            LibrosPag modelo = new LibrosPag();
            modelo.Total = totalLibros;
            modelo.Libros = misLibrosPagin;
            return modelo;
        }

    }
}

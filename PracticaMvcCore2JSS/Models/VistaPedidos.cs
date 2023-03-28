using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaMvcCore2JSS.Models
{

    public class VistaPedidos
    {
        [Key]
        [Column("IDVISTAPEDIDOS")]
        public long IdVistaPedidos { get; set; }
        [Column("IDUSUARIO")]
        public int IdUsuario { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("APELLIDOS")]
        public string Apellidos { get; set; }
        [Column("TITULO")]
        public string Titulo { get; set; }
        [Column("PRECIO")]
        public int Precio { get; set; }
        [Column("Portada")]
        public string Portada { get; set; }
        [Column("FECHA")]
        public DateTime Fecha { get; set; }
        [Column("PRECIOFINAL")]
        public int PrecioFinal { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaMvcCore2JSS.Models
{
    [Table("Pedidos")]
    public class Pedido
    {
        [Key]
        [Column("IDPEDIDO")]
        public int IdPedido { get; set; }
        [Column("IdFactura")]
        public int IdFactura { get; set; }
        [Column("FECHA")]
        public DateTime Fecha { get; set; }
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }
        [Column("CANTIDAD")]
        public int Cantidad { get;set; }
        [Column("IDLIBRO")]
        public int IdLibro { get;set; }
    }
}

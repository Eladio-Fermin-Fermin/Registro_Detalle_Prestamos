using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Registro_Detalle_Prestamos.Models
{
    public class Moras
    {
        [Key]
        public int MoraId { get; set; }
        public DateTime Fecha { get; set; }
        public double Total { get; set; }

        [ForeignKey("MoraId")]
        public virtual List<MorasDetalles> MorasDetalles { get; set; } = new List<MorasDetalles>();

        public Moras()
        {
            this.Fecha = DateTime.Now;
        }
    }
}

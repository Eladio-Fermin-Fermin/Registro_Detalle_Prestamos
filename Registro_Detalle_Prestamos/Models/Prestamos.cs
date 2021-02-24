using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Registro_Detalle_Prestamos.Models
{
    public class Prestamos
    {
        [Key]
        public int PrestamoId { get; set; }
        public DateTime Fecha { get; set; }
        [Required(ErrorMessage = " ")]
        public string Concepto { get; set; }
        [Range(minimum: 100, maximum: 1000000)]
        public double Monto { get; set; }
        public double Balance { get; set; }
        [Required(ErrorMessage = "Selecciona una persona para asignarle el prestamo.")]
        public int PersonaId { get; set; }

        [ForeignKey("PersonaId")]
        public virtual Personas Persona { get; set; }

        public Prestamos()
        {
            this.Fecha = DateTime.Now;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Registro_Detalle_Prestamos.Models
{
    public class Personas
    {
        [Key]
        public int PersonaId { get; set; }
        [Required(ErrorMessage = " ")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = " ")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = " ")]
        public string Cedula { get; set; }
        [Required(ErrorMessage = " ")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = " ")]
        public DateTime FechaNacimiento { get; set; }
        public double Balance { get; set; }

        public Personas()
        {
            this.FechaNacimiento = DateTime.Now;
        }
    }
}

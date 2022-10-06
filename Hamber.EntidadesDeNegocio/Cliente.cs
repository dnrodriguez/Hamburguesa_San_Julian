using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamber.EntidadesDeNegocio
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Nombre es Obligatorio")]
        [StringLength(30,ErrorMessage ="Maximo de 30 Caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Apellido es Obligatorio")]
        [StringLength(30, ErrorMessage = "Maximo de 30 Caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Email es Obligatorio")]
        [StringLength(30, ErrorMessage = "Maximo de 30 Caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Estatus es Obligatorio")]
        public byte Estatus { get; set; }

        [Display(Name ="Fecha")]
        public DateTime Fecha { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }



    }
    public enum Estatus_Cliente
    {
        ACTIVO = 1,
        INACTIVO = 2
    }
}

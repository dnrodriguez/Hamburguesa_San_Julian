using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamber.EntidadesDeNegocio
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Rol")]
        [Required(ErrorMessage ="Rol es Obligatorio")]
        [Display(Name ="Rol")]
        public int IdRol { get; set; }

        [Required(ErrorMessage ="Nombre es Obligatorio")]
        [StringLength(30,ErrorMessage ="Maximo 30 Caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="Apellido es Obligatorio")]
        [StringLength(30,ErrorMessage ="Maximo 30 Caracateres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage ="Login es Obligatorio")]
        [StringLength(25,ErrorMessage ="Maximo 25 Caracteres ")]
        public string Login { get; set; }

        [Required(ErrorMessage ="Pasword es Obligatorio")]
        [StringLength(32,ErrorMessage ="Pasword debe estar entre 32 y 5 Caracteres",MinimumLength =5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage ="Estatus es Obligatorio")]
        public byte Status { get; set; }

        [Display(Name ="FechaRegistro")]
        public DateTime FechaRegistro { get; set; }

        public Rol Rol { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }

        [NotMapped]
        [Required(ErrorMessage ="Confirmar el Password")]
        [StringLength(32,ErrorMessage ="Pasword debe estar entre 32 y 5  Caracteres",MinimumLength =5)]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Pasword y confirmar deben ser Iguales")]
        [Display(Name ="Confirmar Password")]
        public string ConfirmarPassword { get; set; } 



    }
    public enum Status_Usuario
    {
        ACTIVO = 1,
        INACTIVO = 2
    }
}

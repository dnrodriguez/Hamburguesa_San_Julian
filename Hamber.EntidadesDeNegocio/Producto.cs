using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamber.EntidadesDeNegocio
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Categoria")]
        [Required(ErrorMessage ="Categoria es Obligatorio")]
        [Display(Name ="Categoria")]
        public int IdCategoria { get; set; }

        [ForeignKey("Marca")]
        [Required(ErrorMessage ="Marca es Obligatorio")]
        [Display(Name ="Marca")]
        public int IdMarca { get; set; }



        [Required(ErrorMessage ="Nombre es Obligatorio")]
        [StringLength(30,ErrorMessage ="Maximo 30 Caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="Descripcion es Obligatorio")]
        [StringLength(25,ErrorMessage ="Maximo 25 Caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La imagen es requerida")]
        [MaxLength(200, ErrorMessage = "El largo máximo es 200 caracteres")]
        public string Imagen { get; set; }


        [Required(ErrorMessage ="Precio es Obligatorio")]
        [StringLength(15,ErrorMessage ="Maximo 15 Caracteres")]
        public string Precio { get; set; }

        [Display(Name ="Fecha")]
        [Required(ErrorMessage ="Fecha es Obligatorio")]
        public DateTime Fecha { get; set; }


        public Categoria Categoria { get; set; }

        public Marca Marca { get; set; }


        [NotMapped]
        public int Top_Aux { get; set; }

    }
}

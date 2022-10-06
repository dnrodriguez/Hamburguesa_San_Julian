using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamber.EntidadesDeNegocio
{
    public class Categoria
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Nombre de Categoria es Obligatorio")]
        [StringLength(30,ErrorMessage ="Maximo 30 Caracteres")]
        public string Nombre { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }

        public List<Producto> Producto { get; set; }
    }
}

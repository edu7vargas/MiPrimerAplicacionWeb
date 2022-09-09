using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiPrimerAplicacionWeb.Models
{
    public class TipoUsuarioCLS
    {
        [Display(Name = "ID tipo usuario")]
        public int iidtipousuario { get; set; }


        [Display(Name = "Nombre tipo usuario")]
        [Required]
        [StringLength(150,ErrorMessage ="Longitud máxima 150")]
        public string nombre { get; set; }


        [Display(Name = "Descripción")]
        [Required]
        [StringLength(250, ErrorMessage = "Longitud máxima 250")]
        public string descripcion { get; set; }





        public int bhabilitado { get; set; }





    }
}
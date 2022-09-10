using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiPrimerAplicacionWeb.Models
{
    public class RolCLS
    {


        [Display(Name="Id Rol")]
        public int iidRol { get; set; }

        [Display(Name = "Nombre")]
        [Required]
        public string nombre { get; set; }

        [Display(Name = "Descripción")]
        public string descripcion { get; set; }

        public int bhabilitado { get; set; }

    }
}
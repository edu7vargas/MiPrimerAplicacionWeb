using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiPrimerAplicacionWeb.Models
{
    public class PaginaCLS
    {
        [Display(Name ="Id página")]
        public int iidPagina { get; set;  }

        [Display(Name = "Titulo del link")]
        [Required]
        public string mensaje { get; set; }

        [Display(Name = "Nombre de la acción")]
        [Required]
        public string accion { get; set; }


        [Display(Name = "Controlador")]
        [Required]
        public string controlador { get; set; }

        
        public int bhabilitado { get; set; }

    }
}
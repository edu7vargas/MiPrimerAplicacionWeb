using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiPrimerAplicacionWeb.Models
{
    public class MarcaCLS
    {
        [Display(Name ="Id Marca")]

        public int iidmarca { get; set; }

        [Display(Name = "Nombre marca")]
        [Required]
        [StringLength(100,ErrorMessage ="La longitud máxima es de 100 caracteres.")]
        public string nombre { get; set; }

        [Display(Name = "Descripcion marca")]
        [StringLength(200, ErrorMessage = "La longitud máxima es de 200 caracteres.")]
        [Required]
        public string descripcion { get; set; }
        public int bhabilitado { get; set; }


        // AÑADIR UNA PROPIEDAD (ERRORES DE VALIDACIÓN)
        public string mensajeError { get; set; }




    }
}
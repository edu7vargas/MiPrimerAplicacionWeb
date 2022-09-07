using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiPrimerAplicacionWeb.Models
{
    public class SucursalCLS
    {

        [Display(Name = "Id Sucursal")]
        public int iidsucursal { get; set; }

        [Display(Name = "Nombre")]
        [Required]
        [StringLength(100, ErrorMessage = "La longitud máxima es de 100 caracteres.")]
        public string nombre { get; set; }

        [Display(Name = "Dirección")]
        [Required]
        [StringLength(200, ErrorMessage = "La longitud máxima es de 200 caracteres.")]
        public string direccion { get; set; }

        [Display(Name = "Telefono")]
        [Required]
        [StringLength(10, ErrorMessage = "La longitud máxima es de 10 caracteres.")]
        public string telefono { get; set; }

        [Display(Name = "Email")]
        [StringLength(100, ErrorMessage = "La longitud máxima es de 100 caracteres.")]
        [Required]
        [EmailAddress(ErrorMessage ="Ingrese un email valido.")]
        public string email { get; set; }

        [Display(Name = "Fecha de apertura")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime fechaapertura { get; set; }

        public int bhabilitado { get; set; }

        // AÑADO UNA PROPIEDAD (ERRORES DE VALIDACIÓN)
        public string mensajeError { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiPrimerAplicacionWeb.Models
{
    public class ClienteCLS
    {

        [Display(Name = "Id Cliente")]
        public int iidcliente { get; set; }

        [Display(Name = "Nombre")]
        [Required]
        [StringLength(100, ErrorMessage = "La longitud máxima es de 100 caracteres.")]
        public string nombre { get; set; }

        [Display(Name = "Apellido paterno")]
        [Required]
        [StringLength(150, ErrorMessage = "La longitud máxima es de 150 caracteres.")]
        public string appaterno { get; set; }

        [Display(Name = "Apellido materno")]
        [Required]
        [StringLength(150, ErrorMessage = "La longitud máxima es de 150 caracteres.")]
        public string apmaterno { get; set; }


        [Display(Name = "Email")]
        [StringLength(100, ErrorMessage = "La longitud máxima es de 100 caracteres.")]
        [Required]
        [EmailAddress(ErrorMessage = "Ingrese un email valido.")]
        public string email { get; set; }


        [Display(Name = "Dirección")]
        [DataType(DataType.MultilineText)]
        [StringLength(200, ErrorMessage = "La longitud máxima es de 200 caracteres.")]
        [Required]
        public string direccion { get; set; }

        
        [Display(Name = "Sexo")]
        [Required]
        public int iidsexo { get; set; }
        

        [Display(Name = "Telefono fijo")]
        [StringLength(10, ErrorMessage = "La longitud máxima es de 10 caracteres.")]
        public string telefonofijo { get; set; }



        [Display(Name = "Telefono celular")]
        [StringLength(10, ErrorMessage = "La longitud máxima es de 10 caracteres.")]
        public string telefonocelular { get; set; }


        public int bhabilitado { get; set; }



        // AÑADIR UNA PROPIEDAD (ERRORES DE VALIDACIÓN)
        public string mensajeError { get; set; }


    }
}
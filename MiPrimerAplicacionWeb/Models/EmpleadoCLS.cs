using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiPrimerAplicacionWeb.Models
{
    public class EmpleadoCLS
    {
        [Display(Name = "Id Empleado")]
        public int iidEmpleado { get; set; }

        [Display(Name = "Nombre")]
        [Required]
        [StringLength(100, ErrorMessage = "La longitud máxima es de 100 caracteres.")]
        public string nombre { get; set; }

        [Display(Name = "Apellido Paterno")]
        [Required]
        [StringLength(100, ErrorMessage = "La longitud máxima es de 100 caracteres.")]
        public string appaterno { get; set; }


        [Display(Name = "Apellido Materno")]
        [Required]
        [StringLength(100, ErrorMessage = "La longitud máxima es de 100 caracteres.")]
        public string apmaterno { get; set; }

        [Display(Name = "Fecha en contrato")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaContrato { get; set; }


        [Display(Name = "Sueldo")]
        [Required]
        [Range(0,100000, ErrorMessage = "Fuera del rango.")]
        public decimal sueldo { get; set; }


        [Display(Name = "Tipo usuario")]
        [Required]
        public int iidtipoUsuario { get; set; }



        [Display(Name = "Tipo contrato")]
        [Required]
        public int iidtipoContrato { get; set; }



        [Display(Name = "Sexo")]
        [Required]
        public int iidSexo { get; set; }



        public int bhabilitado { get; set; }


        // Propiedades adicionales
        [Display(Name = "Tipo contrato")]
        public string nombreTipoContrato { get; set; }



        [Display(Name = "Tipo usuario")]
        public string nombreTipoUsuario { get; set; }



        // 
        public string mensajeError2 { get; set; }

    }
}
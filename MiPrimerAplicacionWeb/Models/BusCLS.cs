using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiPrimerAplicacionWeb.Models
{
    public class BusCLS
    {
        public int iidbus { get; set; }

        [Display(Name = "Sucursal")]
        public int iidsucursal { get; set; }

        [Display(Name = "Tipo Bus")]
        [Required]
        public int iidtipobus { get; set; }

        [Display(Name = "Modelo")]
        [Required]
        public int iidmodelo { get; set; }

        [Display(Name = "Marca")]
        [Required]
        public int iidmarca { get; set; }

        [Display(Name = "Placa")]
        [Required]
        [StringLength(100, ErrorMessage = "La longitud máxima es de 100 caracteres.")]
        public string placa { get; set; }


        [Display(Name = "Fecha de compra")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechacompra { get; set; }
               


        [Display(Name = "Número de filas")]
        [Required]
        [Range(1, 99, ErrorMessage = "Rango entre 1 y 99.")]
        public int numerofilas { get; set; }


        [Display(Name = "Número de columnas")]
        [Required]
        [Range(1, 99, ErrorMessage = "Rango entre 1 y 99.")]
        public int numerocolumnas { get; set; }


        public int bhabilitado { get; set; }


        [Display(Name = "Descripción")]
        [Required]
        [StringLength(200, ErrorMessage = "La longitud máxima es de 200 caracteres.")]
        public string descripcion { get; set; }


        [Display(Name = "Observación")]
        [StringLength(200, ErrorMessage = "La longitud máxima es de 200 caracteres.")]
        public string observacion { get; set; }

       



        // Propiedades adicionales
        [Display(Name = "Tipo de bus")]
        public string nombreTipoBus { get; set; }


        [Display(Name = "Nombre Sucrusal")]
        public string nombreSucursal { get; set; }

        [Display(Name = "Modelo")]
        public string nombreModelo { get; set; }


        [Display(Name = "Marca")]
        public string nombreMarca { get; set; }


        // AÑADIR UNA PROPIEDAD (ERRORES DE VALIDACIÓN)
        public string mensajeError { get; set; }

    }
}
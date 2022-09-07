using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiPrimerAplicacionWeb.Models
{
    public class ViajeCLS
    {

        [Display(Name = "Id Viaje")]
        public int iidViaje { get; set; }


        [Display(Name = "ID lugar de origen")]
        [Required]
        public int iidLugarOrigen { get; set; }


        [Display(Name = "ID lugar de Destino")]
        [Required]
        public int iidLugarDestino { get; set; }


        [Display(Name = "Id Bus")]
        [Required]
        public int iidBus { get; set; }


        [Display(Name = "Precio")]
        [Required]
        [Range(0,10000, ErrorMessage = "Rango fuera de precio.")]
        public double precio { get; set; }


        [Display(Name = "Fecha de viaje")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaViaje { get; set; }


        [Display(Name = "Número de asientos disponibles")]
        [Required]
        [Range(1, 999, ErrorMessage = "Rango entre 1 y 999.")]
        public int numeroAsientosDisponibles { get; set; }


        [Display(Name = "Nombre de foto")]
        [StringLength(100, ErrorMessage = "La longitud máxima es de 100 caracteres.")]
        public string nombreFoto { get; set; }




        public int bhabilitado { get; set; }



        // Propiedades adicionales
        [Display(Name = "Lugar de origen")]
        public string nombreLugarOrigen { get; set; }


        [Display(Name = "Lugar de Destino")]
        public string nombreLugarDestino { get; set; }


        [Display(Name = "Placa de Bus")]
        public string nombreBus { get; set; }


    }
}
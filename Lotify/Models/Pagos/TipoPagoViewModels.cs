using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lotify.Models.Pagos
{
    public class TipoPagoViewModels
    {
        public int Id { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "Se requiere el nombre del  Pago.")]
        [Display(Name = "Nombre del Tipo de Pago")]

        public string NombreTipo { get; set; }
    }
}
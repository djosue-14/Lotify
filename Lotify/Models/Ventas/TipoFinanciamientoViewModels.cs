using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lotify.Models.Ventas
{
    public class TipoFinanciamientoViewModels
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se requiere el nombre del Tipo de Financiamiento.")]
        [Display(Name = "Nombre del Tipo de Financiamiento")]
        public string Plazo { get; set; }
    }
}
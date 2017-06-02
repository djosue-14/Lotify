using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lotify.Models.Pagos
{
    public class MesPagoViewModels
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se requiere el nombre del mes.")]
        [Display(Name = "Nombre del Mes")]
        public string NombreMes { get; set; }
    }
}
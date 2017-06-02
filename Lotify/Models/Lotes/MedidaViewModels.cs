using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lotify.Models.Lotes
{
    public class MedidaViewModels
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ancho")]
        public decimal Ancho { get; set; }

        [Required]
        [Display(Name = "Largo")]
        public decimal Largo { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lotify.Models.Lotes
{
    public class InteresViewModels
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tasa de Interes")]
        public decimal TasaInteres { get; set; }
    }
}
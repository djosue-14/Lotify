using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lotify.Models.Lotes
{
    public class AreaViewModels
    {

        public int Id { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se requiere el nombre del Area.")]
        [Display(Name = "Nombre del Area")]
        public string NombreArea { get; set; } 
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lotify.Models.Lotes
{
    public class ManzanaViewModels
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se requiere el nombre de la manzana.")]
        [Display(Name = "Nombre de la manzana")]
        public string NombreManzana { get; set; }
      


    }
}
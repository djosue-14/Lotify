using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lotify.Models.Telefonos
{
    public class CompaniaTelefonoViewModels
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se requiere el nombre de la compania telefonica.")]
        [Display(Name = "Nombre de la Compania")]
        public string NombreCompania { get; set; }
    }
}
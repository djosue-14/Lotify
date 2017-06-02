using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lotify.Models.Empleados
{
    public class CargoEmpleadoViewModels
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se requiere el nombre del cargo.")]
        [Display(Name = "Nombre del Cargo")]
        public string NombreCargo{ get; set; }

        [Display(Name  = "Sueldo")]
        public decimal Sueldo { get; set; }


    }
}
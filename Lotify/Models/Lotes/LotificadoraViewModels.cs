using Lotify.Models.Telefonos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lotify.Models.Lotes
{
    public class LotificadoraViewModels
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se el nombre de la lotificadora.")]
        [Display(Name = "Lotificadora")]
        public string NombreLotificadora { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se requiere la direccion de la lotificadora.")]
        [Display(Name = "Direccion")]
        public string Direccion { get; set; }

        //Aca empieza TelefonoCliente

        [Display(Name = "Numero de Telefono")]
        public int NumeroTelefono { get; set; }

        [Display(Name = "Compania de Telefono")]
        public int CompaniaTelefonoId { get; set; }

        [Display(Name = "Lista Companias Telefonicas")]
        public List<CompaniaTelefono> Companias { get; set; }

    }
}
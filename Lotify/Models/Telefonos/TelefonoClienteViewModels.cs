using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lotify.Models.Telefonos
{
    public class TelefonoClienteViewModels
    {
        public int Id { get; set; }

        [Display(Name = "Numero de Telefono")]
        public int NumeroTelefono { get; set; }

        [Display(Name = "Compania de Telefono")]
        public int CompaniaTelefonoId { get; set; }

        [Required]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }

        public List<CompaniaTelefono> Companias { get; set; }

    }
}
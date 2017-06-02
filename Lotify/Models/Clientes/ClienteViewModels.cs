using Lotify.Models.Telefonos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lotify.Models.Clientes
{
    public class ClienteViewModels
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se requiere el nombre del cliente.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se requiere el apellido del cliente.")]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [Required]
        [Display(Name = "DPI")]
        public long Dpi { get; set; }

        [Required]
        [StringLength(1, ErrorMessage = "Se requiere especificar el genero del cliente.")]
        [Display(Name = "Genero")]
        public string Genero { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Se requiere la dirección del cliente.")]
        [Display(Name = "Direccion")]
        public string Direccion { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [Display(Name = "Estado del Cliente")]
        public int EstadoClienteId { get; set; }

        public List<EstadoCliente> EstadoCliente { get; set; }


        //Aca empieza TelefonoCliente

        [Display(Name = "Numero de Telefono")]
        public int NumeroTelefono { get; set; }
        [Display(Name = "Compania de Telefono")]
        public int CompaniaTelefonoId { get; set; }
        [Display(Name = "Lista Companias Telefonicas")]
        public List<CompaniaTelefono> Companias { get; set; }
    }
}
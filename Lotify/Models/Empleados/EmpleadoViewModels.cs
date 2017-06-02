using Lotify.Models.Telefonos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lotify.Models.Empleados
{
    public class EmpleadoViewModels
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se requiere el nombre del empleado.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se requiere el apellido del empleado.")]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [Required]
        [Display(Name = "DPI")]
        public long Dpi { get; set; }

        [Required]
        [StringLength(1, ErrorMessage = "Se requiere especificar el genero del empleado.")]
        [Display(Name = "Genero")]
        public string Genero { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se requiere la dirección del empleado.")]
        [Display(Name = "Direccion")]
        public string Direccion { get; set; }
    
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [Display(Name = "Estado del Empleado")]
        public int EstadoEmpleadoId { get; set; }

        [Required]
        [Display(Name = "Cargo del Empleado")]
        public int CargoEmpleadoId { get; set; }

        [Required]
        [Display(Name = "Usuario del Empleado")]
        public int UserId { get; set; }


        //Datos del Usuario
        //[Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        //[Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        //Aca empieza TelefonoEmpleado

        [Display(Name = "Numero de Telefono")]
        public int NumeroTelefono { get; set; }
        [Display(Name = "Compania de Telefono")]
        public int CompaniaTelefonoId { get; set; }
        [Display(Name = "Lista Companias Telefonicas")]

        public List<CompaniaTelefono> Companias { get; set; }

        public List<EstadoEmpleado> EstadoEmpleado { get; set; }
        public List<CargoEmpleado> CargoEmpleado { get; set; }
        public List<ApplicationUser> User { get; set; }
    }

    public class EmpleadoEditViewModels
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se requiere el nombre del empleado.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se requiere el apellido del empleado.")]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [Required]
        [Display(Name = "DPI")]
        public long Dpi { get; set; }

        [Required]
        [StringLength(1, ErrorMessage = "Se requiere especificar el genero del empleado.")]
        [Display(Name = "Genero")]
        public string Genero { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se requiere la dirección del empleado.")]
        [Display(Name = "Direccion")]
        public string Direccion { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [Display(Name = "Estado del Empleado")]
        public int EstadoEmpleadoId { get; set; }

        [Required]
        [Display(Name = "Cargo del Empleado")]
        public int CargoEmpleadoId { get; set; }

        [Required]
        [Display(Name = "Usuario del Empleado")]
        public int UserId { get; set; }

        //Aca empieza TelefonoEmpleado

        [Display(Name = "Numero de Telefono")]
        public int NumeroTelefono { get; set; }

        [Display(Name = "Compania de Telefono")]
        public int CompaniaTelefonoId { get; set; }

        public List<CompaniaTelefono> Companias { get; set; }


        public List<EstadoEmpleado> EstadoEmpleado { get; set; }
        public List<CargoEmpleado> CargoEmpleado { get; set; }
        public List<ApplicationUser> User { get; set; }
    }
}
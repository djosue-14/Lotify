using Lotify.Models.Clientes;
using Lotify.Models.Empleados;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lotify.Models.Ventas
{
    public class VentaViewModels
    {
        public int Id { get; set; }

        //[Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Venta")]
        public DateTime FechaVenta { get; set; }
        
        public decimal Total { get; set; }
        public decimal Cuota { get; set; }

        [Required]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }

        [Required]
        [Display(Name = "Tipo de Financiamiento")]
        public int TipoFinanciamientoId { get; set; }

        public List<Empleado> Empleado { get; set; }
        public List<Cliente> Cliente { get; set; }
        public List<TipoFinanciamiento> TipoFinanciamiento { get; set; }

        public DetalleVentaViewModels detalle { get; set; }

        [Display(Name = "Cantidad A Ingresar")]
        public decimal Abono { get; set; }

    }

    public class CalculoCuotaViewModels 
    {
        [Required]
        public decimal precio { get; set; }
        [Required]
        public decimal interes { get; set; }
        [Required]
        public int plazo { get; set; }
    }
}
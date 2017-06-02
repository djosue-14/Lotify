using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lotify.Models.Ventas
{
    public class DetalleVentaViewModels
    {
        public int Id { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public int VentaId { get; set; }
        public int LoteId { get; set; }
    }
}
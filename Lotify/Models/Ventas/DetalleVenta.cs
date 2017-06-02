using Lotify.Models.Lotes;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Ventas
{
    public class DetalleVenta
    {
        public DetalleVenta()
        {

        }

        public int Id { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Cantidad { get; set; }

        //Foreign Key a Venta
        public int VentaId { get; set; }
        public virtual Venta Venta { get; set; }

        //Foreign Key a Lote
        public int LoteId { get; set; }
        public virtual Lote Lote { get; set; }

    }

    public class DetalleVentaConfiguration: EntityTypeConfiguration<DetalleVenta>
    {
        public DetalleVentaConfiguration()
        {
            ToTable("DetalleVentas");
        }
    }
}
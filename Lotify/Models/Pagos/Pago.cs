using Lotify.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Pagos
{
    public class Pago
    {
        public Pago()
        {

        }

        public int Id { get; set; }
        //blic int NumeroComprobante { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Cantidad { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal SaldoActual { get; set; }
        
        //Foreign Key a Venta
        public int VentaId { get; set; }
        public virtual Venta Venta { get; set; }

        //Foreign Key a Users
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        
    }

    public class PagoConfiguration: EntityTypeConfiguration<Pago>
    {
        public PagoConfiguration()
        {
            ToTable("Pagos");

            Property(p => p.FechaPago)
                .IsRequired();

            Property(p => p.Cantidad)
                .IsRequired();
        }
    }
}
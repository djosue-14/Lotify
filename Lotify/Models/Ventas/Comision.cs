using Lotify.Models.Empleados;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Ventas
{
    public class Comision
    {
        public Comision()
        {

        }

        public int Id { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaRegistrada { get; set; }

        public int EmpleadoId { get; set; }
        public virtual Empleado Empleado { get; set; }
        // 
        public int VentaId { get; set; }
        public virtual Venta Venta { get; set; }
    }

    public class ComisionConfiguration: EntityTypeConfiguration<Comision>
    {
        public ComisionConfiguration()
        {
            ToTable("Comisiones");

            Property(p => p.FechaRegistrada)
                .IsRequired();

            Property(p => p.Monto)
                .IsRequired();
        }
    }
}
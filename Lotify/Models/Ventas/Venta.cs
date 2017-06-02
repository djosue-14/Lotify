using Lotify.Models.Clientes;
using Lotify.Models.Empleados;
using Lotify.Models.Pagos;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Ventas
{
    public class Venta
    {
        public Venta()
        {
            
        }

        public int Id { get; set; }
        public DateTime FechaVenta { get; set; }

        public decimal Total { get; set; }
        public decimal Cuota { get; set; }

        //Foreign Key para Empleado
        public int EmpleadoId { get; set; }
        public virtual Empleado Empleado { get; set; }

        //Foreign Key para Cliente
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        //Foreign Key para TipoFinanciamiento
        public int TipoFinanciamientoId { get; set; }

        public virtual TipoFinanciamiento TipoFinanciamiento { get; set; }

        public int UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<DetalleVenta> DetalleVentas { get; set; }

        public virtual ICollection<Pago> Pagos { get; set; }

        public virtual ICollection<Comision> Comisiones { get; set; }
    }

    public class VentaConfiguration: EntityTypeConfiguration<Venta>
    {
        public VentaConfiguration()
        {
            ToTable("Ventas");
        }
    }
}
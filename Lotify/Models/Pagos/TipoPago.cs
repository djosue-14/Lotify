using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Pagos
{
    public class TipoPago
    {
        public TipoPago()
        {

        }
        public int Id { get; set; }

        public string NombreTipo { get; set; }

        //public virtual ICollection<Pago> Pagos { get; set; }
    }

    public class TipoPagoConfiguration: EntityTypeConfiguration<TipoPago>
    {
        public TipoPagoConfiguration()
        {
            ToTable("TipoPago");

            Property(p => p.NombreTipo)
                .IsRequired()
                .HasMaxLength(25);
        }
    }
}
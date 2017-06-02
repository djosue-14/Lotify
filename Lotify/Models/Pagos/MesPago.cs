using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Pagos
{
    public class MesPago
    {
        public MesPago()
        {

        }

        public int Id { get; set; }

        public string NombreMes { get; set; }

        //public virtual ICollection<Pago> Pagos { get; set; }
    }

    public class MesPagoConfiguration: EntityTypeConfiguration<MesPago>
    {
        public MesPagoConfiguration()
        {
            ToTable("MesPago");

            Property(p => p.NombreMes)
                .IsRequired()
                .HasMaxLength(15);
        }
    }
}
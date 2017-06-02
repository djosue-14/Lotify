using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Lotes
{
    public class Interes
    {
        public Interes()
        {
        }

        public int Id { get; set; }
        public decimal TasaInteres {get; set;}

        public virtual ICollection<Lote> Lotes { get; set; }
    }

    public class InteresConfiguration: EntityTypeConfiguration<Interes>
    {
        public InteresConfiguration()
        {
            ToTable("Interes");
            Property(p => p.TasaInteres)
                .IsRequired();
        }
    }
}
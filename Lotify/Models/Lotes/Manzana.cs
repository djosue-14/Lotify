using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Lotes
{
    public class Manzana
    {
        public Manzana()
        {
            //this.Areas = new HashSet<Area>();
        }

        public int Id { get; set; }
        public string NombreManzana { get; set; }
        //public virtual ICollection<Ubicacion> Ubicaciones { get; set; }

    }

    public class ManzanaConfiguration: EntityTypeConfiguration<Manzana>
    {
        public ManzanaConfiguration()
        {
            ToTable("Manzanas");

            Property(p => p.NombreManzana)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
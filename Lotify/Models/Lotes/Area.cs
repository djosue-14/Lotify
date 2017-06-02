using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Lotes
{
    public class Area
    {
        public Area()
        {
            //this.Manzanas = new HashSet<Manzana>();
        }

        public int Id { get; set; }
        public string NombreArea {get; set;}

        //public virtual ICollection<Ubicacion> Ubicaciones { get; set; }
    }

    public class AreaConfiguration: EntityTypeConfiguration<Area>
    {
        public AreaConfiguration()
        {
            ToTable("Areas");
            Property(p => p.NombreArea)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
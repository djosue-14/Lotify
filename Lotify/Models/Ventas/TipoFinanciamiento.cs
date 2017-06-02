using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Ventas
{
    public class TipoFinanciamiento
    {
        public TipoFinanciamiento()
        {

        }

        public int Id { get; set; }
        public string Plazo { get; set; }
        
        public virtual ICollection<Venta> Ventas { get; set; }
    }

    public class TipoFinanciamientoConfiguration: EntityTypeConfiguration<TipoFinanciamiento>
    {
        public TipoFinanciamientoConfiguration()
        {
            ToTable("TipoFinanciamientos");

            Property(p => p.Plazo)
                .IsRequired()
                .HasMaxLength(25);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Lotes
{
    public class EstadoLote
    {
        public EstadoLote()
        {

        }

        public int Id { get; set; }
        public string NombreEstado { get; set; }
        public virtual ICollection<Lote> Lotes {get; set;}
    }

    public class EstadoLoteConfiguration: EntityTypeConfiguration<EstadoLote>
    {
        public EstadoLoteConfiguration()
        {
            ToTable("EstadoLote");

            Property(p => p.NombreEstado)
                .IsRequired();
        }
    }
}
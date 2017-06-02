using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Lotes
{
    public class Medida
    {
        public Medida()
        {
        }

        public int Id { get; set; }
        public decimal Ancho {get; set;}
        public decimal Largo { get; set; }

        //public virtual ICollection<Lote> Lotes { get; set; }
    }

    public class MedidaConfiguration: EntityTypeConfiguration<Medida>
    {
        public MedidaConfiguration()
        {
            ToTable("Medidas");
        }
    }
}
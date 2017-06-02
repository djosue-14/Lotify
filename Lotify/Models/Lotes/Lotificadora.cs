using Lotify.Models.Telefonos;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Lotes
{
    public class Lotificadora
    {
        public Lotificadora()
        {

        }

        public int Id { get; set; }

        public string NombreLotificadora { get; set; }
        public string Direccion { get; set; }

        public virtual ICollection<Lote> Lotes { get; set; }
        public virtual ICollection<TelefonoLotificadora> TelefonosLotificadoras { get; set; }
    }

    public class LotificadoraConfiguration: EntityTypeConfiguration<Lotificadora>
    {
        public LotificadoraConfiguration()
        {
            ToTable("Lotificadoras");

            Property(p => p.NombreLotificadora)
                .IsRequired()
                .HasMaxLength(25);

            Property(p => p.Direccion)
                .IsRequired()
                .HasMaxLength(50);
        }    
    }
}
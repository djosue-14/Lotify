using Lotify.Models.Lotes;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Telefonos
{
    public class TelefonoLotificadora
    {
        public TelefonoLotificadora()
        {

        }

        public int Id { get; set; }
        public int NumeroTelefono { get; set; }

        //Foreign Key para CompaniaTelefono
        public int CompaniaTelefonoId { get; set; }
        public virtual CompaniaTelefono CompaniaTelefono { get; set; }

        //Foreign Key a Lotificadora
        public int LotificadoraId { get; set; }
        public virtual Lotificadora Lotificadora { get; set; }
    }

    public class TelefonoLotificadoraConfiguration: EntityTypeConfiguration<TelefonoLotificadora>
    {
        public TelefonoLotificadoraConfiguration()
        {
            ToTable("TelefonosLotificadoras");
        }
    }
}
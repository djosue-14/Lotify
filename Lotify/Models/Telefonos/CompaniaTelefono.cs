using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Telefonos
{
    public class CompaniaTelefono
    {
        public CompaniaTelefono()
        {

        }

        public int Id { get; set; }
        public string NombreCompania { get; set; }
        public virtual ICollection<TelefonoCliente> TelefonoClientes { get; set; }
        public virtual ICollection<TelefonoEmpleado> TelefonoEmpleados { get; set; }
    }

    public class CompaniaTelefonoConfiguration: EntityTypeConfiguration<CompaniaTelefono>
    {
        public CompaniaTelefonoConfiguration()
        {
            ToTable("CompaniasTelefonos");
            Property(p => p.NombreCompania)
                .IsRequired()
                .HasMaxLength(15);

        }
    }
}
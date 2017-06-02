using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Clientes
{
    public class EstadoCliente
    {
        public EstadoCliente()
        {

        }

        public int Id { get; set; }
        public string NombreEstado { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
    }

    public class EstadoClienteConfiguration: EntityTypeConfiguration<EstadoCliente>
    {
        public EstadoClienteConfiguration()
        {
            ToTable("EstadoCliente");

            Property(p => p.NombreEstado)
                .IsRequired()
                .HasMaxLength(15);
        }
    }
}
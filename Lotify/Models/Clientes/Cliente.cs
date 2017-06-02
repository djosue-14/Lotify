using Lotify.Models.Telefonos;
using Lotify.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Clientes
{
    public class Cliente
    {
        public Cliente()
        {

        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public long Dpi { get; set; }
        public string Genero { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }

        //Foreign Key para EstadoCliente
        public int EstadoClienteId { get; set; }
        public virtual EstadoCliente EstadoCliente { get; set; }

        public virtual ICollection<TelefonoCliente> TelefonosClientes { get; set; }

        public virtual ICollection<Venta> Ventas { get; set; }
    }

    public class ClienteConfiguration: EntityTypeConfiguration<Cliente>
    {
        public ClienteConfiguration()
        {
            ToTable("Clientes");

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(25);

            Property(p => p.Apellido)
                .IsRequired()
                .HasMaxLength(25);

            Property(p => p.Genero)
                .IsRequired()
                .HasMaxLength(1);

            Property(p => p.Direccion)
                .IsRequired()
                .HasMaxLength(50);

            Property(p => p.FechaNacimiento)
                .IsRequired();
        }
    }
}
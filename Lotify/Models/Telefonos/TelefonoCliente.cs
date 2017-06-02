using Lotify.Models.Clientes;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Telefonos
{
    public class TelefonoCliente
    {
        public TelefonoCliente()
        {

        }

        public int Id { get; set; }
        public int NumeroTelefono { get; set; }

        //Foreign Key para CompaniaTelefono
        public int CompaniaTelefonoId { get; set; }
        public virtual CompaniaTelefono CompaniaTelefono { get; set; }

        //Foreign Key para Cliente
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

    }

    public class TelefonoClienteConfiguration: EntityTypeConfiguration<TelefonoCliente>
    {
        public TelefonoClienteConfiguration()
        {
            ToTable("TelefonosClientes");
        }
    }
}
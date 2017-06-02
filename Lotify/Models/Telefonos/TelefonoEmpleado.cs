using Lotify.Models.Empleados;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Telefonos
{
    public class TelefonoEmpleado
    {
        public TelefonoEmpleado()
        {

        }
        public int Id { get; set; }
        public int NumeroTelefono { get; set; }

        //Foreign Key para CompaniaTelefono
        public int CompaniaTelefonoId { get; set; }
        public virtual CompaniaTelefono CompaniaTelefono { get; set; }

        //Foreign Key para Empleado
        public int EmpleadoId { get; set; }
        public virtual Empleado Empleado { get; set; }
    }

    public class TelefonoEmpleadoConfiguration: EntityTypeConfiguration<TelefonoEmpleado>
    {
        public TelefonoEmpleadoConfiguration()
        {
            ToTable("TelefonosEmpleados");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Empleados
{
    public class EstadoEmpleado
    {
        public EstadoEmpleado()
        {

        }

        public int Id { get; set; }
        public string NombreEstado { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }

    public class EstadoEmpleadoConfiguration: EntityTypeConfiguration<EstadoEmpleado>
    {
        public EstadoEmpleadoConfiguration()
        {
            ToTable("EstadoEmpleado");

            Property(p => p.NombreEstado)
                .IsRequired()
                .HasMaxLength(15);
        }
    }
}
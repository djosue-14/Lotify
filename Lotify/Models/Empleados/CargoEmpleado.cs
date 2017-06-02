using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Empleados
{
    public class CargoEmpleado
    {
        public CargoEmpleado()
        {

        }

        public int Id { get; set; }
        public string NombreCargo { get; set; }
        public decimal Sueldo { get; set; }
        
        public virtual ICollection<Empleado> Empleados { get; set; }
    }

    public class CargoEmpleadoConfiguration: EntityTypeConfiguration<CargoEmpleado>
    {
        public CargoEmpleadoConfiguration()
        {
            ToTable("CargoEmpleado");

            Property(p => p.NombreCargo)
                .IsRequired()
                .HasMaxLength(25);
        }
    }
}
using Lotify.Models.Telefonos;
using Lotify.Models.Ventas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Lotify.Models.Empleados
{
    public class Empleado
    {
        public Empleado()
        {

        }

        public int Id { get; set; }
        //public string CodigoEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public long Dpi { get; set; }
        public string Genero { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }

        //Foreign Key para EstadoEmpleado
        public int EstadoEmpleadoId { get; set; }
        public virtual EstadoEmpleado EstadoEmpleado { get; set; }

        //Foreign Key para CargoEmpleado
        public int CargoEmpleadoId { get; set; }
        public virtual CargoEmpleado CargoEmpleado { get; set; }

        //Telefonos
        public virtual ICollection<TelefonoEmpleado> TelefonosEmpleados { get; set; }

        //Ventas
        public virtual ICollection<Venta> Ventas { get; set; }

        public virtual ICollection<Comision> Comisiones { get; set; }

        //[Key]
        public int UserId { get; set; }
        //[ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

    }

    public class EmpleadoConfiguration: EntityTypeConfiguration<Empleado>
    {
        public EmpleadoConfiguration()
        {
            ToTable("Empleados");

            Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(25);

            Property(p => p.Apellido)
                .IsRequired()
                .HasMaxLength(25);

            Property(p => p.Dpi)
                .IsRequired();

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
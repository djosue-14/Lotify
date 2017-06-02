using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MySqlDB;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lotify.Models.Telefonos;
using System.Data.Entity.ModelConfiguration.Conventions;
using Lotify.Models.Empleados;
using Lotify.Models.Lotes;
using Lotify.Models.Clientes;
using Lotify.Models.Pagos;
using Lotify.Models.Ventas;

namespace Lotify.Models
{
    // Puede agregar datos del perfil del usuario agregando más propiedades a la clase ApplicationUser. Para más información, visite http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser<int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }

        private string defaultValue = "User";

        [MaxLength(25)]
        [DefaultValue("User")]
        public string UserRole
        {
            get
            {
                return defaultValue;
            }
            set
            {
                defaultValue = value;
            }
        }

        //public int EmpleadoId { get; set; }
        //public virtual Empleado Empleado { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationUserLogin,
        ApplicationUserRole, ApplicationUserClaim>
    {
        static ApplicationDbContext()
        {
            Database.SetInitializer(new MySqlInitializer());
        }

        public ApplicationDbContext()
            : base("Lotify")
        {
           //Configuration.ProxyCreationEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
        //Clientes
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<EstadoCliente> EstadoCliente { get; set; }
        
        //Empleados
        public DbSet<CargoEmpleado> CargoEmpleado { get; set; }
        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<EstadoEmpleado> EstadoEmpleado { get; set; }

        //Lotes
        public DbSet<Area> Area { get; set; }
        public DbSet<EstadoLote> EstadoLote { get; set; }
        public DbSet<Interes> Interes { get; set; }
        public DbSet<Lote> Lote { get; set; }
        public DbSet<Lotificadora> Lotificadora { get; set; }
        public DbSet<Manzana> Manzana { get; set; }
        public DbSet<Medida> Medida { get; set; }
        //public DbSet<Ubicacion> Ubicacion { get; set; }

        //Pagos
        public DbSet<MesPago> MesPago { get; set; }
        public DbSet<Pago> Pago { get; set; }
        public DbSet<TipoPago> TipoPago { get; set; }

        //Telefonos
        public DbSet<CompaniaTelefono> CompaniaTelefono { get; set; }
        public DbSet<TelefonoEmpleado> TelefonoEmpleado { get; set; }
        public DbSet<TelefonoCliente> TelefonoCliente { get; set; }
        public DbSet<TelefonoLotificadora> TelefonoLotificadora { get; set; }

        //Ventas
        public DbSet<Comision> Comision { get; set; }
        public DbSet<DetalleVenta> DetalleVenta { get; set; }
        public DbSet<TipoFinanciamiento> TipoFinanciamiento { get; set; }
        public DbSet<Venta> Venta { get; set; }//*/


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .Ignore(c => c.EmailConfirmed)
                .Ignore(c => c.PhoneNumber)
                .Ignore(c => c.PhoneNumberConfirmed)
                .Ignore(c => c.TwoFactorEnabled)
                .Ignore(c => c.LockoutEndDateUtc)
                .Ignore(c => c.LockoutEnabled)
                .Ignore(c => c.AccessFailedCount);

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<ApplicationRole>().ToTable("Roles");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<ApplicationUserClaim>().ToTable("UserClaims");

            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
            //Tablas De La App...
            //Clientes
            modelBuilder.Configurations.Add(new ClienteConfiguration());
            modelBuilder.Configurations.Add(new EstadoClienteConfiguration());

            //Empleados
            modelBuilder.Configurations.Add(new CargoEmpleadoConfiguration());
            modelBuilder.Configurations.Add(new EstadoEmpleadoConfiguration());
            modelBuilder.Configurations.Add(new EmpleadoConfiguration());

            modelBuilder.Entity<Empleado>()
                    .HasRequired<ApplicationUser>(p => p.User)
                    .WithMany()
                    .HasForeignKey(p => p.UserId);

            //Lotes
            modelBuilder.Configurations.Add(new AreaConfiguration());
            modelBuilder.Configurations.Add(new LoteConfiguration());
            modelBuilder.Configurations.Add(new MedidaConfiguration());
            modelBuilder.Configurations.Add(new EstadoLoteConfiguration());
            modelBuilder.Configurations.Add(new LotificadoraConfiguration());
            modelBuilder.Configurations.Add(new ManzanaConfiguration());
            //modelBuilder.Configurations.Add(new UbicacionConfiguration());


            //Pagos
            modelBuilder.Configurations.Add(new MesPagoConfiguration());
            modelBuilder.Configurations.Add(new PagoConfiguration());
            modelBuilder.Configurations.Add(new TipoPagoConfiguration());

            //Ventas
            modelBuilder.Configurations.Add(new CompaniaTelefonoConfiguration());
            modelBuilder.Configurations.Add(new TelefonoEmpleadoConfiguration());
            modelBuilder.Configurations.Add(new TelefonoClienteConfiguration());
            modelBuilder.Configurations.Add(new TelefonoLotificadoraConfiguration());

            //Ventas
            modelBuilder.Configurations.Add(new ComisionConfiguration());
            modelBuilder.Configurations.Add(new DetalleVentaConfiguration());
            modelBuilder.Configurations.Add(new TipoFinanciamientoConfiguration());
            modelBuilder.Configurations.Add(new VentaConfiguration());

            /*modelBuilder.Entity<Manzana>()
                .HasMany<Area>(s => s.Areas)
                .WithMany(c => c.Manzanas)
                .Map(cs =>
                {
                    cs.MapLeftKey("ManzanaId");
                    cs.MapRightKey("AreaId");
                    cs.ToTable("ManzanaArea");
                });*/
        }
    }

    public class ApplicationUserRole : IdentityUserRole<int>    
    {
    }
    public class ApplicationUserClaim : IdentityUserClaim<int>
    {
    }
    public class ApplicationUserLogin : IdentityUserLogin<int>
    {
    }
    public class ApplicationRole : IdentityRole<int, ApplicationUserRole>
    {
        public ApplicationRole() { }
        public ApplicationRole(string name) { Name = name; }
    }
    public class RoleStore : RoleStore<ApplicationRole, int, ApplicationUserRole>
    {
        public RoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
    public class UserStore : UserStore<ApplicationUser, ApplicationRole, int, ApplicationUserLogin,
        ApplicationUserRole, ApplicationUserClaim>
    {
        public UserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
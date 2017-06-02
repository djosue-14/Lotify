namespace Lotify.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LotifyDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreArea = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ubicacion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManzanaId = c.Int(nullable: false),
                        AreaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Areas", t => t.AreaId, cascadeDelete: true)
                .ForeignKey("dbo.Manzanas", t => t.ManzanaId, cascadeDelete: true)
                .Index(t => t.ManzanaId)
                .Index(t => t.AreaId);
            
            CreateTable(
                "dbo.Lotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroLote = c.Int(nullable: false),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MedidaId = c.Int(nullable: false),
                        EstadoLoteId = c.Int(nullable: false),
                        LotificadoraId = c.Int(nullable: false),
                        UbicacionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EstadoLote", t => t.EstadoLoteId, cascadeDelete: true)
                .ForeignKey("dbo.Lotificadoras", t => t.LotificadoraId, cascadeDelete: true)
                .ForeignKey("dbo.Medidas", t => t.MedidaId, cascadeDelete: true)
                .ForeignKey("dbo.Ubicacion", t => t.UbicacionId, cascadeDelete: true)
                .Index(t => t.MedidaId)
                .Index(t => t.EstadoLoteId)
                .Index(t => t.LotificadoraId)
                .Index(t => t.UbicacionId);
            
            CreateTable(
                "dbo.DetalleVentas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrecioVenta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cantidad = c.Int(nullable: false),
                        VentaId = c.Int(nullable: false),
                        LoteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lotes", t => t.LoteId, cascadeDelete: true)
                .ForeignKey("dbo.Ventas", t => t.VentaId, cascadeDelete: true)
                .Index(t => t.VentaId)
                .Index(t => t.LoteId);
            
            CreateTable(
                "dbo.Ventas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaVenta = c.DateTime(nullable: false, precision: 0),
                        NumeroComprobante = c.Int(nullable: false),
                        EmpleadoId = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                        TipoFinanciamientoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empleados", t => t.EmpleadoId, cascadeDelete: true)
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .ForeignKey("dbo.TipoFinanciamientos", t => t.TipoFinanciamientoId, cascadeDelete: true)
                .Index(t => t.EmpleadoId)
                .Index(t => t.ClienteId)
                .Index(t => t.TipoFinanciamientoId);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 25, storeType: "nvarchar"),
                        Apellido = c.String(nullable: false, maxLength: 25, storeType: "nvarchar"),
                        Dpi = c.Long(nullable: false),
                        Genero = c.String(nullable: false, maxLength: 1, storeType: "nvarchar"),
                        Direccion = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        FechaNacimiento = c.DateTime(nullable: false, precision: 0),
                        EstadoClienteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EstadoCliente", t => t.EstadoClienteId, cascadeDelete: true)
                .Index(t => t.EstadoClienteId);
            
            CreateTable(
                "dbo.EstadoCliente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreEstado = c.String(nullable: false, maxLength: 15, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TelefonosClientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroTelefono = c.Int(nullable: false),
                        CompaniaTelefonoId = c.Int(nullable: false),
                        ClienteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .ForeignKey("dbo.CompaniasTelefonos", t => t.CompaniaTelefonoId, cascadeDelete: true)
                .Index(t => t.CompaniaTelefonoId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.CompaniasTelefonos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreCompania = c.String(nullable: false, maxLength: 15, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TelefonosEmpleados",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroTelefono = c.Int(nullable: false),
                        CompaniaTelefonoId = c.Int(nullable: false),
                        EmpleadoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CompaniasTelefonos", t => t.CompaniaTelefonoId, cascadeDelete: true)
                .ForeignKey("dbo.Empleados", t => t.EmpleadoId, cascadeDelete: true)
                .Index(t => t.CompaniaTelefonoId)
                .Index(t => t.EmpleadoId);
            
            CreateTable(
                "dbo.Empleados",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 25, storeType: "nvarchar"),
                        Apellido = c.String(nullable: false, maxLength: 25, storeType: "nvarchar"),
                        Dpi = c.Long(nullable: false),
                        Genero = c.String(nullable: false, maxLength: 1, storeType: "nvarchar"),
                        Direccion = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        FechaNacimiento = c.DateTime(nullable: false, precision: 0),
                        EstadoEmpleadoId = c.Int(nullable: false),
                        CargoEmpleadoId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CargoEmpleado", t => t.CargoEmpleadoId, cascadeDelete: true)
                .ForeignKey("dbo.EstadoEmpleado", t => t.EstadoEmpleadoId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.EstadoEmpleadoId)
                .Index(t => t.CargoEmpleadoId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CargoEmpleado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreCargo = c.String(nullable: false, maxLength: 25, storeType: "nvarchar"),
                        Sueldo = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comisiones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FechaRegistrada = c.DateTime(nullable: false, precision: 0),
                        EmpleadoId = c.Int(nullable: false),
                        VentaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empleados", t => t.EmpleadoId, cascadeDelete: true)
                .ForeignKey("dbo.Ventas", t => t.VentaId, cascadeDelete: true)
                .Index(t => t.EmpleadoId)
                .Index(t => t.VentaId);
            
            CreateTable(
                "dbo.EstadoEmpleado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreEstado = c.String(nullable: false, maxLength: 15, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pagos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroComprobante = c.Int(nullable: false),
                        FechaPago = c.DateTime(nullable: false, precision: 0),
                        CantidadCancelada = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VentaId = c.Int(nullable: false),
                        TipoPagoId = c.Int(nullable: false),
                        MesPagoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MesPago", t => t.MesPagoId, cascadeDelete: true)
                .ForeignKey("dbo.TipoPago", t => t.TipoPagoId, cascadeDelete: true)
                .ForeignKey("dbo.Ventas", t => t.VentaId, cascadeDelete: true)
                .Index(t => t.VentaId)
                .Index(t => t.TipoPagoId)
                .Index(t => t.MesPagoId);
            
            CreateTable(
                "dbo.MesPago",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreMes = c.String(nullable: false, maxLength: 15, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TipoPago",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreTipo = c.String(nullable: false, maxLength: 15, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TipoFinanciamientos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Plazo = c.String(nullable: false, maxLength: 25, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EstadoLote",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreEstado = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lotificadoras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreLotificadora = c.String(nullable: false, maxLength: 25, storeType: "nvarchar"),
                        Direccion = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TelefonosLotificadoras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroTelefono = c.Int(nullable: false),
                        CompaniaTelefonoId = c.Int(nullable: false),
                        LotificadoraId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CompaniasTelefonos", t => t.CompaniaTelefonoId, cascadeDelete: true)
                .ForeignKey("dbo.Lotificadoras", t => t.LotificadoraId, cascadeDelete: true)
                .Index(t => t.CompaniaTelefonoId)
                .Index(t => t.LotificadoraId);
            
            CreateTable(
                "dbo.Medidas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ancho = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Largo = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Manzanas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreManzana = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ubicacion", "ManzanaId", "dbo.Manzanas");
            DropForeignKey("dbo.Lotes", "UbicacionId", "dbo.Ubicacion");
            DropForeignKey("dbo.Lotes", "MedidaId", "dbo.Medidas");
            DropForeignKey("dbo.TelefonosLotificadoras", "LotificadoraId", "dbo.Lotificadoras");
            DropForeignKey("dbo.TelefonosLotificadoras", "CompaniaTelefonoId", "dbo.CompaniasTelefonos");
            DropForeignKey("dbo.Lotes", "LotificadoraId", "dbo.Lotificadoras");
            DropForeignKey("dbo.Lotes", "EstadoLoteId", "dbo.EstadoLote");
            DropForeignKey("dbo.Ventas", "TipoFinanciamientoId", "dbo.TipoFinanciamientos");
            DropForeignKey("dbo.Pagos", "VentaId", "dbo.Ventas");
            DropForeignKey("dbo.Pagos", "TipoPagoId", "dbo.TipoPago");
            DropForeignKey("dbo.Pagos", "MesPagoId", "dbo.MesPago");
            DropForeignKey("dbo.DetalleVentas", "VentaId", "dbo.Ventas");
            DropForeignKey("dbo.Ventas", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.Ventas", "EmpleadoId", "dbo.Empleados");
            DropForeignKey("dbo.Empleados", "UserId", "dbo.Users");
            DropForeignKey("dbo.TelefonosEmpleados", "EmpleadoId", "dbo.Empleados");
            DropForeignKey("dbo.Empleados", "EstadoEmpleadoId", "dbo.EstadoEmpleado");
            DropForeignKey("dbo.Comisiones", "VentaId", "dbo.Ventas");
            DropForeignKey("dbo.Comisiones", "EmpleadoId", "dbo.Empleados");
            DropForeignKey("dbo.Empleados", "CargoEmpleadoId", "dbo.CargoEmpleado");
            DropForeignKey("dbo.TelefonosEmpleados", "CompaniaTelefonoId", "dbo.CompaniasTelefonos");
            DropForeignKey("dbo.TelefonosClientes", "CompaniaTelefonoId", "dbo.CompaniasTelefonos");
            DropForeignKey("dbo.TelefonosClientes", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.Clientes", "EstadoClienteId", "dbo.EstadoCliente");
            DropForeignKey("dbo.DetalleVentas", "LoteId", "dbo.Lotes");
            DropForeignKey("dbo.Ubicacion", "AreaId", "dbo.Areas");
            DropIndex("dbo.TelefonosLotificadoras", new[] { "LotificadoraId" });
            DropIndex("dbo.TelefonosLotificadoras", new[] { "CompaniaTelefonoId" });
            DropIndex("dbo.Pagos", new[] { "MesPagoId" });
            DropIndex("dbo.Pagos", new[] { "TipoPagoId" });
            DropIndex("dbo.Pagos", new[] { "VentaId" });
            DropIndex("dbo.Comisiones", new[] { "VentaId" });
            DropIndex("dbo.Comisiones", new[] { "EmpleadoId" });
            DropIndex("dbo.Empleados", new[] { "UserId" });
            DropIndex("dbo.Empleados", new[] { "CargoEmpleadoId" });
            DropIndex("dbo.Empleados", new[] { "EstadoEmpleadoId" });
            DropIndex("dbo.TelefonosEmpleados", new[] { "EmpleadoId" });
            DropIndex("dbo.TelefonosEmpleados", new[] { "CompaniaTelefonoId" });
            DropIndex("dbo.TelefonosClientes", new[] { "ClienteId" });
            DropIndex("dbo.TelefonosClientes", new[] { "CompaniaTelefonoId" });
            DropIndex("dbo.Clientes", new[] { "EstadoClienteId" });
            DropIndex("dbo.Ventas", new[] { "TipoFinanciamientoId" });
            DropIndex("dbo.Ventas", new[] { "ClienteId" });
            DropIndex("dbo.Ventas", new[] { "EmpleadoId" });
            DropIndex("dbo.DetalleVentas", new[] { "LoteId" });
            DropIndex("dbo.DetalleVentas", new[] { "VentaId" });
            DropIndex("dbo.Lotes", new[] { "UbicacionId" });
            DropIndex("dbo.Lotes", new[] { "LotificadoraId" });
            DropIndex("dbo.Lotes", new[] { "EstadoLoteId" });
            DropIndex("dbo.Lotes", new[] { "MedidaId" });
            DropIndex("dbo.Ubicacion", new[] { "AreaId" });
            DropIndex("dbo.Ubicacion", new[] { "ManzanaId" });
            DropTable("dbo.Manzanas");
            DropTable("dbo.Medidas");
            DropTable("dbo.TelefonosLotificadoras");
            DropTable("dbo.Lotificadoras");
            DropTable("dbo.EstadoLote");
            DropTable("dbo.TipoFinanciamientos");
            DropTable("dbo.TipoPago");
            DropTable("dbo.MesPago");
            DropTable("dbo.Pagos");
            DropTable("dbo.EstadoEmpleado");
            DropTable("dbo.Comisiones");
            DropTable("dbo.CargoEmpleado");
            DropTable("dbo.Empleados");
            DropTable("dbo.TelefonosEmpleados");
            DropTable("dbo.CompaniasTelefonos");
            DropTable("dbo.TelefonosClientes");
            DropTable("dbo.EstadoCliente");
            DropTable("dbo.Clientes");
            DropTable("dbo.Ventas");
            DropTable("dbo.DetalleVentas");
            DropTable("dbo.Lotes");
            DropTable("dbo.Ubicacion");
            DropTable("dbo.Areas");
        }
    }
}

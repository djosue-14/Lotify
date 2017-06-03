using Lotify.Models;
using Lotify.Models.Clientes;
using Lotify.Models.Empleados;
using Lotify.Models.Lotes;
using Lotify.Models.Ventas;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Lotify.Controllers.Ventas
{
    [Authorize]
    public class VentaController : Controller
    {
        private ApplicationDbContext dbCtx;
        private Venta venta;

        public VentaController()
        {
            dbCtx = new ApplicationDbContext();
            venta = new Venta();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Ventas";
            return View();
        }

        [HttpGet]
        public JsonResult Show()
        {
            var venta = dbCtx.Venta.Select(c => new
                        {
                            c.Id,
                            c.FechaVenta,
                            c.Total,
                            Empleado = new
                            {
                                c.Empleado.Id,
                                c.Empleado.Nombre,
                                c.Empleado.Apellido,
                            },
                            Cliente = new
                            {
                                c.Cliente.Id,
                                c.Cliente.Nombre,
                                c.Cliente.Apellido,
                            },
                            TipoFinanciamiento = new
                            {
                                c.TipoFinanciamiento.Id,
                                c.TipoFinanciamiento.Plazo,
                            }
                        });

            return Json(venta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ShowId(int id)
        {
            var venta = dbCtx.Venta.FirstOrDefault(c => c.Id == id);

            return Json(venta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Dependencias(int id)
        {
            VentaViewModels model = new VentaViewModels();

            model.ClienteId = id;
            //model.Empleado = dbCtx.Empleado.ToList();
            //model.TipoFinanciamiento = dbCtx.TipoFinanciamiento.ToList();
            //var empleado = dbCtx.Empleado.Select(c => new { c.Id, c.Nombre, c.Apellido, c.Dpi});
            var financiamiento = dbCtx.TipoFinanciamiento.Select(c => new { c.Id, c.Plazo });

            return Json(new { ClienteId = model.ClienteId, Financiamiento = financiamiento }, 
                JsonRequestBehavior.AllowGet); 
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Title = "Ingresar Venta";

            /*VentaViewModels model = new VentaViewModels();

            model.ClienteId = id;
            model.Empleado = dbCtx.Empleado.ToList();
            model.TipoFinanciamiento = dbCtx.TipoFinanciamiento.ToList();//*/

            
            return View();
        }

        [HttpPost]
        public ActionResult Create(VentaViewModels model)
        {
            int UserId = Convert.ToInt32(User.Identity.GetUserId());

            int num = 0;
            if (ModelState.IsValid)
            {
                Empleado empleado = dbCtx.Empleado.FirstOrDefault(c => c.UserId == UserId);

                venta.FechaVenta = DateTime.Today;
                venta.Total = model.Total;
                venta.Cuota = model.Cuota;
                venta.EmpleadoId = empleado.Id;
                venta.ClienteId = model.ClienteId;
                venta.TipoFinanciamientoId = model.TipoFinanciamientoId;
                venta.UserId = Convert.ToInt32(User.Identity.GetUserId()); //User.Identity.GetUserId<int>();

                dbCtx.Venta.Add(venta);
                num = dbCtx.SaveChanges();

                model.Id = venta.Id;

                //Detalle
                InsertDetalle(model);

                //Comision.
                InsertarComision(model, empleado.Id);

            }

            if (num > 0)
            {
                // return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK, venta.Id.ToString());
                return Json( new { venta = venta.Id }, JsonRequestBehavior.AllowGet);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            VentaViewModels model = new VentaViewModels();

            venta = dbCtx.Venta.FirstOrDefault(c => c.Id == id);

            model.Id = venta.Id;
            model.Total = venta.Total;
            model.Cuota = venta.Cuota;

            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(VentaViewModels model)
        {
            if (model.Abono >= model.Cuota )
            {
                if (ModelState.IsValid)
                {
                    venta = dbCtx.Venta.FirstOrDefault(a => a.Id == model.Id);
                    venta.Total = (venta.Total - model.Abono);
                    venta.UserId = Convert.ToInt32(User.Identity.GetUserId());
                    dbCtx.SaveChanges();
                }
            }
            else
            {
                return View("Error", model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult CalcularCuota(CalculoCuotaViewModels model)
        {
            decimal x = (1 + model.interes);
            decimal topExp = model.interes * Convert.ToDecimal(Math.Pow(Convert.ToDouble(x), model.plazo));
            decimal downExp = Convert.ToDecimal(Math.Pow(Convert.ToDouble(x), model.plazo)) - 1;
            decimal cuota = model.precio * (topExp / downExp);
            decimal total = cuota * model.plazo;
            decimal montoInteres = total - model.precio;
            
            if (ModelState.IsValid)
            {
                return Json(new { cuota, total, montoInteres }, JsonRequestBehavior.AllowGet);
            }

            return Json( new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest), JsonRequestBehavior.AllowGet);
        }

        public void InsertDetalle(VentaViewModels model)
        {
            DetalleVenta detalle = new DetalleVenta();

            Lote lote = dbCtx.Lote.FirstOrDefault(c => c.Id == model.detalle.LoteId);

            detalle.PrecioVenta = lote.Precio;
            detalle.Cantidad = 1;
            detalle.VentaId = model.Id;
            detalle.LoteId = lote.Id;

            dbCtx.DetalleVenta.Add(detalle);
            dbCtx.SaveChanges();

            //Lote

            CambiarEstado(lote.Id);
        }

        public void InsertarComision(VentaViewModels model, int EmpleadoId)
        {
            Comision comision = new Comision();

            comision.Monto = model.Total * Convert.ToDecimal(0.05);
            comision.FechaRegistrada = DateTime.Today;

            comision.EmpleadoId = EmpleadoId;
            comision.VentaId = model.Id;

            dbCtx.Comision.Add(comision);
            dbCtx.SaveChanges();
        }

        public void CambiarEstado(int id)
        {
            Lote lote = dbCtx.Lote.FirstOrDefault(c => c.Id == id);

            EstadoLote estado = dbCtx.EstadoLote.FirstOrDefault(c => c.NombreEstado == "Vendido");

            lote.EstadoLoteId = estado.Id;

            dbCtx.SaveChanges();
        }
    }
}
using Lotify.Models;
using Lotify.Models.Pagos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lotify.Controllers.Pagos
{
    public class TipoPagoController : Controller
    {
        // GET: Area
        // GET: EstadoLote
        private ApplicationDbContext dbCtx;
        private TipoPago TipoPago;

        public TipoPagoController()
        {
            dbCtx = new ApplicationDbContext();
            TipoPago = new TipoPago();
        }

        // GET: EstadoCliente
        public ActionResult Index()
        {
            ViewBag.Title = "Tipo de Pago";
            var lista = dbCtx.TipoPago.ToList();
            return View();
        }

        [HttpGet]
        public JsonResult Show()
        {
            var listaTipo = dbCtx.TipoPago.Select(c => new
            {
                c.Id,
                c.NombreTipo
            });

            return Json(listaTipo, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ShowId(int id)
        {
            var TipoPago = dbCtx.TipoPago.FirstOrDefault(a => a.Id == id);

            return Json(TipoPago, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Agregar TipoPago";
            return View();
        }

        [HttpPost]
        public ActionResult Create(TipoPagoViewModels model)
        {

            if (ModelState.IsValid)
            {
                TipoPago.NombreTipo = model.NombreTipo;
                dbCtx.TipoPago.Add(TipoPago);
                dbCtx.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Editar TipoPago";

            TipoPagoViewModels tipopago = new TipoPagoViewModels();

            TipoPago = dbCtx.TipoPago.FirstOrDefault(a => a.Id == id);
            tipopago.Id = TipoPago.Id;
            tipopago.NombreTipo = TipoPago.NombreTipo;

            return View(tipopago);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(TipoPagoViewModels model)
        {
            if (ModelState.IsValid)
            {
                TipoPago = dbCtx.TipoPago.FirstOrDefault(a => a.Id == model.Id);
                TipoPago.NombreTipo = model.NombreTipo;
                dbCtx.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(TipoPagoViewModels model)
        {
            var tipopago = (from p in dbCtx.TipoPago
                        where p.Id == model.Id
                        select p).FirstOrDefault();

            dbCtx.TipoPago.Remove(tipopago);

            int num = dbCtx.SaveChanges();

            if (num > 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
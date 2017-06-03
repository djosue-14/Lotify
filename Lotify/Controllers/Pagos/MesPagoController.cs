using Lotify.Models;
using Lotify.Models.Pagos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lotify.Controllers.Pagos
{
    [Authorize]
    public class MesPagoController : Controller
    {
        // GET: MesPagos
        private ApplicationDbContext dbCtx;
        private MesPago MesPago;

        public MesPagoController()
        {
            dbCtx = new ApplicationDbContext();
            MesPago = new MesPago();
        }

        // GET: 
        public ActionResult Index()
        {
            ViewBag.Title = "Mes Pago";
            var lista = dbCtx.MesPago.ToList();
            return View();
        }

        [HttpGet]
        public JsonResult Show()
        {
            var listaMes = dbCtx.MesPago.Select(c => new
            {
                c.Id,
                c.NombreMes
            });

            return Json(listaMes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ShowId(int id)
        {
            var mes = dbCtx.MesPago.FirstOrDefault(a => a.Id == id);

            return Json(mes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Agregar Mes";
            return View();
        }

        [HttpPost]
        public ActionResult Create(MesPagoViewModels model)
        {

            if (ModelState.IsValid)
            {
                MesPago.NombreMes = model.NombreMes;
                dbCtx.MesPago.Add(MesPago);
                dbCtx.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Editar Mes";

            MesPagoViewModels mes = new MesPagoViewModels();

            MesPago = dbCtx.MesPago.FirstOrDefault(a => a.Id == id);
            mes.Id = MesPago.Id;
            mes.NombreMes = MesPago.NombreMes;

            return View(mes);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(MesPagoViewModels model)
        {
            if (ModelState.IsValid)
            {
                MesPago = dbCtx.MesPago.FirstOrDefault(a => a.Id == model.Id);
                MesPago.NombreMes = model.NombreMes;
                dbCtx.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(MesPagoViewModels model)
        {
            var mes = (from p in dbCtx.MesPago
                          where p.Id == model.Id
                          select p).FirstOrDefault();

            dbCtx.MesPago.Remove(mes);

            int num = dbCtx.SaveChanges();

            if (num > 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
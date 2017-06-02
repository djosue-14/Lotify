using Lotify.Models;
using Lotify.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lotify.Controllers.Ventas
{
    public class TipoFinanciamientoController : Controller
    {
        // GET: TipoPago
        private ApplicationDbContext dbCtx;
        private TipoFinanciamiento TipoFinanciamiento;

        public TipoFinanciamientoController()
        {
            dbCtx = new ApplicationDbContext();
            TipoFinanciamiento = new TipoFinanciamiento();
        }

        // GET: 
        public ActionResult Index()
        {
            ViewBag.Title = "Tipo Financiamiento";
            var lista = dbCtx.TipoFinanciamiento.ToList();
            return View();
        }

        [HttpGet]
        public JsonResult Show()
        {
            var listaFinanziamiento = dbCtx.TipoFinanciamiento.Select(c => new
            {
                c.Id,
                c.Plazo
            });

            return Json(listaFinanziamiento, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ShowId(int id)
        {
            var tipo = dbCtx.TipoFinanciamiento.FirstOrDefault(a => a.Id == id);

            return Json(tipo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Agregar Tipo de Financiamiento";
            return View();
        }

        [HttpPost]
        public ActionResult Create(TipoFinanciamientoViewModels model)
        {

            if (ModelState.IsValid)
            {
                TipoFinanciamiento.Plazo = model.Plazo;
                dbCtx.TipoFinanciamiento.Add(TipoFinanciamiento);
                dbCtx.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Editar Tipo de Financiamiento";

            TipoFinanciamientoViewModels tipo = new TipoFinanciamientoViewModels();

            TipoFinanciamiento = dbCtx.TipoFinanciamiento.FirstOrDefault(a => a.Id == id);
            tipo.Id = TipoFinanciamiento.Id;
            tipo.Plazo = TipoFinanciamiento.Plazo;

            return View(tipo);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(TipoFinanciamientoViewModels model)
        {
            if (ModelState.IsValid)
            {
                TipoFinanciamiento = dbCtx.TipoFinanciamiento.FirstOrDefault(a => a.Id == model.Id);
                TipoFinanciamiento.Plazo = model.Plazo;
                dbCtx.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(TipoFinanciamientoViewModels model)
        {
            var tipo = (from p in dbCtx.TipoFinanciamiento
                       where p.Id == model.Id
                       select p).FirstOrDefault();

            dbCtx.TipoFinanciamiento.Remove(tipo);

            int num = dbCtx.SaveChanges();

            if (num > 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
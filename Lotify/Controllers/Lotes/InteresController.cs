using Lotify.Models;
using Lotify.Models.Lotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lotify.Controllers.Lotes
{
    [Authorize]
    public class InteresController : Controller
    {
        // GET: Area
        // GET: EstadoLote
        private ApplicationDbContext dbCtx;
        private Interes interes;

        public InteresController()
        {
            dbCtx = new ApplicationDbContext();
            interes = new Interes();
        }

        // GET: EstadoCliente
        public ActionResult Index()
        {
            ViewBag.Title = "Interes";
            var lista = dbCtx.Interes.ToList();
            return View();
        }

        [HttpGet]
        public JsonResult Show()
        {
            var lista = dbCtx.Interes.Select(c => new
            {
                c.Id,
                c.TasaInteres
            });

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ShowId(int id)
        {
            var interes = dbCtx.Interes.FirstOrDefault(a => a.Id == id);

            return Json(interes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Agregar Interes";
            return View();
        }

        [HttpPost]
        public ActionResult Create(InteresViewModels model)
        {

            if (ModelState.IsValid)
            {
                interes.TasaInteres = model.TasaInteres;
                dbCtx.Interes.Add(interes);
                dbCtx.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Editar Interes";

            InteresViewModels model = new InteresViewModels();

            interes = dbCtx.Interes.FirstOrDefault(a => a.Id == id);
            model.Id = interes.Id;
            model.TasaInteres = interes.TasaInteres;

            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(InteresViewModels model)
        {
            if (ModelState.IsValid)
            {
                interes = dbCtx.Interes.FirstOrDefault(a => a.Id == model.Id);
                interes.TasaInteres = model.TasaInteres;
                dbCtx.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(InteresViewModels model)
        {
            var inter = (from p in dbCtx.Interes
                        where p.Id == model.Id
                        select p).FirstOrDefault();

            dbCtx.Interes.Remove(inter);

            int num = dbCtx.SaveChanges();

            if (num > 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
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
    public class MedidaController : Controller
    {
        // GET: Medida
        private ApplicationDbContext dbCtx;
        private Medida Medida;

        public MedidaController()
        {
            dbCtx = new ApplicationDbContext();
            Medida = new Medida();
        }


        public ActionResult Index()
        {
            ViewBag.Title = "Medida";

            var lista = dbCtx.Medida.ToList();
            return View();
        }

        [HttpGet]
        public JsonResult Show()
        {
            var listaMedida = dbCtx.Medida.Select(c => new
            {
                c.Id,
                c.Ancho,
                c.Largo
            });

            return Json(listaMedida, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ShowId(int id)
        {
            var Medida = dbCtx.Medida.FirstOrDefault(a => a.Id == id);

            return Json(Medida, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Agregar Medidas";
            return View();
        }

        [HttpPost]
        public ActionResult Create(MedidaViewModels model)
        {

            if (ModelState.IsValid)
            {
                Medida.Ancho = model.Ancho;
                Medida.Largo = model.Largo;
                dbCtx.Medida.Add(Medida);
                dbCtx.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Editar Medida";

            MedidaViewModels medida = new MedidaViewModels();

            Medida = dbCtx.Medida.FirstOrDefault(a => a.Id == id);
            medida.Id = Medida.Id;
            medida.Ancho = Medida.Ancho;
            medida.Largo = Medida.Largo;

            return View(medida);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(MedidaViewModels model)
        {
            if (ModelState.IsValid)
            {
                Medida = dbCtx.Medida.FirstOrDefault(a => a.Id == model.Id);
                Medida.Ancho = model.Ancho;
                Medida.Largo = model.Largo;
                dbCtx.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(MedidaViewModels model)
        {
            var medida = (from p in dbCtx.Medida
                          where p.Id == model.Id
                          select p).FirstOrDefault();

            dbCtx.Medida.Remove(medida);

            int num = dbCtx.SaveChanges();

            if (num > 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
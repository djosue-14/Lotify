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
    public class ManzanaController : Controller
    {
         // GET: EstadoLote
        private ApplicationDbContext dbCtx;
        private Manzana Manzana;

        public ManzanaController()
        {
            dbCtx = new ApplicationDbContext();
            Manzana = new Manzana();
        }

        // GET: EstadoCliente
        public ActionResult Index()
        {
            ViewBag.Title = "Manzana";
            var lista = dbCtx.Manzana.ToList();
            return View();
        }

        [HttpGet]
        public JsonResult Show()
        {
            var listaManzana = dbCtx.Manzana.Select(c => new
            {
                c.Id,
                c.NombreManzana
            });

            return Json(listaManzana, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ShowId(int id)
        {
            var manzana = dbCtx.Manzana.FirstOrDefault(a => a.Id == id);
         
            return Json(manzana, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Agregar Manzana";
            return View();
        }

        [HttpPost]
        public ActionResult Create(ManzanaViewModels model)
        {

            if (ModelState.IsValid)
            {
                Manzana.NombreManzana = model.NombreManzana;
                dbCtx.Manzana.Add(Manzana);
                dbCtx.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Editar Manzana";

            ManzanaViewModels manzana = new ManzanaViewModels();

           Manzana = dbCtx.Manzana.FirstOrDefault(a => a.Id == id);
            manzana.Id = Manzana.Id;
            manzana.NombreManzana = Manzana.NombreManzana;

            return View(manzana);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(ManzanaViewModels model)
        {
            if (ModelState.IsValid)
            {
                Manzana = dbCtx.Manzana.FirstOrDefault(a => a.Id == model.Id);
                Manzana.NombreManzana = model.NombreManzana;
                dbCtx.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(ManzanaViewModels model)
        {
            var manzana = (from p in dbCtx.Manzana
                          where p.Id == model.Id
                          select p).FirstOrDefault();

            dbCtx.Manzana.Remove(manzana);

            int num = dbCtx.SaveChanges();

            if (num > 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }
    }



}
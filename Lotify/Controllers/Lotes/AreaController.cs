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
    public class AreaController : Controller
    {
        // GET: Area
        // GET: EstadoLote
        private ApplicationDbContext dbCtx;
        private Area Area;

        public AreaController()
        {
            dbCtx = new ApplicationDbContext();
            Area = new Area();
        }

        // GET: EstadoCliente
        public ActionResult Index()
        {
            ViewBag.Title = "Area";
            var lista = dbCtx.Area.ToList();
            return View();
        }

        [HttpGet]
        public JsonResult Show()
        {
            var listaArea = dbCtx.Area.Select(c => new
            {
                c.Id,
                c.NombreArea
            });

            return Json(listaArea, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ShowId(int id)
        {
            var area = dbCtx.Area.FirstOrDefault(a => a.Id == id);

            return Json(area, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Agregar Area";
            return View();
        }

        [HttpPost]
        public ActionResult Create(AreaViewModels model)
        {

            if (ModelState.IsValid)
            {
                Area.NombreArea = model.NombreArea;
                dbCtx.Area.Add(Area);
                dbCtx.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Editar Area";

            AreaViewModels area = new AreaViewModels();

            Area = dbCtx.Area.FirstOrDefault(a => a.Id == id);
            area.Id = Area.Id;
            area.NombreArea = Area.NombreArea;

            return View(area);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(AreaViewModels model)
        {
            if (ModelState.IsValid)
            {
                Area = dbCtx.Area.FirstOrDefault(a => a.Id == model.Id);
                Area.NombreArea = model.NombreArea;
                dbCtx.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(AreaViewModels model)
        {
            var area = (from p in dbCtx.Area
                           where p.Id == model.Id
                           select p).FirstOrDefault();

            dbCtx.Area.Remove(area);

            int num = dbCtx.SaveChanges();

            if (num > 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
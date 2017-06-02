using Lotify.Models;
using Lotify.Models.Telefonos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lotify.Controllers.Telefonos
{
    public class CompaniaTelefonoController : Controller
    {
        // GET: CompaniaTelefono
        private ApplicationDbContext dbCtx;
        private CompaniaTelefono CompaniaTelefono;

        public CompaniaTelefonoController()
        {
            dbCtx = new ApplicationDbContext();
            CompaniaTelefono = new CompaniaTelefono();
        }

        // GET: 
        public ActionResult Index()
        {
            ViewBag.Title = "Compania Telefono";
            var lista = dbCtx.CompaniaTelefono.ToList();
            return View();
        }

        [HttpGet]
        public JsonResult Show()
        {
            var listaCompania = dbCtx.CompaniaTelefono.Select(c => new
            {
                c.Id,
                c.NombreCompania
            });

            return Json(listaCompania, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ShowId(int id)
        {
            var compania = dbCtx.CompaniaTelefono.FirstOrDefault(a => a.Id == id);

            return Json(compania, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Agregar Compania";
            return View();
        }

        [HttpPost]
        public ActionResult Create(CompaniaTelefonoViewModels model)
        {

            if (ModelState.IsValid)
            {
                CompaniaTelefono.NombreCompania = model.NombreCompania;
                dbCtx.CompaniaTelefono.Add(CompaniaTelefono);
                dbCtx.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Editar Compania";

            CompaniaTelefonoViewModels compania = new CompaniaTelefonoViewModels();

            CompaniaTelefono = dbCtx.CompaniaTelefono.FirstOrDefault(a => a.Id == id);
            compania.Id = CompaniaTelefono.Id;
            compania.NombreCompania = CompaniaTelefono.NombreCompania;

            return View(compania);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(CompaniaTelefonoViewModels model)
        {
            if (ModelState.IsValid)
            {
                CompaniaTelefono = dbCtx.CompaniaTelefono.FirstOrDefault(a => a.Id == model.Id);
                CompaniaTelefono.NombreCompania = model.NombreCompania;
                dbCtx.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(CompaniaTelefonoViewModels model)
        {
            var compania = (from p in dbCtx.CompaniaTelefono
                          where p.Id == model.Id
                          select p).FirstOrDefault();

            dbCtx.CompaniaTelefono.Remove(compania);

            int num = dbCtx.SaveChanges();

            if (num > 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
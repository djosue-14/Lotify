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
    public class EstadoLoteController : Controller
    {
        // GET: EstadoLote
        private ApplicationDbContext dbCtx;
        private EstadoLote EstadoLote;

        public EstadoLoteController()
        {
            dbCtx = new ApplicationDbContext();
            EstadoLote = new EstadoLote();
        }

        // GET: EstadoCliente
        public ActionResult Index()
        {
            ViewBag.Title = "Estado Lote";

            var lista = dbCtx.EstadoLote.ToList();
            return View();
        }

        [HttpGet]
        public JsonResult Show()
        {
            var listaEstado = dbCtx.EstadoLote.Select(c => new
            {
                c.Id,
                c.NombreEstado
            });

            return Json(listaEstado, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ShowId(int id)
        {
            var estado = dbCtx.EstadoLote.FirstOrDefault(a => a.Id == id);
         
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Agregar Estado";
            return View();
        }

        [HttpPost]
        public ActionResult Create(EstadoLoteViewModels model)
        {

            if (ModelState.IsValid)
            {
                EstadoLote.NombreEstado = model.NombreEstado;
                dbCtx.EstadoLote.Add(EstadoLote);
                dbCtx.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Editar Estado";

            EstadoLoteViewModels estado = new EstadoLoteViewModels();

            EstadoLote = dbCtx.EstadoLote.FirstOrDefault(a => a.Id == id);
            estado.Id = EstadoLote.Id;
            estado.NombreEstado = EstadoLote.NombreEstado;

            return View(estado);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(EstadoLoteViewModels model)
        {
            if (ModelState.IsValid)
            {
                EstadoLote = dbCtx.EstadoLote.FirstOrDefault(a => a.Id == model.Id);
                EstadoLote.NombreEstado = model.NombreEstado;
                dbCtx.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(EstadoLoteViewModels model)
        {
            var estado = (from p in dbCtx.EstadoLote
                          where p.Id == model.Id
                          select p).FirstOrDefault();

            dbCtx.EstadoLote.Remove(estado);

            int num = dbCtx.SaveChanges();

            if (num > 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
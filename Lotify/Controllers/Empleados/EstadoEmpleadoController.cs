using Lotify.Models;
using Lotify.Models.Empleados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lotify.Controllers.Empleados
{
    public class EstadoEmpleadoController : Controller
    {
        // GET: EstadoEmpleado
        private ApplicationDbContext dbCtx;
        private EstadoEmpleado EstadoEmpleado;

        public EstadoEmpleadoController()
        {
            dbCtx = new ApplicationDbContext();
            EstadoEmpleado = new EstadoEmpleado();
        }

        // GET: 
        public ActionResult Index()
        {
            ViewBag.Title = "Estado Empleados";

            var lista = dbCtx.EstadoEmpleado.ToList();
            return View();
        }

        [HttpGet]
        public JsonResult Show()
        {
            var listaEstado = dbCtx.EstadoEmpleado.Select(c => new
            {
                c.Id,
                c.NombreEstado
            });

            return Json(listaEstado, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ShowId(int id)
        {
            var estado = dbCtx.EstadoEmpleado.FirstOrDefault(a => a.Id == id);
            
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Agregar Estado";
            return View();
        }

        [HttpPost]
        public ActionResult Create(EstadoEmpleadoViewModels model)
        {

            if (ModelState.IsValid)
            {
                EstadoEmpleado.NombreEstado = model.NombreEstado;
                dbCtx.EstadoEmpleado.Add(EstadoEmpleado);
                dbCtx.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Editar Estado";

            EstadoEmpleadoViewModels estado = new EstadoEmpleadoViewModels();

            EstadoEmpleado = dbCtx.EstadoEmpleado.FirstOrDefault(a => a.Id == id);
            estado.Id = EstadoEmpleado.Id;
            estado.NombreEstado = EstadoEmpleado.NombreEstado;

            return View(estado);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(EstadoEmpleadoViewModels model)
        {
            if (ModelState.IsValid)
            {
                EstadoEmpleado = dbCtx.EstadoEmpleado.FirstOrDefault(a => a.Id == model.Id);
                EstadoEmpleado.NombreEstado = model.NombreEstado;
                dbCtx.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(EstadoEmpleadoViewModels model)
        {
            var estado = (from p in dbCtx.EstadoEmpleado
                          where p.Id == model.Id
                          select p).FirstOrDefault();

            dbCtx.EstadoEmpleado.Remove(estado);

            int num = dbCtx.SaveChanges();

            if (num > 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
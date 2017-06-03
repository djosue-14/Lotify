using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lotify.Models.Clientes;
using Lotify.Models;

namespace Lotify.Controllers.Clientes
{
    [Authorize]
    public class EstadoClienteController : Controller
    {
        private ApplicationDbContext dbCtx;
        private EstadoCliente EstadoCliente;

        public EstadoClienteController()
        {
            dbCtx = new ApplicationDbContext();
            EstadoCliente = new EstadoCliente();
        }

        // GET: EstadoCliente
        public ActionResult Index()
        {
            ViewBag.Title = "Estado Clientes";

            var lista = dbCtx.EstadoCliente.ToList();
            return View();
        }

        [HttpGet]
        public JsonResult Show()
        {
            var listaEstados = dbCtx.EstadoCliente.Select(c => new
            {
                c.Id,
                c.NombreEstado
            });

            return Json(listaEstados, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ShowId(int id)
        {
            var estado = dbCtx.EstadoCliente.FirstOrDefault(a => a.Id == id);
            //var listaEstados = dbCtx.EstadoCliente.Find(Id);
            //var estado = (from p in dbCtx.EstadoCliente where p.Id == Id select p).FirstOrDefault();
            //var estado = dbCtx.EstadoCliente.Where(p => p.Id == id);

            return Json(estado, JsonRequestBehavior.AllowGet);
        }  

        public ActionResult Create()
        {
            ViewBag.Title = "Agregar Estado";
            return View();
        }

        [HttpPost]
        public ActionResult Create(EstadoClienteViewModels model)
        {

            if (ModelState.IsValid)
            {
                EstadoCliente.NombreEstado = model.NombreEstado;
                dbCtx.EstadoCliente.Add(EstadoCliente);
                dbCtx.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Editar Estado";

            EstadoClienteViewModels estado = new EstadoClienteViewModels();

            EstadoCliente = dbCtx.EstadoCliente.FirstOrDefault(a => a.Id == id);
            estado.Id = EstadoCliente.Id;
            estado.NombreEstado = EstadoCliente.NombreEstado;

            return View(estado);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(EstadoClienteViewModels model)
        {
            if (ModelState.IsValid)
            {
                EstadoCliente = dbCtx.EstadoCliente.FirstOrDefault(a => a.Id == model.Id);
                EstadoCliente.NombreEstado = model.NombreEstado;
                dbCtx.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(EstadoClienteViewModels model)
        {
            var estado = (from p in dbCtx.EstadoCliente where p.Id == model.Id
                        select p).FirstOrDefault();

            dbCtx.EstadoCliente.Remove(estado);
          
            int num = dbCtx.SaveChanges();

            if (num > 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
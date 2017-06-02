using Lotify.Models;
using Lotify.Models.Empleados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lotify.Controllers.Empleados
{
    public class CargoEmpleadoController : Controller
    {
        // GET: CargoEmpleados
        // GET: EstadoEmpleado
        private ApplicationDbContext dbCtx;
        private CargoEmpleado CargoEmpleado;

        public CargoEmpleadoController()
        {
            dbCtx = new ApplicationDbContext();
            CargoEmpleado = new CargoEmpleado();
        }

        // GET: 
        public ActionResult Index()
        {
            ViewBag.Title = "Cargo Empleados";

            var lista = dbCtx.CargoEmpleado.ToList();
            return View();
        }

        [HttpGet]
        public JsonResult Show()
        {
            var listaCargo = dbCtx.CargoEmpleado.Select(c => new
            {
                c.Id,
                c.NombreCargo,
                c.Sueldo
            });

            return Json(listaCargo, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ShowId(int id)
        {
            var cargo = dbCtx.CargoEmpleado.FirstOrDefault(a => a.Id == id);

            return Json(cargo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Agregar Cargo";
            return View();
        }

        [HttpPost]
        public ActionResult Create(CargoEmpleadoViewModels model)
        {

            if (ModelState.IsValid)
            {
                CargoEmpleado.NombreCargo = model.NombreCargo;
                CargoEmpleado.Sueldo = model.Sueldo;
                dbCtx.CargoEmpleado.Add(CargoEmpleado);
                dbCtx.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Editar Cargo";

            CargoEmpleadoViewModels cargo = new CargoEmpleadoViewModels();

            CargoEmpleado = dbCtx.CargoEmpleado.FirstOrDefault(a => a.Id == id);
            cargo.Id = CargoEmpleado.Id;
            cargo.NombreCargo = CargoEmpleado.NombreCargo;
            cargo.Sueldo = CargoEmpleado.Sueldo;

            return View(cargo);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(CargoEmpleadoViewModels model)
        {
            if (ModelState.IsValid)
            {
                CargoEmpleado = dbCtx.CargoEmpleado.FirstOrDefault(a => a.Id == model.Id);
                CargoEmpleado.NombreCargo = model.NombreCargo;
                CargoEmpleado.Sueldo = model.Sueldo;
                dbCtx.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(CargoEmpleadoViewModels model)
        {
            var cargo = (from p in dbCtx.CargoEmpleado
                          where p.Id == model.Id
                          select p).FirstOrDefault();

            dbCtx.CargoEmpleado.Remove(cargo);

            int num = dbCtx.SaveChanges();

            if (num > 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
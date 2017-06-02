using Lotify.Models;
using Lotify.Models.Lotes;
using Lotify.Models.Telefonos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lotify.Controllers.Lotes
{
    public class LotificadoraController : Controller
    {
        // GET: Lotificadora
        // GET: Area
        // GET: EstadoLote
        private ApplicationDbContext dbCtx;
        private Lotificadora Lotificadora;
        private TelefonoLotificadora TelLoti;
        //private object lotificadora;
        //private object lotificadoras;

        public LotificadoraController()
        {
            dbCtx = new ApplicationDbContext();
            Lotificadora = new Lotificadora();
        }

        // GET: EstadoCliente
        public ActionResult Index()
        {
            ViewBag.Title = "Lotificadora";

            var lista = dbCtx.Lotificadora.ToList();
            return View(lista);
        }

        [HttpGet]
        public JsonResult Show()
        {
            var listaLotificadora = dbCtx.Lotificadora.Select(c => new
            {
                c.Id,
                c.NombreLotificadora,
                c.Direccion
            });

            return Json(listaLotificadora, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ShowId(int id)
        {
            var Lotificadora = dbCtx.Lotificadora.FirstOrDefault(a => a.Id == id);

            return Json(Lotificadora, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Agregar Lotificadora";

            LotificadoraViewModels LotificadoraTelefono = new LotificadoraViewModels();

            LotificadoraTelefono.Companias = dbCtx.CompaniaTelefono.ToList();

            return View(LotificadoraTelefono);
        }

        [HttpPost]
        public ActionResult Create(LotificadoraViewModels model)
        {

            if (ModelState.IsValid)
            {
                Lotificadora.NombreLotificadora = model.NombreLotificadora;
                Lotificadora.Direccion = model.Direccion;
                dbCtx.Lotificadora.Add(Lotificadora);
                dbCtx.SaveChanges();

                TelefonoLotificadora telefono = new TelefonoLotificadora();
                telefono.NumeroTelefono = model.NumeroTelefono;
                telefono.CompaniaTelefonoId = model.CompaniaTelefonoId;
                telefono.LotificadoraId = Lotificadora.Id;

                dbCtx.TelefonoLotificadora.Add(telefono);
                dbCtx.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Editar Lotificadora";

            LotificadoraViewModels lotificadora = new LotificadoraViewModels();

            Lotificadora = dbCtx.Lotificadora.FirstOrDefault(a => a.Id == id);
            lotificadora.Id = Lotificadora.Id;
            lotificadora.NombreLotificadora = Lotificadora.NombreLotificadora;
            lotificadora.Direccion = Lotificadora.Direccion;

            return View(lotificadora);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(LotificadoraViewModels model)
        {
            if (ModelState.IsValid)
            {
                Lotificadora = dbCtx.Lotificadora.FirstOrDefault(a => a.Id == model.Id);
                Lotificadora.NombreLotificadora= model.NombreLotificadora;
                Lotificadora.Direccion = model.Direccion;
                dbCtx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        //desde aka----------------------------------------------------------------
        public ActionResult EditTel(int id)
        {
            ViewBag.Title = "Editar Numero";

            TelefonoLotificadoraViewModels tellotificadora = new TelefonoLotificadoraViewModels();

            TelLoti = dbCtx.TelefonoLotificadora.FirstOrDefault(a => a.LotificadoraId == id);
            tellotificadora.NumeroTelefono = TelLoti.NumeroTelefono;
            tellotificadora.CompaniaTelefonoId = TelLoti.CompaniaTelefonoId;

            tellotificadora.Companias = dbCtx.CompaniaTelefono.ToList();

            return View(tellotificadora);
        }

        [HttpPost, ActionName("EditTel")]
        public ActionResult EditTel(TelefonoLotificadoraViewModels model)
        {
            if (ModelState.IsValid)
            {
                TelLoti = dbCtx.TelefonoLotificadora.FirstOrDefault(a => a.Id == model.Id);
                TelLoti.NumeroTelefono = model.NumeroTelefono;
                TelLoti.CompaniaTelefonoId = model.CompaniaTelefonoId;

                dbCtx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(AreaViewModels model)
        {
            var lotificadora = (from p in dbCtx.Lotificadora
                        where p.Id == model.Id
                        select p).FirstOrDefault();

            dbCtx.Lotificadora.Remove(lotificadora); 

            int num = dbCtx.SaveChanges();

            if (num > 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
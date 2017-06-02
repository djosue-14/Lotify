using Lotify.Models;
using Lotify.Models.Clientes;
using Lotify.Models.Telefonos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lotify.Controllers.Clientes
{
    public class ClienteController : Controller
    {
        private ApplicationDbContext dbCtx;
        private Cliente cliente;
        private TelefonoCliente TelCliente;

        public ClienteController()
        {
            dbCtx = new ApplicationDbContext();
            cliente = new Cliente();
        }

        // GET: EstadoCliente
        public ActionResult Index()
        {
            ViewBag.Title = "Clientes";
            var lista = dbCtx.Cliente.ToList();
            return View(lista);
        }

        [HttpGet]
        public JsonResult Show()
        {

            var clientes =  dbCtx.Cliente.Select(c => new {
                                c.Id, c.Nombre, c.Apellido, c.Dpi, c.Genero, c.Direccion, c.FechaNacimiento,
                                EstadoCliente = new {
                                    c.EstadoCliente.Id,
                                    c.EstadoCliente.NombreEstado,
                                }
                            });

            return Json(clientes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ShowId(int id)
        {
            var cliente = dbCtx.Cliente.FirstOrDefault(p => p.Id == id);

            return Json(cliente, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Agregar Cliente";
            ClienteViewModels ClienteTelefono = new ClienteViewModels();

            ClienteTelefono.Companias = dbCtx.CompaniaTelefono.ToList();
            
            return View(ClienteTelefono);
        }

        [HttpPost]
        public ActionResult Create(ClienteViewModels model)
        {
            if (ModelState.IsValid)
            {
                cliente.Nombre = model.Nombre;
                cliente.Apellido = model.Apellido;
                cliente.Dpi = Convert.ToInt64(model.Dpi);
                cliente.Genero = model.Genero;
                cliente.Direccion = model.Direccion;
                cliente.FechaNacimiento = model.FechaNacimiento;

                //Seleccionamos el estad cliente que sea 'ACTIVO' y luego lo agregamos a FK EstadoClienteId
                EstadoCliente estado = dbCtx.EstadoCliente.FirstOrDefault(e => e.NombreEstado == "Activo");
                cliente.EstadoClienteId = estado.Id;

                dbCtx.Cliente.Add(cliente);
                dbCtx.SaveChanges();

                //int ClienteId = cliente.Id;

                TelefonoCliente telefono = new TelefonoCliente();
                telefono.NumeroTelefono = model.NumeroTelefono;
                telefono.CompaniaTelefonoId = model.CompaniaTelefonoId;
                telefono.ClienteId = cliente.Id;

                dbCtx.TelefonoCliente.Add(telefono);
                dbCtx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Editar Estado";

            ClienteViewModels model = new ClienteViewModels();

            cliente = dbCtx.Cliente.FirstOrDefault(a => a.Id == id);
            model.Nombre = cliente.Nombre;
            model.Apellido = cliente.Apellido;
            model.Dpi = cliente.Dpi;
            model.Genero = cliente.Genero;
            model.Direccion = cliente.Direccion;
            model.FechaNacimiento = cliente.FechaNacimiento;
            model.EstadoClienteId = cliente.EstadoClienteId;

            model.EstadoCliente = dbCtx.EstadoCliente.ToList();

            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(ClienteViewModels model)
        {
            if (ModelState.IsValid)
            {
                cliente = dbCtx.Cliente.FirstOrDefault(a => a.Id == model.Id);
                cliente.Nombre = model.Nombre;
                cliente.Apellido = model.Apellido;
                cliente.Dpi = model.Dpi;
                cliente.Genero = model.Genero;
                cliente.Direccion = model.Direccion;
                cliente.FechaNacimiento = model.FechaNacimiento;
                cliente.EstadoClienteId = model.EstadoClienteId;

                dbCtx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        //desde aka----------------------------------------------------------------
        public ActionResult EditTel(int id)
        {
            ViewBag.Title = "Editar Numero";

            TelefonoClienteViewModels telcli = new TelefonoClienteViewModels();

            TelCliente = dbCtx.TelefonoCliente.FirstOrDefault(a => a.ClienteId == id);
            telcli.NumeroTelefono = TelCliente.NumeroTelefono;
            telcli.CompaniaTelefonoId = TelCliente.CompaniaTelefonoId;

            telcli.Companias = dbCtx.CompaniaTelefono.ToList();
            return View(telcli);
        }

        [HttpPost, ActionName("EditTel")]
        public ActionResult EditTel(TelefonoClienteViewModels telcli)
        {
            if (ModelState.IsValid)
            {
                TelCliente = dbCtx.TelefonoCliente.FirstOrDefault(a => a.Id == telcli.Id);
                TelCliente.NumeroTelefono = telcli.NumeroTelefono;
                TelCliente.CompaniaTelefonoId = telcli.CompaniaTelefonoId;
                dbCtx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(ClienteViewModels model)
        {
            var estado = (from p in dbCtx.Cliente
                          where p.Id == model.Id
                          select p).FirstOrDefault();

            dbCtx.Cliente.Remove(estado);

            int num = dbCtx.SaveChanges();

            if (num > 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
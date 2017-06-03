using Lotify.Models;
using Lotify.Models.Lotes;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Lotify.Controllers.Lotes
{
    [Authorize]
    public class LoteController : Controller
    {

        private ApplicationDbContext dbCtx;
        private Lote Lotes;

        public LoteController()
        {
            dbCtx = new ApplicationDbContext();
            Lotes = new Lote();
        }
        
        // GET: EstadoCliente
        public ActionResult Index()
        {
            ViewBag.Title = "Lotes";

            return View();
        }

        [HttpGet]
        public JsonResult Show()
        {

            var Lotes = dbCtx.Lote.Select(c => new {
                c.Id,
                c.Precio,
                Medida = new {
                    c.Medida.Id,
                    c.Medida.Ancho,
                    c.Medida.Largo,
                },
                EstadoLote = new {
                    c.EstadoLote.Id,
                    c.EstadoLote.NombreEstado,
                },
                Lotificadora = new {
                    c.Lotificadora.Id,
                    c.Lotificadora.NombreLotificadora,
                    c.Lotificadora.Direccion,
                },
                Manzana = new {
                    c.Manzana.Id,
                    c.Manzana.NombreManzana,
                },
                Area = new {
                    c.Area.Id,
                    c.Area.NombreArea,
                },
               
            });

            return Json(Lotes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ShowId(int id)
        {
            var Lotes = dbCtx.Lote.FirstOrDefault(p => p.Id == id);

            return Json(Lotes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Title = "Ingresar Lotes";

            LoteViewModels model = new LoteViewModels();

            model.Medidas = dbCtx.Medida.ToList();
            model.EstadoLote = dbCtx.EstadoLote.ToList();
            model.Lotificadora = dbCtx.Lotificadora.ToList();
            model.Area = dbCtx.Area.ToList();
            model.Manzana = dbCtx.Manzana.ToList();
            model.Interes = dbCtx.Interes.ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(LoteViewModels model)
        {
            Random random = new Random();
            int numero = random.Next(1, 5);

            //var validImageTypes = new string[]
            //{
            //    "image/gif",
            //    "image/jpeg",
            //    "image/pjpeg",
            //    "image/png",
            //};

            //if (model.ImageUpload == null || model.ImageUpload.ContentLength == 0)
            //{
            //    ModelState.AddModelError("ImageUpload", "Es requerido.");
            //}
            //else if (!validImageTypes.Contains(model.ImageUpload.ContentType))
            //{
            //    ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
            //}

            if (ModelState.IsValid)
            {
                Lotes.Precio = model.Precio;
                Lotes.MedidaId = model.MedidaId;
           
                //Seleccionamos el estad cliente que sea 'ACTIVO' y luego lo agregamos a FK EstadoClienteId
                EstadoLote estado = dbCtx.EstadoLote.FirstOrDefault(e => e.NombreEstado == "Disponible");
                Lotes.EstadoLoteId = estado.Id;

                Lotes.LotificadoraId = model.LotificadoraId;
                Lotes.ManzanaId = model.ManzanaId;
                Lotes.AreaId = model.AreaId;
                Lotes.InteresId = model.InteresId;
                Lotes.ImageUrl = "../../Images/Lotes/lote" + numero + ".png";

                //if(model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                //{
                //    //Invertimos el nombre de la imagen y tomamos la extension de esta
                //    //string extension = Reverse(model.ImageUpload.FileName).Split('.')[0];
                //    //extension = Reverse(extension); //invertimos nuevamente la cadena generada en la linea anterior
                //    //string nameImage;

                //    //string time = DateTime.UtcNow.ToString();
                //    //time = time.Replace("/", "_");
                //    //time = time.Replace(" ", "_");
                //    //time = time.Replace(":", "_");
                //    //time = time.Replace(".", "");

                //    //Le damos un nuevo nombre a la imagen, en base al fecha y hora para que no se repitan urls.
                //    //nameImage = time + "." + extension;

                //    string uploadDir = "~/Images/Lotes/"; //Ruta donde se guardara.
                //    //var imagePath = Path.Combine(Server.MapPath(uploadDir), nameImage);//generamos el path
                //    var imagePath = Path.Combine(Server.MapPath(uploadDir), model.ImageUpload.FileName);

                //    //generamos la url a guardar en la base de datos.
                //    var imageUrl = Path.Combine(uploadDir, model.ImageUpload.FileName);
                //    model.ImageUpload.SaveAs(imagePath);//guardamos la imagen.

                //    Lotes.ImageUrl = imageUrl;//por ultimo la pasamos la url al modelo para ser guardada en la BD.
                //}

                dbCtx.Lote.Add(Lotes);
                dbCtx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Editar Lotes";

            LoteViewModels model = new LoteViewModels();

            Lotes = dbCtx.Lote.FirstOrDefault(a => a.Id == id);
            model.Precio = Lotes.Precio ;
            model.MedidaId = Lotes.MedidaId;
            model.EstadoLoteId = Lotes.EstadoLoteId;
            model.LotificadoraId = Lotes.LotificadoraId;
            model.ManzanaId = Lotes.ManzanaId;
            model.AreaId = Lotes.AreaId;

            model.Medidas = dbCtx.Medida.ToList();
            model.EstadoLote = dbCtx.EstadoLote.ToList();
            model.Lotificadora = dbCtx.Lotificadora.ToList();
            model.Area = dbCtx.Area.ToList();
            model.Manzana = dbCtx.Manzana.ToList();
            model.Interes = dbCtx.Interes.ToList();

            dbCtx.SaveChanges();

            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(LoteViewModels model)
        {
            if (ModelState.IsValid)
            {
                Lotes = dbCtx.Lote.FirstOrDefault(a => a.Id == model.Id);
                Lotes.Precio = model.Precio;

                Lotes.MedidaId = model.MedidaId;
                Lotes.EstadoLoteId = model.EstadoLoteId;
                Lotes.LotificadoraId = model.LotificadoraId;
                Lotes.ManzanaId = model.ManzanaId;
                Lotes.AreaId = model.AreaId;
                Lotes.InteresId = model.InteresId;

                dbCtx.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(LoteViewModels model)
        {
            var estado = (from p in dbCtx.Lote where p.Id == model.Id select p).FirstOrDefault();

            dbCtx.Lote.Remove(estado);

            int num = dbCtx.SaveChanges();

            if (num > 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        public JsonResult ShowDisponibles()
        {
            var lotes = dbCtx.Lote.Select(c => new {
                c.Id,
                c.Precio,
                c.ImageUrl,
                Medida = new
                {
                    c.Medida.Id,
                    c.Medida.Ancho,
                    c.Medida.Largo,
                },
                EstadoLote = new
                {
                    c.EstadoLote.Id,
                    c.EstadoLote.NombreEstado,
                },
                Lotificadora = new
                {
                    c.Lotificadora.Id,
                    c.Lotificadora.NombreLotificadora,
                    c.Lotificadora.Direccion,
                },
                Manzana = new
                {
                    c.Manzana.Id,
                    c.Manzana.NombreManzana,
                },
                Area = new
                {
                    c.Area.Id,
                    c.Area.NombreArea,
                },
                Interes = new
                {
                    c.Interes.Id,
                    c.Interes.TasaInteres,
                }
            }).Where(c => c.EstadoLote.NombreEstado == "Disponible");

            return Json(lotes,
                JsonRequestBehavior.AllowGet);
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
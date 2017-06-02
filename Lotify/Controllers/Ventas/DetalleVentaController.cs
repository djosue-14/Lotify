using Lotify.Models;
using Lotify.Models.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using Lotify.Models.Lotes;
using Lotify.Models.Clientes;

namespace Lotify.Controllers.Ventas
{
    public class DetalleVentaController : Controller
    {
        private ApplicationDbContext dbCtx;
        private DetalleVenta detalle;

        public DetalleVentaController()
        {
            dbCtx = new ApplicationDbContext();
            detalle = new DetalleVenta();
        }

        // GET: DetalleVenta
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReportePdf(int id)
        {
            Venta venta = dbCtx.Venta.FirstOrDefault(c => c.Id == id);

            string nombrePDF = "venta"+venta.Id + ".pdf";
            ExportarReportePDF(venta, Server.MapPath("../../Reportes/FacturaVenta/") + nombrePDF, "Factura Venta");

            return RedirectToAction("Index");
        }

        public void ExportarReportePDF(Venta venta, String rutaPDF, string tituloPDF)
        {
            //Buffer del archivo. ruta, crear, escribir y no compartir.
            System.IO.FileStream fs = new FileStream(rutaPDF, FileMode.Create, FileAccess.Write, FileShare.None);
            Document document = new Document(); //objeto de tipo Document de iTextSharp
            document.SetPageSize(iTextSharp.text.PageSize.A4);//Especifica el tamaño del papel.
            PdfWriter writer = PdfWriter.GetInstance(document, fs); //objeto de tipo PdfWriter de iTextSharp
            document.Open();

            /*********************************Inicio de Configuracion Encabezado****************************************/

            //Encabezado del Reporte PDF
            //Fuente Base.
            BaseFont fuenteBaseEncabezado = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fuentEncabezado = new Font(fuenteBaseEncabezado, 16, 1, BaseColor.GRAY);//Fuente del Encabezado.
            Paragraph tituloEncabezado = new Paragraph();
            tituloEncabezado.Alignment = Element.ALIGN_CENTER; //centrado
            tituloEncabezado.Add(new Chunk(tituloPDF.ToUpper(), fuentEncabezado));
            document.Add(tituloEncabezado);

            Cliente cliente = dbCtx.Cliente.FirstOrDefault(c => c.Id == venta.ClienteId);

            //Author
            Paragraph nombreAutor = new Paragraph();
            BaseFont fuenteBaseAutor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fuenteAutor = new Font(fuenteBaseAutor, 12, 2, BaseColor.GRAY);
            nombreAutor.Alignment = Element.ALIGN_LEFT;
            nombreAutor.Add(new Chunk("Nombre: "+cliente.Nombre +" "+cliente.Apellido, fuenteAutor));
            nombreAutor.Add(new Chunk("\nNIT: " + cliente.Dpi, fuenteAutor));
            nombreAutor.Add(new Chunk("\nFecha: "+venta.FechaVenta.ToShortDateString(), fuenteAutor));
            document.Add(nombreAutor);

            /*********************************Fin de Configuracion Encabezado****************************************/

            //Agrega linea para separar Header y Body.
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            document.Add(p);

            //Agregar Salto de Linea
            document.Add(new Chunk("\n", fuentEncabezado));

            string[] tituloColumna = new string[5] { "CANTIDAD", "DESCRIPCION", "CUOTAS", "TIEMPO", "SUBTOTAL" };
            //Write the table
            PdfPTable table = new PdfPTable(tituloColumna.Length);

            //Encabezado de la Tabla.
            BaseFont fuenteBaseEncabezadoTabla = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fuenteEncabezadoTabla = new Font(fuenteBaseEncabezadoTabla, 10, 1, BaseColor.WHITE);

            //Selects
            detalle = dbCtx.DetalleVenta.FirstOrDefault(c => c.VentaId == venta.Id);
            TipoFinanciamiento finan = dbCtx.TipoFinanciamiento.FirstOrDefault(c => c.Id == venta.TipoFinanciamientoId);
            Lote lote = dbCtx.Lote.FirstOrDefault(c => c.Id == detalle.LoteId);
            Medida medida = dbCtx.Medida.FirstOrDefault(c => c.Id == lote.MedidaId);

            decimal cuotas = venta.Total / Convert.ToDecimal(finan.Plazo);

            string[] filasTabla = new string[5]
                {
                    detalle.Cantidad.ToString(),
                    "Lote " + lote.Id + " De " + medida.Largo + " x " + medida.Ancho,
                    cuotas.ToString(),
                    venta.TipoFinanciamiento.Plazo.ToString(),
                    cuotas.ToString(),
                };

            for (int i = 0; i < tituloColumna.Length; i++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = BaseColor.GRAY;
                cell.AddElement(new Chunk(tituloColumna[i], fuenteEncabezadoTabla));
                table.AddCell(cell);
            }

            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < tituloColumna.Length; j++)
                {
                    table.AddCell(filasTabla[j]);
                }
            }
            document.Add(table);

            //Agregar Salto de Linea
            document.Add(new Chunk("\n", fuentEncabezado));

            BaseFont fuenteBaseFooter = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fuenteFooter = new Font(fuenteBaseFooter, 12, 1, BaseColor.BLACK);//Fuente del Encabezado.
            Paragraph textoFooter = new Paragraph();
            textoFooter.Alignment = Element.ALIGN_RIGHT; //centrado
            textoFooter.Add(new Chunk(
                "Total = (" + venta.TipoFinanciamiento.Plazo + " x " + cuotas.ToString() + ") = "+
                venta.Total + "             ", fuenteFooter));

            document.Add(textoFooter);

            document.Close();
            writer.Close();
            fs.Close();
        }

    }
}
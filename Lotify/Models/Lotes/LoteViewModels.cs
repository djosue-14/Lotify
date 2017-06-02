using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lotify.Models.Lotes
{
    public class LoteViewModels
    {

        public int Id { get; set; }

        //[Required]
        //[Display(Name = "numero de lote")]
        //public int NumeroLote { get; set; }

        [Required]
        [Display(Name = "Precio")]
        public decimal Precio { get; set; }

        [Required]
        [Display(Name = "medida")]
        //Foreign Key Para Medida
        public int MedidaId { get; set; }


        [Required]
        [Display(Name = "Nombre del Estado")]
        //Foreign Key para EstadoLote
        public int EstadoLoteId { get; set; }


        [Required]
        [Display(Name = "lotificadora")]
        //Foreign Key a Lotificadora
        public int LotificadoraId { get; set; }

        [Required]
        [Display(Name = "Manzana")]
        public int ManzanaId { get; set; }


        [Required]
        [Display(Name = "Area")]
        public int AreaId { get; set; }

        public string ImageUrl { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }

        [Required]
        [Display(Name = "Interes")]
        public int InteresId { get; set; }


        public List<Medida> Medidas { get; set; }

        public List<EstadoLote> EstadoLote { get; set; }

        public List<Lotificadora> Lotificadora { get; set; }
        public List<Manzana> Manzana { get; set; }
        public List<Area> Area { get; set; } 
        public List<Interes> Interes { get; set; }


    }
}
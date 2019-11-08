using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IreckonuFileHandler.Core.Resource
{
    public class ProductResource
    {
        [Display(Name = "Key", Order = 1)]
        public string Key { get; set; }
        [Display(Name = "ArtikelCode", Order = 2)]
        public string ArtikelCode { get; set; }
      
        [Display(Name = "ColorCode", Order = 3)]
        public string ColorCode { get; set; }
        [Display(Name = "Description", Order = 4)]
        public string Description { get; set; }
        [Display(Name = "Price", Order = 5)]
        public decimal Price { get; set; }
        [Display(Name = "DiscountPrice", Order = 6)]
        public decimal DiscountPrice { get; set; }
        [Display(Name = "DeliveredIn", Order = 7)]
        public string DeliveredIn { get; set; }
        [Display(Name = "Q1", Order = 8)]
        public string Q1 { get; set; }
        [Display(Name = "Size", Order = 9)]
        public int Size { get; set; }
        [Display(Name = "Color", Order = 10)]
        public string Color { get; set; }

    }
}

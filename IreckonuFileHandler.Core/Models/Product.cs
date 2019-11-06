using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IreckonuFileHandler.Core.Models
{
    public class Product
    {

        public int Id { get; set; }
             
        public string Key { get; set; }
        public string Code { get; set; }
        public string ColorCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public string ExpectedDelivery { get; set; }
        public string Q1 { get; set; }
        public int Size { get; set; }

        public string Color { get; set; }

        public int PathId { get; set; }
    }
}

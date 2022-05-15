using Core.Entities.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Product : BaseEntity, IEntity
    {
        public string Name { get;set;}
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}

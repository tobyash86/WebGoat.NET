using System;
using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
namespace WebGoatCore.Models
{
    public class Product
    {
        [Display(Name = "Product Id:")]
        public int ProductId { get; set; }
        [Display(Name = "Product Name:")]
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        [Display(Name = "Quantity per unit:")]
        public string QuantityPerUnit { get; set; }
        [Display(Name = "Unit price:")]
        public double UnitPrice { get; set; }
        [Display(Name = "Units in stock:")]
        public short UnitsInStock { get; set; }
        [Display(Name = "Units on order:")]
        public short UnitsOnOrder { get; set; }
        [Display(Name = "Reorder level:")]
        public short ReorderLevel { get; set; }
        [Display(Name = "Discontinued")]
        public bool Discontinued { get; set; }

        [Display(Name = "Category:")]
        public virtual Category Category { get; set; }
        [Display(Name = "Supplier:")]
        public virtual Supplier Supplier { get; set; }

        public decimal DecimalUnitPrice => Convert.ToDecimal(this.UnitPrice);
    }
}

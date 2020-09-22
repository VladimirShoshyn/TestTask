using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTask.Models
{
    public enum OrderStatus
    {
        [Display(Name ="In Progress")]
        InProgeress = 0,
        [Display(Name = "Complete")]
        Completed = 1
    }

    public class Order
    {
        [Key]        
        [Display(Name = "Odred number")]
        public int OrderID { get; set; }

        [Display(Name = "Order creation date")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Order status")]
        public OrderStatus OrderStatus { get; set; }

        public virtual IList<OrderRow> OrderRows { get; set; }

        public Order()
        {
            OrderDate = DateTime.Now;
            OrderStatus = OrderStatus.InProgeress;
            OrderRows = new List<OrderRow>();
        }
    }

    public class OrderRow
    {
        [Key]        
        public Guid OrderRowID { get; set; }

        [Display(Name = "Name")]
        public string ProductName { get; set; }

        [Display(Name = "Price")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ProductPrice { get; set; }

        [Display(Name = "Quantity")]
        public int ProductQuantity { get; set; }
                
        [Display(Name = "Order number")]
        public int? OrderID { get; set; }

        public virtual Order Order { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Models;

namespace TestTask.ViewComponents
{
    public class OrderDetail : ViewComponent
    {
        public OrderDetail()
        {            
            Orders = new List<Order>();
            Orders.Add(new Order { OrderID = 1, OrderStatus = OrderStatus.Completed });
            Orders[0].OrderRows.Add(new OrderRow
            {
                OrderRowID = Guid.NewGuid(),
                Order = Orders[0],
                OrderID = 1,
                ProductName = "Product 1",
                ProductPrice = (decimal)23.0,
                ProductQuantity = 12
            }); ;
            Orders[0].OrderRows.Add(new OrderRow
            {
                OrderRowID = Guid.NewGuid(),
                Order = Orders[0],
                OrderID = 1,
                ProductName = "Product 2",
                ProductPrice = (decimal)25.5,
                ProductQuantity = 16
            });

            Orders.Add(new Order { OrderID = 2 });
            Orders[1].OrderRows.Add(new OrderRow
            {
                OrderRowID = Guid.NewGuid(),
                Order = Orders[1],
                OrderID = 2,
                ProductName = "Product 2_1",
                ProductPrice = (decimal)0.3,
                ProductQuantity = 19
            }); ;
            Orders[1].OrderRows.Add(new OrderRow
            {
                OrderRowID = Guid.NewGuid(),
                Order = Orders[1],
                OrderID = 2,
                ProductName = "Product 2_2",
                ProductPrice = (decimal)125.5,
                ProductQuantity = 3
            });
        }

        public List<Order> Orders { get; private set; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int.TryParse(Request.Query["id"], out int orderNumber);
            if (orderNumber == 0)
                return View(new Order());

            return View(Orders.Find(s => s.OrderID == orderNumber));
        }

    }
}

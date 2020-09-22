using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TestTask.Models;

namespace TestTask.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
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

        public List<Models.Order> Orders { get; set; }        
        public Models.Order SelectedOrder { get; set; }

        public void OnGet(int? id)
        {
            //Orders типа получаем из API через httpRequest

            if (id != null)
                SelectedOrder = Orders.Find(s => s.OrderID == id);
        }
    }
}

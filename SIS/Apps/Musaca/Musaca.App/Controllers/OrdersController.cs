using Musaca.Models;
using Musaca.Services;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;

namespace Musaca.App.Controllers
{
    using SIS.MvcFramework.Results;

    public class OrdersController : Controller
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public IActionResult Cashout()
        {
            Order currentActiveOrder = this.orderService.GetCurrentActiveOrderByCashierId(this.User.Id);

            this.orderService.CompleteOrder(currentActiveOrder.Id, this.User.Id);

            return this.Redirect("/");
        }
    }
}

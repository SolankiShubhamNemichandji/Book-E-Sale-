using bookStore.Repositories;
using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace bookstore.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class orderController : ControllerBase
    {
        orderRepository _order = new orderRepository();
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(finalOrderModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult add(finalOrderModel model)
        {
            try
            {
                if (model == null)
                {
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Please insert details properly!");
                }
                Order ord = new Order()
                {
                    Id = model.id,
                    Userid = model.userId,
                    Date = model.orderDate,
                    Cartids = model.cartIds,
                };
                var res = _order.add(ord);
                finalOrderModel finalord = new finalOrderModel(res);
                return StatusCode(HttpStatusCode.OK.GetHashCode(), finalord);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);

            }
        }
    }
}
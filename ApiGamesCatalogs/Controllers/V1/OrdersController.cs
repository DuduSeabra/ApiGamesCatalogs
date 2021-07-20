using ApiGamesCatalogs.InputModel;
using ApiGamesCatalogs.Services;
using ApiGamesCatalogs.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalogs.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Search all orders paged
        /// </summary>
        /// <remarks>
        /// It is not possible to return orders without pagination
        /// </remarks>
        /// <param name="page">Indicates which page is being consulted. At least 1</param>
        /// <param name="quantity">Indicates the number of registers per page. Minimum 1 and maximum 50</param>
        /// <response code="200">Return to order list</response>
        /// <response code="204">If there are no orders</response>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> Obtain([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var orders = await _orderService.Obtain(page, quantity);

            if (orders.Count() == 0)
                return NoContent();

            return Ok(orders);
        }

        /// <summary>
        /// Search a order by id
        /// </summary>
        /// <param name="idOrder">Fetched order id</param>
        /// <response code="200">Return the filtered order</response>
        /// <response code="204">If there is no order with this id</response>

        [HttpGet("{idOrder}")]
        public async Task<ActionResult<OrderViewModel>> Obtain([FromRoute] Guid idOrder)
        {
            var order = await _orderService.Obtain(idOrder);

            if (order == null)
                return NoContent();

            return Ok(order);
        }

        /// <summary>
        /// Insert a new order
        /// </summary>
        /// <param name="orderInputModel">New order parameters</param>
        /// <response code="200">Insert the new order and return it</response>
        /// <response code="204">If there is already a order with this name for this producer</response>

        [HttpPost]
        public async Task<ActionResult<OrderViewModel>> InsertOrder([FromBody] OrderInputModel orderInputModel)
        {
            var order = await _orderService.Insert(orderInputModel);

            return Ok(order);
        }

        /// <summary>
        /// Delete the selected order
        /// </summary>
        /// <param name="idOrder">Fetched order id</param>
        /// <response code="200">Delete the selected order</response>
        /// <response code="204">If there is no order with this id</response>

        [HttpDelete("{idOrder}")]
        public async Task<ActionResult> DeleteOrder([FromRoute] Guid idOrder)
        {
            await _orderService.Remove(idOrder);

            return Ok();
        }
    }
}

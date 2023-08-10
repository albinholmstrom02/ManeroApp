using System.Collections.Generic;
using Backend.Contexts;
using Backend.Models.Entities;
using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DataContext _context;

        public OrdersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var orders = _context.Orders.ToList();
            if (orders.Count == 0)
            {
                return NotFound("Orders not available");
            }
            return Ok(orders);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var orders = _context.Orders.Find(id);
            if (orders == null)
            {
                return NotFound($"Order details not found with id {id}");
            }
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var orderEntity = new OrderEntity
                {
                    UserId = orderViewModel.UserID,
                    Address = orderViewModel.Address,
                    PostalCode = orderViewModel.PostalCode,
                    TotalPrice = orderViewModel.TotalPrice,
                    Products = JsonConvert.SerializeObject(orderViewModel.CartItems),
                };
                
                _context.Orders.Add(orderEntity);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the order.");
            }
        }

        [HttpPost]
        public IActionResult Edit(OrderEntity order)
        {
            if (order == null || order.OrderId == 0)
            {
                if (order == null)
                {
                    return BadRequest("Order data is invalid.");
                }
                else if (order.OrderId == 0)
                {
                    return BadRequest($"Order Id {order.OrderId} is invalid");
                }
            }
            var model = _context.Orders.Find(order.OrderId);
            if (model == null)
            {
                return BadRequest($"Order Id {order.OrderId} is invalid");
            }

            model.OrderId = order.OrderId;
            model.UserId = order.UserId;
            model.Products = order.Products;
            model.TotalPrice = order.TotalPrice;
            _context.SaveChanges();
            return Ok("Order details updated");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound($"Order not found with id {id}");
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return Ok("Order details deleted.");
        }
    }
}

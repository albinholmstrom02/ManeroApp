using Backend.Contexts;
using Backend.Models.Entities;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;
        public ProductsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var products = _context.Products.ToList();
            if (products.Count == 0)
            {
                return NotFound("Products not available");
            }
            return Ok(products);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound($"Product details not found with id {id}");
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Add(ProductEntity product)
        {
            _context.Add(product);
            _context.SaveChanges();
            return Ok("Product created");
        }

        [HttpPut]
        public IActionResult Edit(ProductEntity product)
        {
            if (product == null || product.Id == 0)
            {
                if (product == null)
                {
                    return BadRequest("Product data is invalid.");
                }
                else if (product.Id == 0)
                {
                    return BadRequest($"Product Id {product.Id} is invalid");
                }
            }
            var model = _context.Products.Find(product.Id);
            if (model == null)
            {
                return BadRequest($"Product Id {product.Id} is invalid");
            }
            model.Title = product.Title;
            model.Price = product.Price;
            model.Description = product.Description;
            model.Rating = product.Rating;
            model.ImageUrl = product.ImageUrl;
            model.ProductType = product.ProductType;
            _context.SaveChanges();
            return Ok("Product details updated");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound($"Product not found with id {id}");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok("Product details deleted.");
        }

    }
}

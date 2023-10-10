using Exam11.Server.Models;
using Exam11.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam11.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OrderController(MyDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet]
        public async Task<List<Order>> GetOrders()
        {
            return await _context.Orders.Include(o => o.Items).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrder(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var order = await _context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.OrderId == id);
            return Ok(order);

        }

        [HttpPost]
        public async Task<IActionResult> Post(Order order)
        {

            if (order.ImageFile != null)
            {
                order.CustomerImage = await UploadImageAsync(order.ImageFile);
            }
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Order order)
        {
            if (order.ImageFile != null)
            {
                order.CustomerImage = await UploadImageAsync(order.ImageFile);
            }

           
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();    
        }

        [HttpGet("products")]
        public async Task<List<Product>> Product()
        {
            return await _context.Products.ToListAsync();
        }




        private async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Image");
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return filePath;
        }
    }
}

using EcomApi.Model;
using Humanizer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcomApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class ProdController : ControllerBase
    {
        private readonly EcomDBContext context;

        public ProdController(EcomDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var prodlist = await context.Products.ToListAsync();
            return Ok(prodlist);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProd(ProductDTO dt)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            string fileName = Guid.NewGuid() + Path.GetExtension(dt.ImageFile!.FileName);
            string filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dt.ImageFile.CopyToAsync(stream);
            }

            var product = new Product
            {
                ProdName = dt.Name,
                Price = dt.Price,
                CategoryId = dt.CategoryId,
                ImagePath = "/images/" + fileName
            };

            context.Products.Add(product);
            await context.SaveChangesAsync();
            return Ok("Product Added Successfully");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditProd([FromForm] int id, ProductDTO dt)
        {
            var check = await context.Products.FindAsync(id);

            if (check == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            check.ProdName = dt.Name;
            check.Price = dt.Price;
            check.CategoryId = dt.CategoryId;

            if (dt.ImageFile != null)
            {
                // Delete old image
                if (!string.IsNullOrEmpty(check.ImagePath))
                {
                    var oldPath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        check.ImagePath.TrimStart('/')
                    );
                    if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                }

                // Save new image
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                string fileName = Guid.NewGuid() + Path.GetExtension(dt.ImageFile.FileName);
                string filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dt.ImageFile.CopyToAsync(stream);
                }

                check.ImagePath = "/images/" + fileName;
            }



            context.Products.Update(check);
            await context.SaveChangesAsync();

            return Ok("Product Updated");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> ProdDelete(int id)
        {
            var del = await context.Products.FindAsync(id);

            if (del == null)
                return NotFound();

            if (!string.IsNullOrEmpty(del.ImagePath))
            {
                var fullPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    del.ImagePath.TrimStart('/')
                );
                if (System.IO.File.Exists(fullPath)) System.IO.File.Delete(fullPath);
            }


            context.Products.Remove(del);
            await context.SaveChangesAsync();
            return Ok("Product deleted successfully");
        }


    }
}

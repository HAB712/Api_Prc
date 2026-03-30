using EcomApi.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EcomApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class CateController : ControllerBase
    {
        private readonly EcomDBContext context;

        public CateController(EcomDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var catelist = await context.Categories.ToListAsync();
            return Ok(catelist);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCate(CategoryDTO dt)
        {
            var cate = new Category();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            cate.CateName = dt.CateName;

            context.Categories.Add(cate);
            await context.SaveChangesAsync();
            return Ok("Category Added Successfully");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> editCate([FromRoute] int id, [FromBody] CategoryDTO vm)
        {
            if (vm == null)
                return BadRequest("Request body is null.");

            var check = await context.Categories.FindAsync(id);

            if (check == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            check.CateName = vm.CateName;
            context.Categories.Update(check);
            await context.SaveChangesAsync();

            return Ok("Category Updated");
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var del = await context.Categories.FindAsync(id);
           
            if (del == null)
                return NotFound();

            context.Categories.Remove(del);
            await context.SaveChangesAsync();
            return Ok("Category deleted successfully");
        }

    }
}

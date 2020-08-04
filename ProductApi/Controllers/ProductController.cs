﻿using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Repository;
using System.Threading.Tasks;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Get()
        {
            return Ok(await _repo.ListProducts());
        }

        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _repo.GetProduct(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            if (product != null)
            {
                _repo.Add(product);
                await _repo.SaveChangesAsync();
                return Created($"/api/evento/{product.Id}", product);
            }
            else
                return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] int id, Product product)
        {
            var p = await _repo.GetProduct(id);

            if (p == null)
                return NotFound();

            _repo.Update(product);
            await _repo.SaveChangesAsync();

            return Ok(product);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var product = await _repo.GetProduct(id);

            if (product == null)
                return NotFound();

            _repo.Delete(product);
            await _repo.SaveChangesAsync();

            return Ok();
        }
    }
}

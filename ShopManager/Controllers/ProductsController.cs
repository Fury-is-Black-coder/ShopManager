using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopManager.Data;
using ShopManager.Models;
using ShopManager.DTOs;

namespace ShopManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _db;

    public ProductsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] bool? active)
    {
        IQueryable<Product> query = _db.Products;

        if (active.HasValue)
            query = query.Where(p => p.IsActive == active.Value);

        var products = await query.ToListAsync();
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto dto)
    {
        if (await _db.Products.AnyAsync(p => p.Sku == dto.Sku))
            return Conflict("Product with this SKU already exists.");

        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Sku = dto.Sku
        };

        _db.Products.Add(product);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateProductDto dto)
    {
        var product = await _db.Products.FindAsync(id);
        if (product == null)
            return NotFound();

        product.Name = dto.Name;
        product.Price = dto.Price;

        await _db.SaveChangesAsync();
        return Ok(product);
    }

    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product == null)
            return NotFound();

        product.IsActive = false;
        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("{id:guid}/activate")]
    public async Task<IActionResult> Activate(Guid id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product == null)
            return NotFound();

        product.IsActive = true;
        await _db.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product == null)
            return NotFound();

        _db.Products.Remove(product);
        await _db.SaveChangesAsync();

        return NoContent();
    }


}

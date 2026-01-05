using Microsoft.EntityFrameworkCore;
using ShopManager.Data;
using ShopManager.Models;

namespace ShopManager.Services;

public class ProductService
{
    private readonly AppDbContext _db;

    public ProductService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Product> Create(string name, decimal price, string sku)
    {
        if (await _db.Products.AnyAsync(p => p.Sku == sku))
            throw new InvalidOperationException("SKU already exists");

        var product = new Product
        {
            Name = name,
            Price = price,
            Sku = sku
        };

        _db.Products.Add(product);
        await _db.SaveChangesAsync();

        return product;
    }

    public async Task Deactivate(Guid id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product == null)
            throw new InvalidOperationException("Product not found");

        product.IsActive = false;
        await _db.SaveChangesAsync();
    }

}

using Microsoft.EntityFrameworkCore;
using ShopManager.Data;
using ShopManager.Services;
using Xunit;

public class ProductServiceTests
{
    private static AppDbContext GetDb()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task Create_ShouldCreateProduct()
    {
        var db = GetDb();
        var service = new ProductService(db);

        var product = await service.Create("Test", 10, "SKU1");

        Assert.NotNull(product);
        Assert.Equal("SKU1", product.Sku);
        Assert.Single(db.Products);
    }

    [Fact]
    public async Task Create_ShouldThrow_WhenSkuExists()
    {
        var db = GetDb();
        var service = new ProductService(db);

        await service.Create("Test", 10, "SKU1");

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            service.Create("Test2", 20, "SKU1"));
    }

    [Fact]
    public async Task Deactivate_ShouldSetIsActiveFalse()
    {
        var db = GetDb();
        var service = new ProductService(db);

        var product = await service.Create("Test", 10, "SKU2");

        await service.Deactivate(product.Id);

        var saved = await db.Products.FindAsync(product.Id);
        Assert.False(saved!.IsActive);
    }

}

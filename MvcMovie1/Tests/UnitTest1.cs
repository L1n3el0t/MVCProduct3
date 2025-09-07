using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMovie1.Data;
using MvcMovie1.Models;
using MvcMovie1.Services;
using Xunit;

namespace Tests;

public class UnitTest1
{
    private static ProductService CreateService()
    {
        var dbContext = new DbContextOptionsBuilder<MvcMovie1Context>();
        var inMemDb = dbContext.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var options = inMemDb.Options;
        var context = new MvcMovie1Context(options);

        return new ProductService(context);
    }

    [Fact]
    public async Task MovieService_CanPerformCRUDOp()
    {
        var svc = CreateService();

        var product = new Product
        {
            Title = "Intersteller",
            ReleaseDate = DateTime.Parse("2014-6-19"),
            Genre = "Sci Fi",
            Price = 6.99M
        };

        await svc.AddAsync(product);

        //READ
        var read = await svc.GetByIdAsync(product.Id);
        Assert.NotNull(read);
        Assert.Equal(product.Title, read.Title);

        //UPDATE
        read.Title = "Interstellar 10 years";
        await svc.UpdateAsync(read);
        var updated = await svc.GetByIdAsync(product.Id);
        Assert.NotNull(updated);
        Assert.Equal(product.Title, updated.Title);


        //DELETE
        await svc.DeleteAsync(product.Id);
        var deleted = await svc.GetByIdAsync(product.Id);
        Assert.Null(deleted);
    }
}
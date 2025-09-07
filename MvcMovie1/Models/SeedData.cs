using Microsoft.EntityFrameworkCore;
using MvcMovie1.Data;
using MvcMovie2.Features.Products.Models;

namespace MvcMovie1.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcMovie1Context(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcMovie1Context>>()))
            {
                // Look for any movies.
                if (context.Product.Any())
                {
                    return;   // DB has been seeded
                }
                context.Product.AddRange(
                    new Product
                    {
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Genre = "Romantic Comedy",
                        Price = 7.99M
                    },
                    new Product
                    {
                        Title = "Ghostbusters ",
                        ReleaseDate = DateTime.Parse("1984-3-13"),
                        Genre = "Comedy",
                        Price = 8.99M
                    },
                    new Product
                    {
                        Title = "Ghostbusters 2",
                        ReleaseDate = DateTime.Parse("1986-2-23"),
                        Genre = "Comedy",
                        Price = 9.99M
                    },
                    new Product
                    {
                        Title = "Rio Bravo",
                        ReleaseDate = DateTime.Parse("1959-4-15"),
                        Genre = "Western",
                        Price = 3.99M
                    },
                    new Product
                    {
                        Title = "Intersteller",
                        ReleaseDate = DateTime.Parse("2014-6-19"),
                        Genre = "Sci Fi",
                        Price = 6.99M
                    }
                );
                context.SaveChanges();
            }

        }
    }
}
        
    

using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcMovie2.Features.Products.Models
{
    public class ProductGenreViewModel
    {
        public List<Product>? Products { get; set; }
        public SelectList? Genres { get; set; }
        public string? ProductGenre { get; set; }
        public string? SearchString { get; set; }
    }
}

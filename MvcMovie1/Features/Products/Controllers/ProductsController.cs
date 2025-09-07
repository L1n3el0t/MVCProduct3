using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcMovie1.Models;
using MvcMovie2.Features.Products.Models;
using MvcMovie2.Features.Products.Services;

namespace MvcMovie2.Features.Products.Controllers
{

    [Route("products")]
    public class ProductsController : Controller
    {
        private readonly IProductService _products;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService products, ILogger<ProductsController> logger)
        {
            _products = products;
            _logger = logger;

        }



        // GET: products
        [HttpGet("")]
        public async Task<IActionResult> Index(string productGenre, string searchString)
        {

            IEnumerable<Product> all = await _products.GetAllAsync();
            IEnumerable<Product> products = all;
            IEnumerable<string?> genreQuery = all.Select(product => product.Genre).Distinct();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(product => product.Title != null && product.Title.ToUpper().Contains(searchString, StringComparison.OrdinalIgnoreCase));

                _logger.LogInformation("Searching by string {searchString} ", searchString);
            }

            if (!string.IsNullOrEmpty(productGenre))
            {
                products = products.Where(product => product.Genre == productGenre);
                _logger.LogInformation("Searching by genre {productGenre} ", productGenre);
            }

            var productGenreVM = new ProductGenreViewModel
            {
                Genres = new SelectList(genreQuery),
                Products = products.ToList()
            };

            return View(productGenreVM);

        }



        // GET: products/details/5
        [HttpGet("details/{id:int}", Name = "ProductDetails")]
        public async Task<IActionResult> Details(int id)
        {


            var product = await _products.GetByIdAsync(id);

            _logger.LogInformation("Displaying details for product {id} ", id);
            return View(product);
        }

        // GET: products/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            _logger.LogInformation("Create GET");
            return View();
        }

        // POST: products/create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price")] Product product)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Create POST model invalid");
                return View(product);
            }

            await _products.AddAsync(product);
            return RedirectToAction(nameof(Index));

        }

        // GET: products/edit/5
        [HttpGet("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {

            var product = await _products.GetByIdAsync(id);
            _logger.LogInformation("Edit GET for product {id} ", id);
            return View(product);
        }

        // POST: products/edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Product product)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Edit POST model invalid for product {id} ", id);
                return View(product);
            }
            await _products.UpdateAsync(product);
            return RedirectToAction(nameof(Index));

        }

        // GET: products/delete/5
        [HttpGet("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _products.GetByIdAsync(id);
            _logger.LogInformation("Delete GET for product {id} ", id);
            return View(product);
        }

        // POST: products/delete/5
        [HttpPost("delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _products.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: products/bygenre/comedy
        [HttpGet("bygenre/{genre}")]
        public async Task<IActionResult> ByGenre(string genre)
        {
            var all = await _products.GetAllAsync();
            var products = all.Where(product => product.Genre != null && string.Equals(product.Genre, genre, StringComparison.OrdinalIgnoreCase));
            var viewModel = new ProductGenreViewModel
            {
                Genres = new SelectList(all.Select(product => product.Genre).Distinct()),
                Products = products.ToList()
               ,
                ProductGenre = genre
            };
            return View("Index", viewModel);
        }

        // GET: products/byreleasedate/20
        [HttpGet("released/{year:int:min(1900)}/{month:int:range(1,12)?}")]
        public async Task<IActionResult> ByReleaseDate(int year, int month)
        {
            var all = await _products.GetAllAsync();
            var products = all.Where(product => product.ReleaseDate.Year == year && (month == 0 ? true: product.ReleaseDate.Month == month));
            var viewModel = new ProductGenreViewModel
            {
                Genres = new SelectList(all.Select(product => product.Genre).Distinct()),
                Products = products.ToList()

            };
            return View("Index", viewModel);

        }
    }
}
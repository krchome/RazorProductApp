using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using NuGet.Protocol.Plugins;
using RazorProductApp.Data;
using RazorProductApp.Models;

namespace RazorProductApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        //Use the ProductDbContext class to access the database
        private readonly ProductDbContext _context;
        //Inject the ProductDbContext class into the IndexModel class

        public IndexModel(ILogger<IndexModel> logger, ProductDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Pagination parameters
        public int PageSize { get; set; } = 3;
        public int PageNumber { get; set; } = 1;
        public int TotalPages { get; set; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;


        // Search and sort parameters
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }
        [BindProperty(SupportsGet = true)]
       // public string SortOrder { get; set; } = "asc";


        //Create a property to store all products
        // List of products
        public IList<Product> Products { get; set; }
        public void OnGet(int pageNumber = 1, string sortOrder = "name_asc")
        {

            
            //apply search filter, sorting and paginate the products
            IQueryable<Product> productsQuery = _context.Products;
            
            //Default sort order
           //  string sortOrder = "name_asc";

            ViewData["NameSort"] = sortOrder == "name_asc" ? "name_desc" : "name_asc";
            ViewData["ManufacturerSort"] = sortOrder == "manufacturer_asc" ? "manufacturer_desc" : "manufacturer_asc";
            ViewData["PriceSort"] = sortOrder == "price_asc" ? "price_desc" : "price_asc";

            //var productsQuery = from pq in _context.Products
            //                    select pq;
            // Apply search filter
            if (!string.IsNullOrEmpty(SearchString))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Name.Contains(SearchString) ||
                    p.Manufacturer.Contains(SearchString));
            }
            // Apply sorting
            
            switch (sortOrder)
            {
                case "name_asc":
                    productsQuery = productsQuery.OrderBy(p => p.Name);
                    break;
                case "name_desc":
                    productsQuery = productsQuery.OrderByDescending(p => p.Name);
                    break;
                case "manufacturer_asc":
                    productsQuery = productsQuery.OrderBy(p => p.Manufacturer);
                    break;
                case "manufacturer_desc":
                    productsQuery = productsQuery.OrderByDescending(p => p.Manufacturer);
                    break;
                case "price_asc":
                    productsQuery = productsQuery.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    productsQuery = productsQuery.OrderByDescending(p => p.Price);
                    break;
                default:
                    productsQuery = productsQuery.OrderBy(p => p.Name);
                    break;
            }
            // Update the PageNumber based on the pageNumber parameter
            PageNumber = pageNumber;
            // Calculate TotalPages for pagination
            var TotalProducts = productsQuery.Count();
            TotalPages = (int)Math.Ceiling(TotalProducts / (double)PageSize);

            // Paginate the results
            Products = productsQuery.Skip((PageNumber - 1) * PageSize)
                                    .Take(PageSize)
                                    .ToList();
            ////Display the Products on Index.cshtml page
            //switch (sortOrder)
            //{
            //    case "name_asc":
            //        Products = (IList<Product>)Products.OrderBy(p => p.Name).ToList();
            //        break;
            //    case "name_desc":
            //        Products = (IList<Product>)Products.OrderByDescending(p => p.Name).ToList();
            //        break;
            //    case "manufacturer_asc":
            //        //productsQuery = productsQuery.OrderBy(p => p.Manufacturer);
            //        break;
            //    case "manufacturer_desc":
            //       // productsQuery = productsQuery.OrderByDescending(p => p.Manufacturer);
            //        break;
            //    case "price_asc":
            //       // productsQuery = productsQuery.OrderBy(p => p.Price);
            //        break;
            //    case "price_desc":
            //        //productsQuery = productsQuery.OrderByDescending(p => p.Price);
            //        break;
            //    default:
            //       // productsQuery = productsQuery.OrderBy(p => p.Name);
            //        break;
            //}

        }



    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        //Create a property to store all products
        public List<Product> Products { get; set; }
        public void OnGet()
        {
          
            Products = _context.Products.ToList();
            //Display the Products on Index.cshtml page

        }


       
    }
}
//The application needs to have pagination to display a limited number of products on the Index page.
//Write complete code to add the pagination feature to the Index page of the RazorProductApp application.
// Path: RazorProductApp/Pages/Index.cshtml.cs

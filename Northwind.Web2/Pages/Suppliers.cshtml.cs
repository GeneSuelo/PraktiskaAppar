using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages; //för PageModel
using Northwind.EntityModels; 

namespace Northwind.Web.Pages
{
    public class SuppliersModel : PageModel
    {
        private NorthwindDatabaseContext db;
        public SuppliersModel(NorthwindDatabaseContext injectedContext) 
        {
            this.db = injectedContext;
        }
        public IEnumerable<Supplier>? Suppliers { get; set; }
        public void OnGet()
        {
            ViewData["Title"] = "Northwind App - Suppliers";

            Suppliers = db.Suppliers.OrderBy(c => c.Country)
                .ThenBy(c => c.CompanyName);
        }
    }
}

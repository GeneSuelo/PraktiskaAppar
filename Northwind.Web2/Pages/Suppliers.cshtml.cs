using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages; //för PageModel

namespace Northwind.Web.Pages
{
    public class SuppliersModel : PageModel
    {
        public IEnumerable<string>? Suppliers { get; set; }
        public void OnGet()
        {
            ViewData["Title"] = "Northwind App - Suppliers";

            Suppliers = new[]
            {
                "Exotic Liquids", "Beta Limited", "Alfa Corp"
            };
        }
    }
}

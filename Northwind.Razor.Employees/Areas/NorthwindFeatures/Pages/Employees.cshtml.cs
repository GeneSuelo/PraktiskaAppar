using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Northwind.EntityModels;

namespace Northwind.Razor.Employees.MyFeature.Pages
{
    public class EmployeesModel : PageModel
    {
        private NorthwindDatabaseContext db;

        public EmployeesModel(NorthwindDatabaseContext injectedContext) 
        {
            db = injectedContext;
        }

        public Employee[] Employees { get; set; } = null!;
        public void OnGet()
        {
            ViewData["Title"] = "Northwind App - Employees";
            //TODO: Implementera kod för att hämta
            //employees från databased, sortera med LastName sen FrstName
            Employees = db.Employees.OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName).ToArray();
        }
    }
}
 
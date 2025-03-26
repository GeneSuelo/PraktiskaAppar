using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Northwind.Razor.Employees.MyFeature.Pages
{
    public class EmployeesMode : PageModel
    {
        public void OnGet()
        {
            ViewData["Title"] = "Northwind App - Employees";
            //TODO: Implementera kod för att hämta
            //employees från databased, sortera med LastName sen FrstName
        }
    }
}

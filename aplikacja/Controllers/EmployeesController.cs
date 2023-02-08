using aplikacja.Data;
using aplikacja.Models;
using aplikacja.Models.Domain;
using Azure.Core.GeoJson;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace aplikacja.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly CrudDbContext crudDbContext;

        public EmployeesController(CrudDbContext crudDbContext)
        {
            this.crudDbContext = crudDbContext;
        }
        [HttpGet]

        public IActionResult Add()
        {
            return View();
        }

        public async Task<IActionResult> Add(AddEmployeeViewModel add)
        {
            var empoyee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = add.Name,
                Email = add.Email,
                Salary = add.Salary,
                Position = add.Position,
                BirthDate = add.BirthDate,
            };

            await crudDbContext.Employees.AddAsync(empoyee);
            await crudDbContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }
    }
}
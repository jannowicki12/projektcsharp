using aplikacja.Data;
using aplikacja.Models;
using aplikacja.Models.Domain;
using Azure.Core.GeoJson;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {

            var employees = await crudDbContext.Employees.ToListAsync();
            return View(employees);
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


        [HttpGet]

        public async Task<IActionResult> View(Guid id)
        {
            var employee = await crudDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Position = employee.Position,
                    BirthDate = employee.BirthDate,
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await crudDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.BirthDate = model.BirthDate;
                employee.Position = model.Position;

                await crudDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            };
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await crudDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                crudDbContext.Employees.Remove(employee);
                await crudDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
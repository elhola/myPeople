using Empal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Empal.Controllers
{
    public class EmployeeController : Controller
    {
        private static List<Employee> employees = new()
        {
            new Employee { Id = 1, FirstName = "Петро", LastName = "Власенко", Patronymic = "Олегович", Birthday = new DateTime(1989, 1, 22), Department = "IT", Position = "Програміст", Salary = 50000 },
            new Employee { Id = 2, FirstName = "Марія", LastName = "Юдіна", Patronymic = "Андріївна", Birthday = new DateTime(1990, 8, 22), Department = "HR", Position = "HR-менеджер", Salary = 40000 },
            new Employee { Id = 3, FirstName = "Олексій", LastName = "Іванов", Patronymic = "Сергійович", Birthday = new DateTime(1985, 3, 10), Department = "Продажи", Position = "Продавець", Salary = 45000 },
            new Employee { Id = 4, FirstName = "Олена", LastName = "Василенко", Patronymic = "Сергіївна", Birthday = new DateTime(1982, 12, 5), Department = "Финансы", Position = "Фінансовый аналітик", Salary = 55000 },
            new Employee { Id = 5, FirstName = "Дмитро", LastName = "Вакарчук", Patronymic = "Дмитрович", Birthday = new DateTime(1978, 7, 18), Department = "IT", Position = "Системний адмінистратор", Salary = 48000 },
        };

        public IActionResult Index()
        {
            return View(employees);
        }

        public IActionResult Details(int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        public IActionResult Edit(int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee updatedEmployee)
        {
            var employee = employees.FirstOrDefault(e => e.Id == updatedEmployee.Id);
            if (employee == null)
            {
                return NotFound();
            }

            employee.FirstName = updatedEmployee.FirstName;
            employee.LastName = updatedEmployee.LastName;
            employee.Patronymic = updatedEmployee.Patronymic;
            employee.Birthday = updatedEmployee.Birthday;
            employee.Department = updatedEmployee.Department;
            employee.Position = updatedEmployee.Position;
            employee.Salary = updatedEmployee.Salary;

            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee newEmployee)
        {
            newEmployee.Id = employees.Max(e => e.Id) + 1;
            employees.Add(newEmployee);
            return RedirectToAction("Index");


        }

        public IActionResult Search(string lastName, string department, string position)
        {
            var searchResults = employees.Where(e =>
                (string.IsNullOrEmpty(lastName) || e.LastName.Contains(lastName)) &&
                (string.IsNullOrEmpty(department) || e.Department.Contains(department)) &&
                (string.IsNullOrEmpty(position) || e.Position.Contains(position))
            ).ToList();

            return View("Search", searchResults);
        }


        public IActionResult Delete(int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }


        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                employees.Remove(employee);
            }
            return RedirectToAction("Index");
        }

        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

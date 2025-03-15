using IKEA.BLL.Models.Departments;
using IKEA.BLL.Models.Employees;
using IKEA.BLL.Services.Departments;
using IKEA.BLL.Services.Employees;
using IKEA.DAL.Common.Enums;
using IKEA.PL.Models.Department;
using IKEA.PL.Models.Employee;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services

        private readonly IEmployeeService _employeeService;
        private readonly ILogger<CreatedEmployeeDto> _logger;
        private readonly IWebHostEnvironment _environment;

        public EmployeeController(IEmployeeService employeeService, ILogger<CreatedEmployeeDto> logger, IWebHostEnvironment environment)
        {
            _employeeService = employeeService;
            _logger = logger;
            _environment = environment;
        }

        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();
            return View(employees);
        }

        #endregion

        #region Create

        #region Get
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        #endregion

        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(EmployeeVM employeeVM)
        {
            if (!ModelState.IsValid)  // Server Side Validation
            {
                return View(employeeVM);
            }

            string message = string.Empty;
            try
            {
                var employee = new CreatedEmployeeDto()
                {
                    Name = employeeVM.Name,
                    Address = employeeVM.Address,
                    Age = employeeVM.Age,
                    Email = employeeVM.Email,
                    EmployeeType = employeeVM.EmployeeType,
                    Gender = employeeVM.Gender,
                    HiringDate = employeeVM.HiringDate,
                    IsActive = employeeVM.IsActive,
                    PhoneNumber = employeeVM.PhoneNumber,
                    Salary = employeeVM.Salary
                };
                var result = _employeeService.CreateEmployee(employee);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Employee is not created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(employeeVM);
                }
            }
            catch (Exception ex)
            {
                // 1- Log Exeption
                _logger.LogError(ex, ex.Message);
                // 2- Set Frindly Message
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(employeeVM);
                }
                else
                {
                    message = "Employee is not created";
                    return View("Error", message);
                }
            }

        }

        #endregion

        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
                  return NotFound();
            return View(employee);
        }
        #endregion

        #region Edit

        #region Get

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null)
                return NotFound();
            return View(new EmployeeVM
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                Address = employee.Address,
                Email = employee.Email,
                HiringDate = employee.HiringDate,
                PhoneNumber = employee.PhoneNumber,
                EmployeeType = employee.EmployeeType,
                Gender = employee.Gender
                
            });
        }

        #endregion

        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit([FromRoute] int id, [FromBody] EmployeeVM employeeVM)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }

            var message = string.Empty;
            try
            {
                var updatedEmployee = new UpdatedEmployeeDto()
                {
                    Id = id,
                    Name = employeeVM.Name,
                    Address = employeeVM.Address,
                    Age = employeeVM.Age,
                    Email = employeeVM.Email,
                    EmployeeType = employeeVM.EmployeeType,
                    Gender = employeeVM.Gender,
                    HiringDate = employeeVM.HiringDate,
                    IsActive = employeeVM.IsActive,
                    PhoneNumber = employeeVM.PhoneNumber,
                    Salary = employeeVM.Salary
                };
                var updated = _employeeService.UpdateEmployee(updatedEmployee) > 0;
                if (updated)
                {
                    return RedirectToAction(nameof(Index));
                }

                message = "Sorry, An Error Occured During Updating The Employee";
                ModelState.AddModelError(string.Empty, message);
                return View(updatedEmployee);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment() ? ex.Message : "Sorry, An Error Occured While Updating The Employee";
                ModelState.AddModelError(string.Empty, message);
                return View(employeeVM);
            }

        }

        #endregion


        #endregion

        #region Delete

        #region Get
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        #endregion

        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete(int id)
        { 
            var massage = string.Empty;
            try
            {
                var deleted = _employeeService.DeleteEmployee(id);
                if (deleted)
                    return RedirectToAction(nameof(Index));

                massage = "Sorry, An Error Occured During Deleting The Employee";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                massage = _environment.IsDevelopment() ? ex.Message : "Sorry, An Error Occured During Deleting The EMployee";

            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #endregion
    }
}

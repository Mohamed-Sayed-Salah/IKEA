using IKEA.BLL.Models.Departments;
using IKEA.BLL.Services;
using IKEA.DAL.Models.Departments;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<CreatedDepartmentDTO> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService,ILogger<CreatedDepartmentDTO> logger, IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
        }

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
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
        public IActionResult Create(CreatedDepartmentDTO createdDepartment)
        {
            if(!ModelState.IsValid)  // Server Side Validation
            {
                return View(createdDepartment);
            }
            string message = string.Empty;
            try
            {
                var result = _departmentService.CreateDepartment(createdDepartment);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Department is not created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(createdDepartment);
                }
            }catch (Exception ex)
            {
                // 1- Log Exeption
                _logger.LogError(ex, ex.Message);
                // 2- Set Frindly Message
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(createdDepartment);
                }
                else
                {
                    message = "Department is not created";
                    return View("Error", message);
                }
            }

        }

        #endregion

        #endregion

    }
}

using IKEA.BLL.Models.Departments;
using IKEA.BLL.Services.Departments;
using IKEA.DAL.Models.Departments;
using IKEA.PL.Models.Department;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {

        #region Services

        private readonly IDepartmentService _departmentService;
        private readonly ILogger<CreatedDepartmentDTO> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService, ILogger<CreatedDepartmentDTO> logger, IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
        }

        #endregion

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
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            if(!ModelState.IsValid)  // Server Side Validation
            {
                return View(departmentVM);
            }
            string message = string.Empty;

            try
            {
                var createdDepartment = new CreatedDepartmentDTO()
                {
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    CreationDate = departmentVM.CreationDate
                };
                var result = _departmentService.CreateDepartment(createdDepartment);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Department is not created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(departmentVM);
                }
            }catch (Exception ex)
            {
                // 1- Log Exeption
                _logger.LogError(ex, ex.Message);
                // 2- Set Frindly Message
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(departmentVM);
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

        #region Details

         [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null)
                return NotFound();
            return View(department);
        }


        #endregion

        #region Edit

        #region Get

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null)
                return NotFound();
            return View(new DepartmentViewModel
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate
            });
        }

        #endregion

        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit([FromRoute]int id, [FromBody]DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentVM);
            }

            var message = string.Empty;
            try
            {
                var updatedDepartment = new UpdatedDepartmentDTO()
                {
                    Id = id,
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    CreationDate = departmentVM.CreationDate
                };
                var updated = _departmentService.UpdateDepartment(updatedDepartment)>0;
                if (updated)
                {
                    return RedirectToAction(nameof(Index));
                }
              
                    message = "Sorry, An Error Occured During Updating The Department";
                    ModelState.AddModelError(string.Empty, message);
                    return View(departmentVM);
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment()? ex.Message : "Sorry, An Error Occured While Updating The Department ";
                ModelState.AddModelError(string.Empty, message);
                return View(departmentVM);
            }

        }

        #endregion

        #endregion

        #region Delete

        #region Get
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null)
                return NotFound();
            return View(department);
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
                var deleted = _departmentService.DeleteDepartment(id);
                            if (deleted)
                                 return RedirectToAction(nameof(Index));

                            massage = "Sorry, An Error Occured During Deleting The Department";
           
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                massage = _environment.IsDevelopment() ? ex.Message : "Sorry, An Error Occured During Deleting The Department";
                
            }
            return RedirectToAction(nameof(Index));

        }

        #endregion

        #endregion

    }
}

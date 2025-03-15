using IKEA.BLL.Models.Employees;

namespace IKEA.BLL.Services.Employees
{
   public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployees();
        EmployeeDetailsDto? GetEmployeeById(int id);
        int CreateEmployee(CreatedEmployeeDto EmployeeDTO);
        int UpdateEmployee(UpdatedEmployeeDto EmployeeDTO);
        bool DeleteEmployee(int id);
    }
}

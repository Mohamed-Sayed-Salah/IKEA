using IKEA.BLL.Models.Employees;
using IKEA.DAL.Common.Enums;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Presistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService 
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            return _employeeRepository.GetAllAsQuarable().Where(e => !e.IsDeleted)
                .Select(e => new EmployeeDto()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Age = e.Age,
                    Salary = e.Salary,
                    IsActive = e.IsActive,
                    Email = e.Email,
                    Gender = e.Gender, 
                    EmployeeType = e.EmployeeType
                });
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is { })
                return new EmployeeDetailsDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    Salary = employee.Salary,
                    IsActive = employee.IsActive,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender,
                    EmployeeType = employee.EmployeeType,
                };

            return null;
        }
        public int CreateEmployee(CreatedEmployeeDto EmployeeDTO)
        {
            var employee = new Employee()
            {
                Name = EmployeeDTO.Name,
                Age = EmployeeDTO.Age,
                Address = EmployeeDTO.Address,
                Salary = EmployeeDTO.Salary,
                IsActive = EmployeeDTO.IsActive,
                Email = EmployeeDTO.Email,
                PhoneNumber = EmployeeDTO.PhoneNumber,
                HiringDate = EmployeeDTO.HiringDate,
                Gender = EmployeeDTO.Gender,
                EmployeeType = EmployeeDTO.EmployeeType,
                DepartmentId = EmployeeDTO.DepartmentId,
                CreatedBy = 1,
                LastModificationBy = 1,
                LastModificationOn = DateTime.UtcNow,
            };
            return _employeeRepository.Add(employee);
        }

        public int UpdateEmployee(UpdatedEmployeeDto EmployeeDTO)
        {
            var employee = new Employee()
            {
                Id = EmployeeDTO.Id,
                Name = EmployeeDTO.Name,
                Age = EmployeeDTO.Age,
                Address = EmployeeDTO.Address,
                Salary = EmployeeDTO.Salary,
                IsActive = EmployeeDTO.IsActive,
                Email = EmployeeDTO.Email,
                PhoneNumber = EmployeeDTO.PhoneNumber,
                HiringDate = EmployeeDTO.HiringDate,
                Gender = EmployeeDTO.Gender,
                EmployeeType = EmployeeDTO.EmployeeType,
                DepartmentId = EmployeeDTO.DepartmentId,
                CreatedBy = 1,
                LastModificationBy = 1,
                LastModificationOn = DateTime.UtcNow,
            };
            return _employeeRepository.Update(employee);
        }
        public bool DeleteEmployee(int id)
        {
             var employee = _employeeRepository.GetById(id);
            if (employee is { })
                return _employeeRepository.Delete(employee)>0;
            return false;
        }

     
    }
}

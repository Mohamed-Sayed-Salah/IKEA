﻿using IKEA.DAL.Models.Employees;
using IKEA.DAL.Presistance.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories.Employees
{
    public interface IEmployeeRepository : IGenericRepository<Employee> 
    {
    }
}

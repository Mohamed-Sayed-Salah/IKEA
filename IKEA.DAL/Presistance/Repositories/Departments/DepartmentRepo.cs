﻿using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories.Departments
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentRepo(ApplicationDbContext dbContext)
        // Ask CLr for object from ApplicationDbContext implicitly
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Department> GetAll(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
            {
                _dbContext.Departments.AsNoTracking().ToList();
            }
            return _dbContext.Departments.ToList();
        }

        public Department? GetById(int id)
        {
            //var department = _dbContext.Departments.Local.FirstOrDefault(D => D.Id == id);
            var department = _dbContext.Departments.Find(id);
            return department;
        }
        public IQueryable<Department> GetAllAsQuarable()
        {
            return _dbContext.Departments;
        }
        public int Add(Department entity)
        {
            _dbContext.Departments.Add(entity);
            //Add Local
            return _dbContext.SaveChanges();
        }

        public int Update(Department entity)
        {
            _dbContext.Departments.Update(entity);
            return _dbContext.SaveChanges();
        }
        public int Delete(Department entity)
        {
            _dbContext.Departments.Remove(entity);
            return _dbContext.SaveChanges();
        }

    }
}

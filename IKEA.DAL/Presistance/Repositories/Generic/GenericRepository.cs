﻿using IKEA.DAL.Models;
using IKEA.DAL.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Presistance.Repositories.Generic
{
    public class GenericRepository<T> :IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<T> GetAll(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
            {
                _dbContext.Set<T>().AsNoTracking().ToList();
            }
            return _dbContext.Set<T>().ToList();
        }

        public T? GetById(int id)
        {
            //var T = _dbContext.Ts.Local.FirstOrDefault(D => D.Id == id);
            var T = _dbContext.Set<T>().Find(id);
            return T;
        }
        public IQueryable<T> GetAllAsQuarable()
        {
            return _dbContext.Set<T>();
        }
        public int Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            //Add Local
            return _dbContext.SaveChanges();
        }

        public int Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }
        public int Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChanges();
        }
    }
}

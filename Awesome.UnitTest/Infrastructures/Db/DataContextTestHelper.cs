using Awesome.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Awesome.UnitTest.Infrastructures.Db
{
    public class DataContextTestHelper
    {
        private readonly DataContext _dataContext;

        public DataContextTestHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Add(object obj)
        {
            _dataContext.Add(obj);
            _dataContext.SaveChanges();
        }
        public T Retrieve<T>(int id) where T : class
        {
            return _dataContext.Set<T>().Find(id);
        }

        public void AddRange(IEnumerable<object> obj)
        {
            _dataContext.AddRange(obj);
            _dataContext.SaveChanges();
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using PersonRegistration.Domain.Core;

namespace PersonRegistration.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Rolback()
        {
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}

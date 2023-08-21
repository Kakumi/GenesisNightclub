using GenesisNightclub.Application.Interfaces;
using GenesisNightclub.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Infrastucture.UnitOfWork
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private readonly NightclubContext _context;

        public SqlUnitOfWork(NightclubContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

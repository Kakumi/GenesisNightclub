using GenesisNightclub.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Infrastucture.UnitOfWork
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        public void Save()
        {
        }

        public Task SaveAsync()
        {
            return Task.CompletedTask;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Application.Interfaces
{
    public interface IUnitOfWork
    {
        void Save();
        Task SaveAsync();
    }
}

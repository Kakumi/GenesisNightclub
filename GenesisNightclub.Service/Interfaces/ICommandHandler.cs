using GenesisNightclub.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Application.Interfaces
{
    public interface ICommandHandler<T> : IRequestHandler<T, Result> where T : ICommand 
    {
    }

    public interface ICommandHandler<T1, T2> : IRequestHandler<T1, Result<T2>> where T1 : ICommand<T2>
    {
    }
}

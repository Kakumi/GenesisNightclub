using GenesisNightclub.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Application.Member.Commands.DeleteMember
{
    public sealed record DeleteMemberCommand(int Id) : ICommand;
}

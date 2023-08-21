using GenesisNightclub.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Application.Member.Commands.UpdateMember
{
    public sealed record UpdateMemberCommand(
            Domain.Models.Member Member,
            string? Contact,
            DateTime? EndBlacklisted,
            string? Firstname,
            string? Lastname
    ) : ICommand;
}

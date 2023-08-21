using GenesisNightclub.Application.Interfaces;
using GenesisNightclub.Application.Member.Queries.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Application.Member.Queries.GetMembers
{
    internal class GetMembersQuery : IQuery<ICollection<MemberResponse>>
    {
    }
}

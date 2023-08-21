using GenesisNightclub.Application.Interfaces;
using GenesisNightclub.Application.Member.Queries.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Application.Member.Queries.GetMemberById
{
    public sealed record GetMemberByIdQuery(int Id) : IQuery<MemberResponse>;
}

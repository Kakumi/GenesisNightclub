using GenesisNightclub.Application.Interfaces;
using GenesisNightclub.Application.Member.Queries.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Application.Member.Queries.GetMemberByNationalNumber
{
    public sealed record GetMemberByNationalNumberQuery(string NationalNumber) : IQuery<MemberResponse>
    {
    }
}

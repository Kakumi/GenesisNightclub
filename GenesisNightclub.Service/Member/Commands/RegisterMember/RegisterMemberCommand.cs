using GenesisNightclub.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Application.Member.Commands.RegisterMember
{
    public sealed record RegisterMemberCommand(
            string Contact,
            int CardNumber,
            string Lastname,
            string Firstname,
            DateTime Birthdate,
            string NationalNumber,
            DateTime ValidFrom,
            DateTime ValidTo,
            int MemberCardId
        ) : ICommand;
}

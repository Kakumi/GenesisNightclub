using AutoMapper;
using GenesisNightclub.Domain.DTOs;
using GenesisNightclub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Domain.Mappers
{
    public class MemberProfile : Profile
    {
        public MemberProfile() 
        {
            CreateMap<Member, MemberDTO>();
            CreateMap<IdentityCard, IdentityCardDTO>();
            CreateMap<MemberCard, MemberCardDTO>();
            CreateMap<MemberDTO, Member>();
            CreateMap<IdentityCardDTO, IdentityCard>();
            CreateMap<MemberCardDTO, MemberCard>();
        }
    }
}

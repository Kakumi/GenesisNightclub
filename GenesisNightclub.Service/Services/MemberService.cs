using AutoMapper;
using GenesisNightclub.Domain.Interfaces;
using GenesisNightclub.Domain.Models;
using GenesisNightclub.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Service.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public MemberService(IMemberRepository memberRepository, IMapper mapper) 
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<Member?> GetMember(int id)
        {
            var memberDTO = await _memberRepository.GetMember(id);
            var member = _mapper.Map<Member>(memberDTO);

            return member;
        }

        public async Task<Member?> GetMember(string nationalNumber)
        {
            var memberDTO = await _memberRepository.GetMember(nationalNumber);
            var member = _mapper.Map<Member>(memberDTO);

            return member;
        }

        public async Task<List<Member>> GetMembers()
        {
            var membersDTO = await _memberRepository.GetMembers();
            var members = membersDTO.Select(x => _mapper.Map<Member>(x)).ToList();

            return members;
        }

        public async Task RegisterMember(Member member)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateMember(Member member)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteMember(Member member)
        {
            throw new NotImplementedException();
        }
    }
}

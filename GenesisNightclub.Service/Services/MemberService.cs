using AutoMapper;
using GenesisNightclub.Domain.DTOs;
using GenesisNightclub.Domain.Interfaces;
using GenesisNightclub.Domain.Models;
using GenesisNightclub.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            if (!member.IsValidContact())
            {
                throw new ValidationException("Contact detail is not a phone number or an email");
            }

            if (!member.IdentityCard.IsValidNationNumber())
            {
                throw new ValidationException("National number is invalid");
            }

            if (!member.IdentityCard.IsAdult())
            {
                throw new ValidationException("Age is invalid (not an adult)");
            }

            if (member.IdentityCard!.ValidTo <= member.IdentityCard.ValidFrom)
            {
                throw new ValidationException("The date from Identity Card ValidTo must be after ValidFrom");
            }

            if (member.IdentityCard.ValidTo <= DateTime.Now)
            {
                throw new ValidationException("The identity card is not valid anymore (expiration)");
            }

            var memberIDCard = await _memberRepository.GetMember(member.IdentityCard.NationalNumber);
            if (memberIDCard != null)
            {
                throw new ValidationException("An user is already registerd with the same identity card");
            }

            var memberDTO = _mapper.Map<MemberDTO>(member);
            await _memberRepository.RegisterMember(memberDTO);
        }

        public async Task UpdateMember(Member member)
        {
            if (!member.IsValidContact())
            {
                throw new ValidationException("Contact detail is not a phone number or an email");
            }

            var memberDTO = _mapper.Map<MemberDTO>(member);
            await _memberRepository.UpdateMember(memberDTO);
        }

        public async Task DeleteMember(Member member)
        {
            var memberDTO = _mapper.Map<MemberDTO>(member);
            await _memberRepository.DeleteMember(memberDTO);
        }
    }
}

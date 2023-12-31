﻿using GenesisNightclub.Domain.DTOs;
using GenesisNightclub.Application.Interfaces;
using GenesisNightclub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Repository.Repositories
{
    public class InMemoryMemberRepository : IMemberRepository
    {
        private List<MemberDTO> _members;

        public InMemoryMemberRepository() {
            _members = new List<MemberDTO>()
            {
                new MemberDTO()
                {
                    Id = 1,
                    Contact = "test@email.com",
                    EndBlacklisted = null,
                    IdentityCardNumber = 1,
                    IdentityCard = new IdentityCardDTO()
                    {
                        Number = 1,
                        Lastname = "Brebion",
                        Firstname = "Damien",
                        Birthdate = DateTime.Now,
                        NationalNumber = "test",
                        ValidFrom = DateTime.Now,
                        ValidTo = DateTime.Now
                    },
                    MemberCardId = 1,
                    MemberCard = new MemberCardDTO()
                    {
                        Id = 1
                    }
                }
            };
        }

        public Task<MemberDTO?> GetMember(int id)
        {
            return Task.FromResult(_members.FirstOrDefault(m => m.Id == id));
        }

        public Task<MemberDTO?> GetMember(string nationalNumber)
        {
            return Task.FromResult(_members.FirstOrDefault(m => m.IdentityCard?.NationalNumber == nationalNumber));
        }

        public Task<List<MemberDTO>> GetMembers()
        {
            return Task.FromResult(_members);
        }

        public Task<List<MemberDTO>> GetMembers(int memberCardId)
        {
            return Task.FromResult(_members.Where(x => x.MemberCard != null && x.MemberCard.Id == memberCardId).ToList());
        }

        public Task RegisterMember(MemberDTO member)
        {
            _members.Add(member);

            return Task.CompletedTask;
        }

        public Task UpdateMember(MemberDTO member)
        {
            DeleteMember(member);

            RegisterMember(member);

            return Task.CompletedTask;
        }

        public Task DeleteMember(MemberDTO member)
        {
            var memberFound = _members.FirstOrDefault(x => x.Id == member.Id);
            if (memberFound != null)
            {
                return Task.FromResult(_members.Remove(memberFound));
            }

            return Task.CompletedTask;
        }
    }
}

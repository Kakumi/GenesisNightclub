using GenesisNightclub.Domain.DTOs;
using GenesisNightclub.Domain.Interfaces;
using GenesisNightclub.Repository.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Repository.Repositories
{
    public class SqlMemberRepository : IMemberRepository
    {
        private readonly NightclubContext _context;

        public SqlMemberRepository(NightclubContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public Task DeleteMember(MemberDTO member)
        {
            throw new NotImplementedException();
        }

        public async Task<MemberDTO?> GetMember(int id)
        {
            return await _context.Members
                .Include(x => x.IdentityCard)
                .Include(x => x.MemberCard)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<MemberDTO?> GetMember(string nationalNumber)
        {
            return await _context.Members
                .Include(x => x.IdentityCard)
                .Include(x => x.MemberCard)
                .FirstOrDefaultAsync(x => x.IdentityCard.NationalNumber == nationalNumber);
        }

        public async Task<List<MemberDTO>> GetMembers()
        {
            return await _context.Members
                .Include(x => x.IdentityCard)
                .Include(x => x.MemberCard)
                .ToListAsync();
        }

        public Task RegisterMember(MemberDTO member)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMember(MemberDTO member)
        {
            throw new NotImplementedException();
        }
    }
}

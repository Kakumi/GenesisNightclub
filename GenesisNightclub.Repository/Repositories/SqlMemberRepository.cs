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
                .FirstOrDefaultAsync(x => x.IdentityCard != null && x.IdentityCard.NationalNumber == nationalNumber);
        }

        public async Task<List<MemberDTO>> GetMembers()
        {
            return await _context.Members
                .Include(x => x.IdentityCard)
                .Include(x => x.MemberCard)
                .ToListAsync();
        }

        public async Task<List<MemberDTO>> GetMembers(int memberCardId)
        {
            return await _context.Members
                .Include(x => x.IdentityCard)
                .Include(x => x.MemberCard)
                .Where(x => x.MemberCard != null && x.MemberCard.Id == memberCardId)
                .ToListAsync();
        }

        public async Task RegisterMember(MemberDTO member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMember(MemberDTO member)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMember(MemberDTO member)
        {
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
        }
    }
}

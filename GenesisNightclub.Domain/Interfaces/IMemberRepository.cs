using GenesisNightclub.Domain.DTOs;
using GenesisNightclub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Domain.Interfaces
{
    public interface IMemberRepository
    {
        Task<MemberDTO?> GetMember(int id);
        Task<MemberDTO?> GetMember(string nationalNumber);
        Task<List<MemberDTO>> GetMembers();
        Task<List<MemberDTO>> GetMembers(int memberCardId);
        Task RegisterMember(MemberDTO member);
        Task UpdateMember(MemberDTO member);
        Task DeleteMember(MemberDTO member);
    }
}

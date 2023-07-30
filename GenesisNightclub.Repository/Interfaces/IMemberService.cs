using GenesisNightclub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Repository.Interfaces
{
    public interface IMemberService
    {
        Task<Member?> GetMember(int id);
        Task<Member?> GetMember(string nationalNumber);
        Task<List<Member>> GetMembers();
        Task RegisterMember(Member member);
        Task UpdateMember(Member member);
        Task DeleteMember(Member member);
    }
}

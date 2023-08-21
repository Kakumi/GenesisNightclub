using GenesisNightclub.Domain.Models;
using GenesisNightclub.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Domain.Interfaces
{
    public interface IMemberService
    {
        Task<Member?> GetMember(int id);
        Task<Member?> GetMember(string nationalNumber);
        Task<List<Member>> GetMembers();
        Task<Result> RegisterMember(string contact,
            int cardNumber,
            string lastname,
            string firstname,
            DateTime birthdate,
            string nationalNumber,
            DateTime validFrom,
            DateTime validTo,
            int memberCardId);
        Task<Result> UpdateMember(int id,
            string? contact,
            DateTime? endBlacklisted,
            string? firstname,
            string? lastname);
        Task<Result> DeleteMember(int id);
    }
}

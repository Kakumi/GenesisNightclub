using GenesisNightclub.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Domain.DTOs
{
    public class MemberDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string? Contact { get; set; }
        public DateTime? EndBlacklisted { get; set; }

        public int IdentityCardId { get; set; }
        public IdentityCardDTO? IdentityCard { get; set; }

        public int MemberCardId { get; set; }
        public MemberCardDTO? MemberCard { get; set; }
    }
}

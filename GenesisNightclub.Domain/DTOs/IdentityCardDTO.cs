using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Domain.DTOs
{
    public class IdentityCardDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Number { get; set; }
        public string? Lastname { get; set; }
        public string? Firstname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? NationalNumber { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }

        public int MemberId { get; set; }
        public MemberDTO Member { get; set; }
    }
}

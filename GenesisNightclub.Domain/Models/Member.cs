using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenesisNightclub.Domain.Models
{
    public class Member
    {
        public int Id { get; set; }
        public IdentityCard IdentityCard { get; set; }
        public MemberCard MemberCard { get; set; }
        public string Contact { get; set; }
        public DateTime? EndBlacklisted { get; set; }

        public Member(IdentityCard identityCard, MemberCard memberCard, string contact)
        {
            IdentityCard = identityCard;
            MemberCard = memberCard;
            Contact = contact;
            EndBlacklisted = null;
        }

        internal Member()
        {

        }

        public bool IsBlacklisted()
        {
            return EndBlacklisted != null && DateTime.Compare(DateTime.Now, (DateTime)EndBlacklisted) < 0;
        }
    }
}

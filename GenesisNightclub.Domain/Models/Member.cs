using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace GenesisNightclub.Domain.Models
{
    public class Member
    {
        public int Id { get; set; }
        public IdentityCard IdentityCard { get; set; }
        public MemberCard MemberCard { get; set; }
        [JsonIgnore] //Ignored by the api response because it's a private field information
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

        public bool IsValidContact()
        {
            Regex regexPhone = new Regex(@"^([\+]?(?:00)?[0-9]{1,3}[\s.-]?[0-9]{1,12})$");
            Regex regexEmail = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return regexPhone.Match(Contact).Success || regexEmail.Match(Contact).Success;
        }
    }
}

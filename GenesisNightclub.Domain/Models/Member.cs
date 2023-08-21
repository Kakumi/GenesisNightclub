using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using AutoMapper.Execution;

namespace GenesisNightclub.Domain.Models
{
    public class Member
    {
        public int Id { get; set; }
        public IdentityCard IdentityCard { get; set; }
        public MemberCard MemberCard { get; set; }
        public string Contact { get; set; }
        public DateTime? EndBlacklisted { get; set; }

        private Member(IdentityCard identityCard, MemberCard memberCard, string contact)
        {
            IdentityCard = identityCard;
            MemberCard = memberCard;
            Contact = contact;
            EndBlacklisted = null;
        }

        internal Member()
        {

        }

        public static Member Create(IdentityCard identityCard, MemberCard memberCard, string contact)
        {
            var member = new Member(identityCard, memberCard, contact);
            if (!member.IsValidContact())
            {
                throw new ValidationException("Contact detail is not a phone number or an email");
            }

            if (!member.IdentityCard.IsValidNationNumber())
            {
                throw new ValidationException("National number is invalid");
            }

            if (!member.IdentityCard.IsAdult())
            {
                throw new ValidationException("Age is invalid (not an adult)");
            }

            if (member.IdentityCard!.ValidTo <= member.IdentityCard.ValidFrom)
            {
                throw new ValidationException("The date from Identity Card ValidTo must be after ValidFrom");
            }

            if (member.IdentityCard.ValidTo <= DateTime.Now)
            {
                throw new ValidationException("The identity card is not valid anymore (expiration)");
            }

            return member;
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

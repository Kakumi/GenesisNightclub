using AutoMapper.Execution;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GenesisNightclub.Domain.Models
{
    public class IdentityCard
    {
        public int Number { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public DateTime Birthdate { get; set; }
        public string NationalNumber { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        private IdentityCard(int number, string lastname, string firstname, DateTime birthdate, string nationalNumber, DateTime validFrom, DateTime validTo)
        {
            Number = number;
            Lastname = lastname;
            Firstname = firstname;
            Birthdate = birthdate;
            NationalNumber = nationalNumber;
            ValidFrom = validFrom;
            ValidTo = validTo;
        }

        internal IdentityCard() { }

        public static IdentityCard Create(int number, string lastname, string firstname, DateTime birthdate, string nationalNumber, DateTime validFrom, DateTime validTo)
        {
            var identityCard = new IdentityCard(number, lastname, firstname, birthdate, nationalNumber, validFrom, validTo);

            if (!identityCard.IsValidNationNumber())
            {
                throw new ValidationException("National number is invalid");
            }


            if (identityCard!.ValidTo <= identityCard.ValidFrom)
            {
                throw new ValidationException("The date from Identity Card ValidTo must be after ValidFrom");
            }

            if (identityCard.ValidTo <= DateTime.Now)
            {
                throw new ValidationException("The identity card is not valid anymore (expiration)");
            }

            return identityCard;
        }

        public bool IsAdult()
        {
            var age = CalculateAge(Birthdate, DateTime.Now);
            return age >= 18;
        }

        private int CalculateAge(DateTime birthDate, DateTime currentDate)
        {
            int age = currentDate.Year - birthDate.Year;

            // Check if the birth date hasn't occurred this year yet
            if (currentDate.Month < birthDate.Month || (currentDate.Month == birthDate.Month && currentDate.Day < birthDate.Day))
            {
                age--;
            }

            return age;
        }

        public bool IsValid()
        {
            return ValidTo > ValidFrom && ValidTo > DateTime.Now;
        }

        public bool IsValidNationNumber()
        {
            Regex regexNationNumber = new Regex(@"^\d{3}\.\d{2}\.\d{2}-\d{3}-\d{2}$");
            return regexNationNumber.Match(NationalNumber).Success;
        }
    }
}

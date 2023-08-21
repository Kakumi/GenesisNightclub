using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GenesisNightclub.Domain.Models
{
    public class MemberCard
    {
        public int Id { get; set; }

        private MemberCard(int id)
        {
            Id = id;
        }

        internal MemberCard() { }

        public static MemberCard Create(int id)
        {
            return new MemberCard(id);
        }
    }
}

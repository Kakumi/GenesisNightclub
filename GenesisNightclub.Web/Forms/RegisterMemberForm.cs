using System.ComponentModel.DataAnnotations;

namespace GenesisNightclub.Web.Forms
{
    public class RegisterMemberForm
    {
        [Required]
        public IdentityCardForm? IdentityCard { get; set; }
        [Required]
        public MemberCardForm? MemberCard { get; set; }
        [Required]
        public string? Contact { get; set; }
    }
}

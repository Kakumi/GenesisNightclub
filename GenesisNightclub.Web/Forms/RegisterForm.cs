namespace GenesisNightclub.Web.Forms
{
    public class RegisterForm
    {
        public IdentityCardForm? IdentityCard { get; set; }
        public MemberCardForm? MemberCard { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}

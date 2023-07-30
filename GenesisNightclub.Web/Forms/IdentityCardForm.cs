namespace GenesisNightclub.Web.Forms
{
    public class IdentityCardForm
    {
        public int CardNumber { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public DateTime Birthdate { get; set; }
        public string NationalNumber { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}

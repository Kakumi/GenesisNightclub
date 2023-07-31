using System.ComponentModel.DataAnnotations;

namespace GenesisNightclub.Web.Forms
{
    public class IdentityCardForm
    {
        [Required]
        public int Number { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public DateTime? Birthdate { get; set; }
        [Required]
        public string NationalNumber { get; set; }
        [Required]
        public DateTime? ValidFrom { get; set; }
        [Required]
        public DateTime? ValidTo { get; set; }
    }
}

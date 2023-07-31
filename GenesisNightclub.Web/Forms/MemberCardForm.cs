using System.ComponentModel.DataAnnotations;

namespace GenesisNightclub.Web.Forms
{
    public class MemberCardForm
    {
        [Required]
        public int Id { get; set; }
    }
}

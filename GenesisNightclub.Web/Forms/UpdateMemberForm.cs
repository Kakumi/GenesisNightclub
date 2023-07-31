using System.ComponentModel.DataAnnotations;

namespace GenesisNightclub.Web.Forms
{
    public class UpdateMemberForm
    {
        public string? Lastname { get; set; }
        public string? Firstname { get; set; }
        public string? Contact { get; set; }
        public DateTime? EndBlacklisted { get; set; }
    }
}

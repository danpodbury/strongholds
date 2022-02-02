using System.ComponentModel.DataAnnotations;

namespace Strongholds.Models
{
    public class UserData
    {
        public string? name { get; set; }
        public string? email { get; set; } = string.Empty;
        public string? password { get; set; }
        public string? username { get; set; }

        [Display(Name="new-password")]
        public string? newPassword { get; set; }
    }
}

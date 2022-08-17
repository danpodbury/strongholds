using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StrongholdsUtil.Models
{
    public class Login: IResponse
    {
        [Key]
        public int LoginID { get; set; }

        public string Username { get; set; }

        public string HashedToken { get; set; }

        // [NotMapped]
        // leave mapped for dev only
        public string? Token { get; set; }

        [NotMapped]
        public Error? Error { get; set; }


        [InverseProperty("Login")]
        public virtual List<Robot>? Robots { get; set; }
        
        [InverseProperty("Login")]
        public virtual List<Station>? Stations { get; set; }
    }

    public interface IResponse
    {
        [NotMapped]
        public Error? Error { get; set; }
    }

}

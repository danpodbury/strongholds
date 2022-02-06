using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StrongholdsAPI.Models
{
    public class Login: IResponse
    {
        [Key]
        public int LoginID { get; set; }

        public string Username { get; set; }
        
        public string HashedToken { get; set; }

        //[NotMapped] dev only
        public string? Token { get; set; }

        [NotMapped]
        public Error? Error { get; set; }


        //[InverseProperty("Login")]
        //public virtual List<Robot>? Robots { get; set; }
        //
        //[InverseProperty("LoginID")]
        //public virtual List<Station>? Stations { get; set; }
    }

    public interface IResponse
    {
        [NotMapped]
        public Error? Error { get; set; }
    }

}

using System.ComponentModel.DataAnnotations;

namespace StrongholdsAPI.Models
{
    public class Error
    {
        [Key]
        public int ErrorID { get; set; }
        public string ErrorText { get; set; }

    }

}

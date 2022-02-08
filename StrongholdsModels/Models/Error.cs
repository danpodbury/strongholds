using System.ComponentModel.DataAnnotations;

namespace StrongholdsUtil.Models
{
    public class Error
    {
        [Key]
        public int ErrorID { get; set; }
        public string ErrorText { get; set; }

    }

}

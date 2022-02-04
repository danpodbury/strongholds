using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StrongholdsAPI.Models
{
    public class Robot
    {
        [Key]
        public int RobotID { get; set; }
        public string Name { get; set; }
        public float latitude { get; set; } = 0f;
        public float longitude { get; set; } = 0f;

    }

}

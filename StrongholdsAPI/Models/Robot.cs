using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StrongholdsAPI.Models
{
    public class Robot
    {
        [Key]
        public int RobotID { get; set; }
        public string Name { get; set; }
        public Coords Coordinates { get; set; }

    }


    [NotMapped]
    public class Coords
    {
        public float latitude { get; set; }

        public float longitude { get; set; }  
    }
}

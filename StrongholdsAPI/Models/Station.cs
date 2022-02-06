using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StrongholdsAPI.Models
{
    public class Station
    {
        [Key]
        public int StationID { get; set; }

        [ForeignKey("Login")]
        public int LoginID { get; set; }

        public float Oxygen { get; set; } = 100f;
        public float Power { get; set; } = 100f;
        public List<Robot> Robots { get; set; } = new List<Robot>();

        public float latitude { get; set; } = 0f;
        public float longitude { get; set; } = 0f;

    }

}

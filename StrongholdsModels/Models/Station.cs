using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StrongholdsUtil.Models
{
    public class Station
    {
        [Key]
        public int StationID { get; set; }

        [ForeignKey("Login")]
        public int LoginID { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Login Login { get; set; }

        // Robots docked at this station
        //[InverseProperty("Station")]
        //public virtual List<Robot>? Robots { get; set; }


        public float Oxygen { get; set; } = 100f;
        public float Power { get; set; } = 100f;
        public float latitude { get; set; } = 0f;
        public float longitude { get; set; } = 0f;

    }

}

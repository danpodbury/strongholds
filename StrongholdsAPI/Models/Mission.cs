using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StrongholdsAPI.Models
{
    public class Mission
    {
        [Key]
        public int MissionID { get; set; }

        [ForeignKey("Login")]
        public int LoginID { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Login Login { get; set; }

        [InverseProperty("Mission")]
        public virtual List<Objective> Objectives { get; set; }

    }

    public class Objective
    {
        [ForeignKey("Mission")]
        public int MissionID { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Mission Mission { get; set; }

        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public Activity Act { get; set; }
    }

    public enum Activity
    {
        Mine,
        Scan,
        Collect,
        Dig,
        Find
    }
}

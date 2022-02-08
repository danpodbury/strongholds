using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StrongholdsUtil.Models
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
        public virtual List<Objective> Objectives { get; set; } = new List<Objective>();

    }

    public class Objective
    {
        [Key]
        public int ObjectiveID { get; set; }

        [ForeignKey("Mission")]
        public int MissionID { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Mission Mission { get; set; }

        public float Latitude { get; set; } = 0f;
        public float Longitude { get; set; } = 0f;

        public Act Action { get; set; } = Act.Scan;
    }

    public enum Act
    {
        Go,
        Mine,
        Scan,
        Collect,
        Dig,
        Find
    }
}

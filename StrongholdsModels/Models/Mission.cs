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
        public virtual List<Objective> Objectives { get; set; }

    }

    public class Objective
    {
        [Key]
        public int ObjectiveID { get; set; }

        [ForeignKey("Mission")]
        public int MissionID { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Mission Mission { get; set; }

        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public Act Action { get; set; }
    }

    public enum Act
    {
        Mine,
        Scan,
        Collect,
        Dig,
        Find
    }
}

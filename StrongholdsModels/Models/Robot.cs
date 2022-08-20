using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace StrongholdsUtil.Models
{
    public class Robot
    {
        [Key]
        public int RobotID { get; set; }


        [ForeignKey("Login")]
        public int LoginID { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Login Login { get; set; }

        public string Name { get; set; } = "bot";
        public float Latitude { get; set; } = 0f;
        public float Longitude { get; set; } = 0f;
        public float Battery { get; set; } = 100f;
        public int Memory { get; set; } = 8;
        public float Speed { get; set; } = 1f;

        public RobotStatus Status { get; set; } = Robot.RobotStatus.Idle;

        //[InverseProperty("Robot")]
        //public virtual Mission? Mission { get; set; }

        public void step(Mission mission)
        {
            if (Status == RobotStatus.OnMission)
            {
                // Go idle if no active mission
                if (mission == null) {
                    Status = RobotStatus.Idle;
                    return; 
                }

                // Execute objectives until none left
                if (mission.Objectives.Count > 0)
                {
                    var currObj = mission.Objectives[0];
                    if (atPosition(currObj.Latitude, currObj.Longitude))
                    {
                        // Pop objective
                        Console.WriteLine($"At objective [{mission.Objectives[0].ObjectiveID}] ({currObj.Latitude}, {currObj.Longitude})");
                        mission.Objectives.RemoveAt(0);
                    } else
                    {
                        // Get direction vec
                        var lat = mission.Objectives.First().Latitude;
                        var lon = mission.Objectives.First().Longitude;

                        // TODO: move to vector type and method
                        var vec_x = lon - Longitude;
                        var vec_y = lat - Latitude;

                        // Normalise vector
                        var mag = MathF.Sqrt(MathF.Pow(vec_x, 2) + MathF.Pow(vec_y, 2));
                        vec_x = (mag != 0f) ? vec_x / mag : 0f;
                        vec_y = (mag != 0f) ? vec_y / mag : 0f;

                        // Move toward next objective
                        Longitude += vec_x * Speed * 0.1f * 10f;
                        Latitude  += vec_y * Speed * 0.1f * 10f;


                        Battery -= 0.1f;
                    }

                } else
                {
                    // Return to base
                    Console.WriteLine($"Mission [{mission.MissionID}] complete. Returning to base.");

                    Latitude = 0f;
                    Longitude = 0f;
                    Status = RobotStatus.Idle;
                }
            }
        }

        private bool atPosition(float lat, float lon, float radius = 0.5f)
        {
            return (MathF.Abs(lat - this.Latitude) < radius) && (MathF.Abs(lon - this.Longitude) < radius);
        }

        public enum RobotStatus
        {
            Idle,
            OnMission,
            Offline
        }
    }

 
}

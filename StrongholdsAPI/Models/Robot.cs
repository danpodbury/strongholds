﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StrongholdsAPI.Models
{
    public class Robot
    {
        [Key]
        public int RobotID { get; set; }


        [ForeignKey("Login")]
        public int LoginID { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Login Login { get; set; }


        [ForeignKey("Station")]
        public int StationID { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Station? Station { get; set; }


        public string Name { get; set; } = "bot";
        public float Latitude { get; set; } = 0f;
        public float Longitude { get; set; } = 0f;
        public float Battery { get; set; } = 100f;
        public int Memory { get; set; } = 8;
        public float Speed { get; set; } = 10f;

        public virtual Mission? Mission { get; set; }

        public void step()
        {
            if (RobotID % 2 == 0)
            {
                Latitude += 0.001f;
                Longitude += 0.001f;
            }
        }
    }

 
}

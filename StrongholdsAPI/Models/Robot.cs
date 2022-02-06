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
        public virtual Login Login { get; set; }

        public string Name { get; set; }
        public float latitude { get; set; } = 0f;
        public float longitude { get; set; } = 0f;


        public void step()
        {
            if (RobotID % 2 == 0)
            {
                latitude += 0.001f;
                longitude += 0.001f;
            }
        }
    }

}

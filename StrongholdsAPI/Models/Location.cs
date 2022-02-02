using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StrongholdsAPI.Models
{
    public class Location
    {
        [Key]
        public int LocationID { get; set; }
        public string Name { get; set; }
        public Coords Coordinates { get; set; }

    }

}

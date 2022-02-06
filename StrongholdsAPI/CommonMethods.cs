using StrongholdsAPI.Models;

namespace StrongholdsAPI
{
    public class CommonMethods
    {
        public Robot CreateDefaultRobot()
        {
            return new Robot
            {
                Name = "bot01",

            };
        }
    }
}

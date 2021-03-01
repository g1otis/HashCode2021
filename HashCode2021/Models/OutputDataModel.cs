using System.Collections.Generic;

namespace HashCode2021.Models
{
    public class OutputDataModel
    {
        public Dictionary<int, IntersectionOptions> Intersections { get; set; } = new Dictionary<int, IntersectionOptions>();
    }

    public class IntersectionOptions
    {
        public int Id { get; set; }

        public List<StreetOptions> IncomingStreets { get; set; } = new List<StreetOptions>();
    }

    public class StreetOptions
    {
        public string StreetName { get; set; }
        public int GreenLightDuration { get; set; }
    }
}

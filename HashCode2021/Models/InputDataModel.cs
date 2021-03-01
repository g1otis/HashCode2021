using System.Collections.Generic;

namespace HashCode2021.Models
{
    public class InputDataModel
    {
        public long TotalDuration { get; set; }
        public HashSet<int> IntersectionIds { get; set; } = new();

        public Dictionary<string, Street> Streets { get; set; } = new();

        public HashSet<Car> Cars { get; set; } = new();

        public int Bonus { get; set; }
    }

    public class Car
    {
        public List<string> JourneyStreets { get; set; }
    }

    public class Street
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public int StartIntersection { get; set; }
        public int EndIntersection { get; set; }
    }
}

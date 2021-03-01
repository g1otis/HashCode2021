using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode2021.Models
{
    public class GraphModel
    {
        public Street[,]IntersectionsGraph { get; set; }

        public GraphModel(InputDataModel model)
        {
            IntersectionsGraph = new Street[model.IntersectionIds.Count, model.IntersectionIds.Count];

            model.Streets.Values.ToList().ForEach(street => IntersectionsGraph[street.StartIntersection, street.EndIntersection] = street);
        }

        public List<int> GetAdjacentIntersectionsLeadToIntersection(int intersectionId)
        {
            var adjacents = new List<int>();

            for (int i = 0; i < IntersectionsGraph.Length; i++)
            {
                var street = IntersectionsGraph[i, intersectionId];
                if (street != null)
                {
                    adjacents.Add(i);
                }
            }

            return adjacents;
        }
    }
}

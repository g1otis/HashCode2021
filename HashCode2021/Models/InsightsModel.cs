using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode2021.Models
{
    public class InsightsModel
    {
        public GraphModel GraphModel { get; set; }
        public Dictionary<string, int> TotalCarsPassByStreet { get; set; } = new();

        public InsightsModel(InputDataModel model)
        {
            GraphModel = new GraphModel(model);

            model.Cars.ToList()
                .ForEach(
                    car => car.JourneyStreets
                        .ForEach(
                            streetName =>
                            {
                                if (TotalCarsPassByStreet.ContainsKey(streetName))
                                {
                                    TotalCarsPassByStreet[streetName]++;
                                    return;
                                }

                                TotalCarsPassByStreet[streetName] = 1;
                            }));
        }
    }
}
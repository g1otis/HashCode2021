using System.Linq;
using System.Threading.Tasks;
using HashCode2021.Models;

namespace HashCode2021.Optimizers
{
    class IntersectionsWithoutTrafficOptimizer : IOptimizer
    {
        /// <inheritdoc />
        public async Task OptimizeAsync(InputDataModel inputModel,
                                        InsightsModel insightsModel,
                                        OutputDataModel outputModel)
        {
            await Task.Run(
                () =>
                {
                    outputModel.Intersections.Values
                        .ToList()
                        .ForEach(
                            intersectionOptions =>
                                intersectionOptions.IncomingStreets = intersectionOptions.IncomingStreets
                                    .Where(
                                        street => insightsModel.TotalCarsPassByStreet.TryGetValue(street.StreetName, out var totalCars)
                                                  && totalCars > 0)
                                    .ToList());

                    outputModel.Intersections = outputModel.Intersections
                        .Where(pair => pair.Value.IncomingStreets.Any())
                        .ToDictionary(pair => pair.Key, pair => pair.Value);
                });
        }
    }
}
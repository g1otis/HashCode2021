using System.Linq;
using System.Threading.Tasks;
using HashCode2021.Models;

namespace HashCode2021.Processors
{
    public class RandomProcessor : IProcessor
    {
        private const int RandomDuration = 7;
        /// <inheritdoc />
        public async Task<OutputDataModel> ProcessAsync(InputDataModel input, InsightsModel graphModel)
        {
            return await Task.Run(
                () =>
                {
                    var outputDataModel = new OutputDataModel();

                    var intersWithIncomStreets = input.Streets.Values
                        .Select(street => (street.EndIntersection, street.Name))
                        .GroupBy(tuple => tuple.EndIntersection, tuple => tuple.Name)
                        .ToList();

                    outputDataModel.Intersections = intersWithIncomStreets
                        .Select(
                            grouping => new IntersectionOptions
                            {
                                Id = grouping.Key,
                                IncomingStreets = grouping
                                    .Select(
                                        s => new StreetOptions
                                        {
                                            GreenLightDuration = RandomDuration,
                                            StreetName = s
                                        })
                                    .ToList()
                            })
                        .ToDictionary(options => options.Id, options => options);
            
                    return outputDataModel;
                });
        }
    }
}
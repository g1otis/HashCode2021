using System.Linq;
using System.Threading.Tasks;
using HashCode2021.Models;

namespace HashCode2021.Processors
{
    public class SlightlySmartProcessor : IProcessor
    {
        /// <inheritdoc />
        public async Task<OutputDataModel> ProcessAsync(InputDataModel input, InsightsModel graphModel)
        {
            return await Task.Run(
                () =>
                {
                    var outputDataModel = new OutputDataModel();

                    var intersWithIncomStreets = input.Streets.Values
                        .Select(street => (street.EndIntersection, street))
                        .GroupBy(tuple => tuple.EndIntersection, tuple => tuple.street)
                        .ToList();

                    outputDataModel.Intersections = intersWithIncomStreets
                        .Select(
                            grouping =>
                            {
                                return new IntersectionOptions
                                {
                                    Id = grouping.Key,
                                    IncomingStreets = grouping
                                        .Select(
                                            street => new StreetOptions
                                            {
                                                GreenLightDuration = street.Duration/grouping.Count(),
                                                StreetName = street.Name
                                            })
                                        .ToList()
                                };
                            })
                        .ToDictionary(options => options.Id, options => options);
            
                    return outputDataModel;
                });
        }
    }
}

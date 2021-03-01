using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HashCode2021.Models;

namespace HashCode2021.Aggregators
{
    class FirstPrevailsAndApplyAverageAggregator : IAggregator
    {
        /// <inheritdoc />
        public async Task<OutputDataModel> AggregateAsync(List<OutputDataModel> models)
        {
            return await Task.Run(
                () =>
                {
                    if (models.Count == 1)
                    {
                        return models.First();
                    }

                    var outputModel = new OutputDataModel();

                    var firstOutputDataModel = models.First();

                    foreach (var idAndIntersection in firstOutputDataModel.Intersections)
                    {
                        var intersectionId = idAndIntersection.Key;
                        var streets = idAndIntersection.Value.IncomingStreets;
                        var streetsByName = streets.ToDictionary(options => options.StreetName, options => options);

                        var intersectionFromOtherOutputs = models.Skip(1)
                            .Where(model => model.Intersections.ContainsKey(intersectionId))
                            .Select(model => model.Intersections[intersectionId])
                            .ToList();

                        var foundStreets = intersectionFromOtherOutputs
                            .SelectMany(
                                fromOther =>
                                    fromOther.IncomingStreets
                                        .Where(incStreet => streetsByName.ContainsKey(incStreet.StreetName)))
                            .GroupBy(incStreet => incStreet.StreetName)
                            .ToDictionary(grouping => grouping.Key, grouping => grouping.ToList());

                        var intersectionOptionsUpdated = new IntersectionOptions();
                        var streetOptionsUpdated = streets
                            .Select(
                                street =>
                                {
                                    if (foundStreets.TryGetValue(street.StreetName, out var relatedStreets))
                                    {
                                        return new StreetOptions
                                        {
                                            StreetName = street.StreetName,
                                            GreenLightDuration =
                                                (street.GreenLightDuration
                                                 + relatedStreets.Sum(relStreet => relStreet.GreenLightDuration))
                                                / (1 + relatedStreets.Count)
                                        };
                                    }

                                    return street;
                                })
                            .ToList();

                        intersectionOptionsUpdated.Id = intersectionId;
                        intersectionOptionsUpdated.IncomingStreets.AddRange(
                            streetOptionsUpdated);

                        outputModel.Intersections.Add(intersectionId, intersectionOptionsUpdated);
                    }

                    return outputModel;
                });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashCode2021.Models;

namespace HashCode2021.Aggregators
{
    class FirstPrevailsAggregator : IAggregator
    {
        /// <inheritdoc />
        public async Task<OutputDataModel> AggregateAsync(List<OutputDataModel> models)
        {
            return await Task.Run(
                () =>
                {
                    return models.First();
                });
        }
    }
}

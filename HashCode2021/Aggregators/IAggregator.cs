// HashCode2021.HashCode2021.IAggregator.cs created by Panagiotis Foutros
// at 28/02/2021 12:06 AM

using System.Collections.Generic;
using System.Threading.Tasks;
using HashCode2021.Models;

namespace HashCode2021.Aggregators
{
    public interface IAggregator
    {
        Task<OutputDataModel> AggregateAsync(List<OutputDataModel> models);
    }
}
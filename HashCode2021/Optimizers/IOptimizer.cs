// HashCode2021.HashCode2021.IOptimizer.cs created by Panagiotis Foutros
// at 28/02/2021 1:26 AM

using System.Threading.Tasks;
using HashCode2021.Models;

namespace HashCode2021.Optimizers
{
    public interface IOptimizer
    {
        Task OptimizeAsync(InputDataModel inputModel, InsightsModel insightsModel, OutputDataModel outputModel);
    }
}
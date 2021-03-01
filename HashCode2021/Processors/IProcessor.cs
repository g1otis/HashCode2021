using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashCode2021.Models;

namespace HashCode2021.Processors
{
    public interface IProcessor
    {
        Task<OutputDataModel> ProcessAsync(InputDataModel input, InsightsModel graphModel);
    }
}

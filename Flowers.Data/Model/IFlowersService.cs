using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flowers.Model
{
    public interface IFlowersService
    {
        Task<IList<Flower>> Refresh();

        Task<bool> Save(Flower flower);
    }
}
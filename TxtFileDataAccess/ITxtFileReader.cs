using System.Collections.Generic;
using System.Threading.Tasks;

namespace TxtFileDataAccess
{
    public interface ITxtFileReader
    {
        Task<IEnumerable<string>> ReadAllLinesAsync();
    }
}
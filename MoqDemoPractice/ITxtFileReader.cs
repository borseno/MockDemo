using System.Threading.Tasks;

namespace MoqDemoPractice
{
    public interface ITxtFileReader
    {
        Task<string[]> ReadAllLinesAsync();
    }
}
using System.IO;
using System.Threading.Tasks;

namespace MoqDemoPractice
{
    public class TxtFileReader : ITxtFileReader
    {
        private readonly string _path;

        public TxtFileReader(string path) => _path = path;

        public Task<string[]> ReadAllLinesAsync()
            => File.ReadAllLinesAsync(_path);
    }
}
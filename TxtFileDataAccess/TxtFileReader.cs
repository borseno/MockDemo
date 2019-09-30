using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TxtFileDataAccess
{
    public class TxtFileReader : ITxtFileReader
    {
        private readonly string _path;

        public TxtFileReader(string path) => _path = path;

        public async Task<IEnumerable<string>> ReadAllLinesAsync()
            => await File.ReadAllLinesAsync(_path);
    }
}
using SharedModels;
using System;
using System.Threading.Tasks;
using TxtFileDataAccess;

namespace MoqDemoPractice
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var reader = new TxtFileReader("dataStorage.txt");

            var fileAccessor = new TxtFileDataAccess<User>(reader);

            var users = await fileAccessor.LoadDataAsync();

            Console.WriteLine(String.Join(Environment.NewLine, users));

            return 0;
        }
    }
}

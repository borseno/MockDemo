using System;
using System.Threading.Tasks;

namespace MoqDemoPractice
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var reader = new TxtFileReader("dataStorage.txt");

            var fileAccessor = new TxtFileDataAccess(reader);

            var users = await fileAccessor.LoadDataAsync<User>();

            Console.WriteLine(String.Join(Environment.NewLine, users));

            return 0;
        }
    }
}

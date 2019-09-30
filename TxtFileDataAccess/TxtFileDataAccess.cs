using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MoqDemoPractice
{
    public class TxtFileDataAccess<T>
    {
        private readonly ITxtFileReader _reader;

        public TxtFileDataAccess(ITxtFileReader reader) => _reader = reader;

        public async Task<ICollection<T>> LoadDataAsync()
        {
            var rows = await _reader.ReadAllLinesAsync();

            return rows.Select(i => ReadRowAs(i)).ToList();
        }

        private T ReadRowAs(string row)
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            var instance = Activator.CreateInstance<T>();

            foreach (var i in properties)
            {
                var name = i.Name;
                var propertyType = i.PropertyType;

                if (propertyType != typeof(int) && propertyType != typeof(string))
                {
                    throw new NotSupportedException("types of properties other than string and int arent supported");
                }

                var match = Regex.Match(row, $"{name}=[^;]*;");

                if (!match.Success)
                {
                    throw new InvalidOperationException(
                        "Invalid class for data, the following property couldn't be found in a row: " 
                        + name);
                }

                var leftBorderOfValueLength = name.Length + 1;
                var valueLength = match.Length - 1 - leftBorderOfValueLength;

                var value = match.Value.AsSpan().Slice(name.Length + 1, valueLength);

                if (propertyType == typeof(int))
                {
                    i.SetValue(instance, Int32.Parse(value));
                }
                else
                {
                    i.SetValue(instance, value.ToString());
                }
            }

            return instance;
        }
    }
}

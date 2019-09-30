using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TxtFileDataAccess
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

                if (!Validate(propertyType))
                {
                    throw new NotSupportedException
                        ("only primitives, string and decimal are supported. Your type: " + propertyType);
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

                var castedValue = ConvertToType(value, propertyType);
                
                i.SetValue(instance, castedValue);
            }

            return instance;
        }

        private object ConvertToType(ReadOnlySpan<char> value, Type propertyType)
        {   
            if (propertyType == typeof(int))
            {
                return Int32.Parse(value);
            }
            if (propertyType == typeof(short))
            {
                return Int16.Parse(value);
            }
            if (propertyType == typeof(byte))
            {
                return Byte.Parse(value);
            }
            if (propertyType == typeof(char))
            {
                if (value.Length == 1)
                {
                    return value[0];
                }
                else if (value.Length == 0)
                {
                    return default(char);
                }
                else
                {
                    throw new InvalidDataException
                        ("Data has invalid format. Char value contained more than 1 character");
                }
            }
            if (propertyType == typeof(sbyte))
            {
                return SByte.Parse(value);
            }
            if (propertyType == typeof(long))
            {
                return Int64.Parse(value);
            }
            if (propertyType == typeof(ushort))
            {
                return UInt16.Parse(value);
            }
            if (propertyType == typeof(uint))
            {
                return UInt32.Parse(value);
            }
            if (propertyType == typeof(ulong))
            {
                return UInt64.Parse(value);
            }
            if (propertyType == typeof(string))
            {
                return value.ToString();
            }
            if (propertyType == typeof(float))
            {
                return Single.Parse(value);
            }
            if (propertyType == typeof(double))
            {
                return Double.Parse(value);
            }
            if (propertyType == typeof(decimal))
            {
                return Decimal.Parse(value);
            }
            if (propertyType == typeof(bool))
            {
                return Boolean.Parse(value);
            }

            throw new NotSupportedException("Type is not supported. Type was: " + propertyType);
        }

        private static bool Validate(Type propertyType)
        {
            return 
                propertyType.IsPrimitive || 
                propertyType == typeof(decimal) ||
                propertyType == typeof(string);
        }
    }
}

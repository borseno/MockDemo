using Autofac.Extras.Moq;
using MoqDemoPractice;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TestsMoqDemo
{
    public partial class LoadDataAsyncTests
    {
        [Fact]
        public void LoadDataAsync_ShouldConvertRowsToCLRObjects()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ITxtFileReader>()
                    .Setup(reader => reader.ReadAllLinesAsync())
                    .Returns(GetSampleRowsAsync());

                var accessor = mock.Create<TxtFileDataAccess>();

                var expected = GetSampleUsers();
                var actual = accessor.LoadDataAsync<User>().GetAwaiter().GetResult().ToList();

                Assert.True(actual != null);
                Assert.Equal(expected.Count, actual.Count);

                for (int i = 0; i < expected.Count; i++)
                {
                    Assert.Equal(expected[i].Age, actual[i].Age);
                    Assert.Equal(expected[i].Name, actual[i].Name);
                }
            }
        }

        private List<User> GetSampleUsers()
        {
            return new List<User>
            {
                new User
                {
                    Age = 17,
                    Name = "Antony"
                },
                new User
                {
                    Age = 44,
                    Name = "Lesh"
                },
                new User
                {
                    Age = 14,
                    Name = "Bagoon"
                },
                new User
                {
                    Age = 30,
                    Name = "Richardello"
                }
            };
        }
        private string[] GetSampleRows()
        {
            var users = GetSampleUsers();

            var rows = users.Select(i => $"Name={i.Name};Age={i.Age};");

            return rows.ToArray();
        }
        private Task<string[]> GetSampleRowsAsync()
        {
            var rows = GetSampleRows();

            return Task.FromResult(rows);
        }
    }
}

using HeadlessAPI.DataProvider;
using HeadlessAPI.Web;
using System.Collections.Generic;
using Xunit;
using Newtonsoft.Json;

namespace HeadlessAPI.Test
{
    public class MSSQLProviderItemsTest
    {
        private IRepository _repo;
        private ContentType _testContentType => new ContentType
        {
            Name = "test",
            Columns = new List<Column> {
                new Column {
                    Name = "column1",
                    Type = "nvarchar"
                },
                new Column {
                    Name = "column2",
                    Type = "nvarchar"
                },
            }
        };
        public MSSQLProviderItemsTest()
        {
            _repo = new MSSQLProvider();

            _repo.ClearTable("ContentTypes");
            try
            {
                _repo.DeleteTable(_testContentType.Name);
            }
            catch { }
        }

        [Fact]
        public void CreateItemsTest()
        {
            _repo.CreateItem(_testContentType.Name, "");
            Assert.Equal(1, _repo.GetAllItems(_testContentType.Name).Count);
        }

        [Fact]
        public void DeleteContentTypeTest()
        {
            var id = _repo.CreateItem(_testContentType.Name, "");
            _repo.DeleteItem(_testContentType.Name, id);
            Assert.Equal(0, _repo.GetAllItems(_testContentType.Name).Count);
        }

        [Fact]
        public void GetItemTest()
        {
            var id = _repo.CreateItem(_testContentType.Name, "");
            var item = _repo.GetItem(_testContentType.Name, id);

            Assert.NotNull(item);
        }

        [Fact]
        public void GetAllItemsTest()
        {
            _repo.GetAllItems(_testContentType.Name);
            Assert.Equal(1, _repo.GetAllItems(_testContentType.Name).Count);
        }
    }
}

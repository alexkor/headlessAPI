using HeadlessAPI.DataProvider;
using HeadlessAPI.Web;
using System.Collections.Generic;
using Xunit;
using Newtonsoft.Json;

namespace HeadlessAPI.Test
{
    public partial class MSSQLProviderContentTypesTest
    {
        private IRepository _repo;
        public MSSQLProviderContentTypesTest()
        {
            _repo = new MSSQLProvider();

            _repo.ClearTable("ContentTypes");
            try
            {
                _repo.DeleteTable(TestContentType.Name);
            }
            catch { }
        }

        private ContentType TestContentType => new ContentType
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

        [Fact]
        public void CreateContentTypeTest()
        {
            _repo.CreateContentType(TestContentType);
            Assert.Equal(1, _repo.GetAllContentTypes().Count);
        }

        [Fact]
        public void DeleteContentTypeTest()
        {
            var id = _repo.CreateContentType(TestContentType);
            _repo.DeleteContentType(id);
            Assert.Equal(0, _repo.GetAllContentTypes().Count);
        }

        [Fact]
        public void GetContentTypeTest()
        {
            var id = _repo.CreateContentType(TestContentType);
            var contentType = _repo.GetContentType(id);

            Assert.NotNull(contentType);
            Assert.Equal("test", contentType.Name);
        }

        [Fact]
        public void GetAllContentTypesTest()
        {
            _repo.CreateContentType(TestContentType);

            Assert.Equal(1, _repo.GetAllContentTypes().Count);
        }
    }
}

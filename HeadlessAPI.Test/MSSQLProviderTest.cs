using HeadlessAPI.DataProvider;
using HeadlessAPI.Web;
using System.Collections.Generic;
using Xunit;
using Newtonsoft.Json;

namespace HeadlessAPI.Test
{
    public class MSSQLProviderTest
    {
        public MSSQLProviderTest()
        {

        }

        [Fact]
        public void CreateContentTypeTest()
        {
            IRepository _repo = new MSSQLProvider();
            var columns = new Dictionary<string, string>();
            columns.Add("column1", "nvarchar");
            columns.Add("column2", "nvarchar");
            columns.Add("column3", "nvarchar");

            var contentType = new ContentType
            {
                Name = "test",
                Columns = JsonConvert.SerializeObject(columns)
            };

            _repo.CreateContentType(contentType);
        }

        [Fact]
        public void DeleteContentTypeTest()
        {
            IRepository _repo = new MSSQLProvider();
            _repo.DeleteContentType(1);
        }

        [Fact]
        public void GetContentTypeTest()
        {
            IRepository _repo = new MSSQLProvider();
            var contentType = _repo.GetContentType(2);

            Assert.Equal("test", contentType.Name);
        }

        [Fact]
        public void GetAllContentTypesTest()
        {
            IRepository _repo = new MSSQLProvider();
            var contentTypes = _repo.GetAllContentTypes();

            Assert.Equal(1, contentTypes.Count);
        }
    }
}

using System.Collections.Generic;

namespace HeadlessAPI.Web
{
    public interface IRepository
    {
        ICollection<ContentType> GetAllContentTypes();
        ContentType GetContentType(int id);
        int CreateContentType(ContentType value);
        void DeleteContentType(int id);
        void UpdateContentType(ContentType value);
        void ClearTable(string v);
        void DeleteTable(string v);
    }
    public class ContentType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Column> Columns { get; set; }
    }

    public class Column
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}

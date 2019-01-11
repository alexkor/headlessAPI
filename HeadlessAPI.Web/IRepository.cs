using System.Collections.Generic;

namespace HeadlessAPI.Web
{
    public interface IRepository
    {
        ICollection<ContentType> GetAllContentTypes();
        ContentType GetContentType(int id);
        void CreateContentType(ContentType value);
        void DeleteContentType(int id);
        void UpdateContentType(ContentType value);
    }
    public class ContentType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Columns { get; set; }
    }
}

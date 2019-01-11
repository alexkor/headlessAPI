using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace HeadlessAPI.Web
{
    public interface IRepository
    {
        ICollection<ContentType> GetAllContentTypes();
        ContentType GetContentType(int id);
        int CreateContentType(ContentType value);
        void DeleteContentType(int id);
        void UpdateContentType(int id, ContentType value);
        void ClearTable(string v);
        void DeleteTable(string v);

        ICollection<string> GetAllItems(string tableName);
        string GetItem(string tableName, int id);
        void UpdateItem(string tableName, int id, string jsonItem);
        int CreateItem(string tableName, string jsonItem);
        void DeleteItem(string tablename, int id);
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

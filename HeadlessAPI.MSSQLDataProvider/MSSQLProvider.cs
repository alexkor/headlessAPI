using HeadlessAPI.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace HeadlessAPI.DataProvider
{
    public class MSSQLProvider : IRepository
    {
        private SqlConnection _conn;
        public MSSQLProvider()
        {
            _conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HeadlessAPI;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            _conn.Open();
        }

        public void ClearTable(string tableName)
        {
            string command = $@"TRUNCATE TABLE {tableName}";
            var sqlCommand = new SqlCommand(command, _conn);
            sqlCommand.ExecuteNonQuery();
        }

        public void DeleteTable(string tableName)
        {
            string command = $@"DROP TABLE {tableName}";
            var sqlCommand = new SqlCommand(command, _conn);
            sqlCommand.ExecuteNonQuery();
        }

        public int CreateContentType(ContentType contentType)
        {
            string columnScheme = string.Join(',', contentType.Columns.Select(col => $"[{col.Name}] [{col.Type}]"));
            string command = $@"CREATE TABLE {contentType.Name} ([Id] [int] IDENTITY(1,1) PRIMARY KEY, {columnScheme})";
            var sqlCommand = new SqlCommand(command, _conn);
            sqlCommand.ExecuteNonQuery();

            var scheme = JsonConvert.SerializeObject(contentType.Columns);
            command = $"INSERT INTO ContentTypes ([Name], [Columns]) OUTPUT Inserted.ID VALUES ('{contentType.Name}', '{scheme}')";
            sqlCommand = new SqlCommand(command, _conn);
            using (var reader = sqlCommand.ExecuteReader())
            {
                reader.Read();
                return (int)reader[0];
            }
        }

        public void DeleteContentType(int id)
        {
            var contentType = GetContentType(id);

            var command = $@"DROP TABLE {contentType.Name}";
            var sqlCommand = new SqlCommand(command, _conn);
            sqlCommand.ExecuteNonQuery();

            command = $@"DELETE FROM ContentTypes WHERE Id = '{id}'";
            sqlCommand = new SqlCommand(command, _conn);
            sqlCommand.ExecuteNonQuery();
        }

        public ICollection<ContentType> GetAllContentTypes()
        {
            ICollection<ContentType> contentTypes = new List<ContentType>();
            string command = $@"SELECT * FROM ContentTypes";
            var sqlCommand = new SqlCommand(command, _conn);
            using (var reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    contentTypes.Add(new ContentType
                    {
                        Name = (string)reader["Name"],
                        Columns = JsonConvert.DeserializeObject<ICollection<Column>>((string)reader["Columns"])
                    });
                }
            }

            return contentTypes;
        }

        public ContentType GetContentType(int id)
        {
            string command = $@"SELECT TOP 1 * FROM ContentTypes WHERE Id = {id}";
            var sqlCommand = new SqlCommand(command, _conn);
            using (var reader = sqlCommand.ExecuteReader())
            {
                reader.Read();
                ContentType contentType = new ContentType
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    Columns = JsonConvert.DeserializeObject<ICollection<Column>>((string)reader["Columns"]),
                };
                return contentType;
            }
        }

        public void UpdateContentType(ContentType value)
        {
            var command = $@"UPDATE ContentTypes 
                SET [Columns] = '{value.Columns}',
                    [Name] = '{value.Name}'
                WHERE Id = '{value.Id}'";
            var sqlCommand = new SqlCommand(command, _conn);
            sqlCommand.ExecuteNonQuery();
        }

        public void UpdateContentType(int id, ContentType value)
        {
            throw new NotImplementedException();
        }

        public ICollection<string> GetAllItems(string tableName)
        {
            string command = $@"SELECT * FROM {tableName}";
            var sqlCommand = new SqlCommand(command, _conn);
            using (var reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {

                }

                return null;
            }
        }

        public string GetItem(string tableName, int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(string tableName, int id, string jsonItem)
        {
            throw new NotImplementedException();
        }

        public int CreateItem(string tableName, string jsonItem)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(string tablename, int id)
        {
            throw new NotImplementedException();
        }
    }
}

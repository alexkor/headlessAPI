using HeadlessAPI.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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

        public void CreateContentType(ContentType contentType)
        {
            List<string> columnScheme = new List<string>();
            foreach (var column in JsonConvert.DeserializeObject<Dictionary<string, string>>(contentType.Columns))
            {
                columnScheme.Add($"[{column.Key}] [{column.Value}]");
            }
            string command = $@"CREATE TABLE {contentType.Name} ([Id] [int] IDENTITY(1,1) PRIMARY KEY, {string.Join(',', columnScheme)})";
            var sqlCommand = new SqlCommand(command, _conn);
            sqlCommand.ExecuteNonQuery();

            var scheme = JsonConvert.SerializeObject(contentType);
            command = $"INSERT INTO ContentTypes ([Scheme]) VALUES ('{scheme}')";
            sqlCommand = new SqlCommand(command, _conn);
            sqlCommand.ExecuteNonQuery();
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
            string command = $@"SELECT [Scheme] FROM ContentTypes";
            var sqlCommand = new SqlCommand(command, _conn);
            using (var reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    var scheme = reader.GetFieldValue<string>(0);
                    var contentType = JsonConvert.DeserializeObject<ContentType>(scheme);
                    contentTypes.Add(contentType);
                }
            }

            return contentTypes;
        }

        public ContentType GetContentType(int id)
        {
            string command = $@"SELECT TOP 1 [Scheme] FROM ContentTypes WHERE Id = {id}";
            var sqlCommand = new SqlCommand(command, _conn);
            using (var reader = sqlCommand.ExecuteReader())
            {
                reader.Read();
                ContentType contentType = new ContentType
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    Columns = (string)reader["Columns"],
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
    }
}

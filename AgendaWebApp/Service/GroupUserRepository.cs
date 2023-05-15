using AgendaWebApp.Data;
using AgendaWebApp.Models;
using Microsoft.Data.SqlClient;

namespace AgendaWebApp.Service
{
    public class GroupUserRepository
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TodoContext;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public ICollection<GroupModel> GetGroupsByUser(string userId)
        {
            List<GroupModel> foundGroups = new List<GroupModel>();

            string sqlStatement = "SELECT g.GroupId, g.Name, g.Description " +
                "FROM dbo.GroupUser gu " +
                "JOIN dbo.Groups g " +
                "ON gu.GroupId = g.GroupId" +
                "WHERE UserId LIKE @userId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                //Setting the @Name parameter to the search term
                command.Parameters.AddWithValue("@Name", userId);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundGroups.Add(new GroupModel { GroupId = (int)reader[0], Name = (string)reader[1], Description = (string)reader[2] });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return foundGroups;
            }
        }
    }
}


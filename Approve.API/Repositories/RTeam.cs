using Approve.API.Models;
using Approve.API.Repositories;
using System.Data.SqlClient;

namespace Approve.API.Repositories
{
    public class RTeam
    {
        public static Task<IEnumerable<MTeam>> GetAllTeams()
        {
            List<MTeam> teams = new List<MTeam>();

            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.tblTeam", connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        MTeam team = new MTeam(reader.GetInt32(0), reader.GetString(1));
                        teams.Add(team);
                    }

                    reader.Close();
                }

                return Task.FromResult<IEnumerable<MTeam>>(teams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static Task<MTeam> GetTeamByID(int ID)
        {
            MTeam team;

            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.tblTeam where ID = @ID", connection);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                        team = new MTeam(reader.GetInt32(0), reader.GetString(1));
                    else
                        return null;

                    reader.Close();
                }

                return Task.FromResult<MTeam>(team);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static Task<MTeam> GetTeamByName(string Name)
        {
            MTeam team;

            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.tblTeam where Name = @Name", connection);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                        team = new MTeam(reader.GetInt32(0), reader.GetString(1));
                    else
                        return null;

                    reader.Close();
                }

                return Task.FromResult<MTeam>(team);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static Task<Int32> GetNextTeamID()
        {
            int nextID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(""))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT TOP 1 ID FROM tblTeam ORDER BY ID DESC;", connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        nextID = reader.GetInt32(0);
                    }

                    reader.Close();
                }

                return Task.FromResult<Int32>(nextID + 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static void DeleteTeam(MTeam team)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"DELETE FROM dbo.tblTeam where ID = @ID", connection);
                    cmd.Parameters.AddWithValue("@ID", team.ID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void CreateTeam(MTeam team)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.tblTeam VALUES (@ID, @Name)", connection);
                    cmd.Parameters.AddWithValue("@ID", team.ID);
                    cmd.Parameters.AddWithValue("@Name", team.Name);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void UpdateTeam(MTeam team)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"UPDATE dbo.tblTeam SET Name = @Name WHERE ID = @ID", connection);

                    cmd.Parameters.AddWithValue("@ID", team.ID);
                    cmd.Parameters.AddWithValue("@Name", team.Name);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

using Approve.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace Approve.API.Repositories
{
    public class REmployee
    {
        public static Task<IEnumerable<MEmployee>> GetAllEmployees()
        {
            List<MEmployee> employees = new List<MEmployee>();

            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.tblEmployee", connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        MEmployee employee = new MEmployee(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), RTeam.GetTeamByID(reader.GetInt32(3)).Result, reader.GetString(4), reader.GetString(5));
                        employees.Add(employee);
                    }

                    reader.Close();
                }

                return Task.FromResult<IEnumerable<MEmployee>>(employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static Task<MEmployee> GetEmployeeByID(int ID)
        {
            MEmployee employee;

            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.tblEmployee where ID = @ID", connection);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                        employee = new MEmployee(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), RTeam.GetTeamByID(reader.GetInt32(3)).Result, reader.GetString(4), reader.GetString(5));
                    else
                        return null;

                    reader.Close();
                }

                return Task.FromResult<MEmployee>(employee);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static Task<MEmployee> GetEmployeeByName(string Name)
        {
            MEmployee employee;

            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.tblEmployee where Name = @Name", connection);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                        employee = new MEmployee(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), RTeam.GetTeamByID(reader.GetInt32(3)).Result, reader.GetString(4), reader.GetString(5));
                    else
                        return null;

                    reader.Close();
                }

                return Task.FromResult<MEmployee>(employee);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static Task<IEnumerable<MEmployee>> GetEmployeesByTeam(string Team)
        {
            List<MEmployee> employees = new List<MEmployee>();

            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.tblEmployee where Team = @Team", connection);
                    cmd.Parameters.AddWithValue("@Team", Team);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        MEmployee employee = new MEmployee(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), RTeam.GetTeamByID(reader.GetInt32(3)).Result, reader.GetString(4), reader.GetString(5)); employees.Add(employee);
                    }

                    reader.Close();
                }

                return Task.FromResult<IEnumerable<MEmployee>>(employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static Task<Int32> GetNextEmployeeID()
        {
            int nextID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT TOP 1 ID FROM tblEmployee ORDER BY ID DESC;", connection);
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

        public static void DeleteEmployee(MEmployee employee)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"DELETE FROM dbo.tblEmployee where ID = @ID", connection);
                    cmd.Parameters.AddWithValue("@ID", employee.ID);
                    
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void CreateEmployee(MEmployee employee)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO dbo.tblEmployee VALUES (@ID, @Name, @Email, @TeamID, @Username, @Password)", connection);
                    cmd.Parameters.AddWithValue("@ID", employee.ID);
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@Email", employee.Email);
                    cmd.Parameters.AddWithValue("@TeamID", employee.Team.ID);
                    cmd.Parameters.AddWithValue("@Username", employee.Username);
                    cmd.Parameters.AddWithValue("@Password", employee.Password);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void UpdateEmployee(MEmployee employee)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE dbo.tblEmployee SET Name = @Name, Email = @Email, TeamID = @TeamID, Username = @Username, Password = @Password WHERE ID = @ID", connection);
                    cmd.Parameters.AddWithValue("@ID", employee.ID);
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@Email", employee.Email);
                    cmd.Parameters.AddWithValue("@Team", employee.Team.ID); 
                    cmd.Parameters.AddWithValue("@Username", employee.Username);
                    cmd.Parameters.AddWithValue("@Password", employee.Password);

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
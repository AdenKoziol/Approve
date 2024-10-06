using Approve.API.Models;
using System.Data.SqlClient;

namespace Approve.API.Repositories
{
    public class RMachine
    {
        public static Task<IEnumerable<MMachine>> GetAllMachines()
        {
            List<MMachine> machines = new List<MMachine>();

            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.tblMachine", connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        MMachine machine = new MMachine(reader.GetInt32(0), reader.GetString(1));
                        machines.Add(machine);
                    }

                    reader.Close();
                }

                return Task.FromResult<IEnumerable<MMachine>>(machines);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static Task<MMachine> GetMachineByID(int ID)
        {
            MMachine machine;

            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.tblMachine where ID = @ID", connection);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                        machine = new MMachine(reader.GetInt32(0), reader.GetString(1));
                    else
                        return null;

                    reader.Close();
                }

                return Task.FromResult<MMachine>(machine);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static Task<MMachine> GetMachineByName(string Name)
        {
            MMachine machine;

            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.tblMachine where Name = @Name", connection);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                        machine = new MMachine(reader.GetInt32(0), reader.GetString(1));
                    else
                        return null;

                    reader.Close();
                }

                return Task.FromResult<MMachine>(machine);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static Task<Int32> GetNextMachineID()
        {
            int nextID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT TOP 1 ID FROM tblMachine ORDER BY ID DESC;", connection);
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

        public static void DeleteMachine(MMachine machine)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"DELETE FROM dbo.tblMachine where ID = @ID", connection);
                    cmd.Parameters.AddWithValue("@ID", machine.ID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void CreateMachine(MMachine machine)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"INSERT INTO dbo.tblMachine VALUES (@ID, @Name)", connection);

                    cmd.Parameters.AddWithValue("@ID", machine.ID);
                    cmd.Parameters.AddWithValue("@Name", machine.Name);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void UpdateMachine(MMachine machine)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"UPDATE dbo.tblMachine SET Name = @Name WHERE ID = @ID", connection);

                    cmd.Parameters.AddWithValue("@ID", machine.ID);
                    cmd.Parameters.AddWithValue("@Name", machine.Name);

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

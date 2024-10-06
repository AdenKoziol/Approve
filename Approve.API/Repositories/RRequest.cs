using Approve.API.Models;
using System.Data.SqlClient;

namespace Approve.API.Repositories
{
    public class RRequest
    {
        public static Task<IEnumerable<MRequest>> GetAllRequests(bool Completed)
        {
            List<MRequest> requests = new List<MRequest>();

            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.tblRequest {(Completed ? "" : "WHERE IsCompleted = 0")}", connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        MRequest request = new MRequest(reader.GetInt32(0), RMachine.GetMachineByID(reader.GetInt32(1)).Result, reader.GetString(2), REmployee.GetEmployeeByID(reader.GetInt32(3)).Result, reader.GetDateTime(4), RTeam.GetTeamByID(reader.GetInt32(5)).Result, RTeam.GetTeamByID(reader.GetInt32(6)).Result, RTeam.GetTeamByID(reader.GetInt32(7)).Result, RTeam.GetTeamByID(reader.GetInt32(8)).Result, RTeam.GetTeamByID(reader.GetInt32(9)).Result, reader.GetInt32(10) == 1 ? true : false);
                        requests.Add(request);
                    }

                    reader.Close();
                }

                return Task.FromResult<IEnumerable<MRequest>>(requests);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static Task<MRequest> GetRequestByID(int ID)
        {
            MRequest request;

            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.tblRequest where ID = @ID", connection);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                        request = new MRequest(reader.GetInt32(0), RMachine.GetMachineByID(reader.GetInt32(1)).Result, reader.GetString(2), REmployee.GetEmployeeByID(reader.GetInt32(3)).Result, reader.GetDateTime(4), RTeam.GetTeamByID(reader.GetInt32(5)).Result, RTeam.GetTeamByID(reader.GetInt32(6)).Result, RTeam.GetTeamByID(reader.GetInt32(7)).Result, RTeam.GetTeamByID(reader.GetInt32(8)).Result, RTeam.GetTeamByID(reader.GetInt32(9)).Result, reader.GetInt32(10) == 1 ? true : false);
                    else
                        return null;

                    reader.Close();
                }

                return Task.FromResult<MRequest>(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static Task<Int32> GetNextRequestID()
        {
            int nextID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT TOP 1 ID FROM tblRequest ORDER BY ID DESC;", connection);
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

        public static void DeleteRequest(MRequest request)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"DELETE FROM dbo.tblRequest where ID = @ID", connection);
                    cmd.Parameters.AddWithValue("@ID", request.ID);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void CreateRequest(MRequest request)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO dbo.tblRequest VALUES (@ID, @MachineID, @Description, @PosterID, @DatePosted, @Team1ID, @Team2ID, @Team3ID, @Team4ID, @Team5ID, @IsCompleted)", connection);
                    cmd.Parameters.AddWithValue("@ID", request.ID);
                    cmd.Parameters.AddWithValue("@MachineID", request.Machine.ID);
                    cmd.Parameters.AddWithValue("@Description", request.Description);
                    cmd.Parameters.AddWithValue("@PosterID", request.Poster.ID);
                    cmd.Parameters.AddWithValue("@DatePosted", request.DatePosted);
                    cmd.Parameters.AddWithValue("@Team1ID", request.Team1.ID);
                    cmd.Parameters.AddWithValue("@Team2ID", request.Team2.ID);
                    cmd.Parameters.AddWithValue("@Team3ID", request.Team3.ID);
                    cmd.Parameters.AddWithValue("@Team4ID", request.Team4.ID);
                    cmd.Parameters.AddWithValue("@Team5ID", request.Team5.ID);
                    cmd.Parameters.AddWithValue("@IsCompleted", request.IsCompleted ? 1 : 0);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void UpdateRequest(MRequest request)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE dbo.tblRequest SET MachineID = @MachineID, Description = @Description, PosterID = @PosterID, DatePosted = @DatePosted, Team1ID = @Team1ID, Team2ID = @Team2ID, Team3ID = @Team3ID, Team4ID = @Team4ID, Team5ID = @Team5ID, IsCompleted = @IsCompleted WHERE ID = @ID", connection);
                    cmd.Parameters.AddWithValue("@ID", request.ID);
                    cmd.Parameters.AddWithValue("@MachineID", request.Machine.ID);
                    cmd.Parameters.AddWithValue("@Description", request.Description);
                    cmd.Parameters.AddWithValue("@PosterID", request.Poster.ID);
                    cmd.Parameters.AddWithValue("@DatePosted", request.DatePosted);
                    cmd.Parameters.AddWithValue("@Team1ID", request.Team1.ID);
                    cmd.Parameters.AddWithValue("@Team2ID", request.Team2.ID);
                    cmd.Parameters.AddWithValue("@Team3ID", request.Team3.ID);
                    cmd.Parameters.AddWithValue("@Team4ID", request.Team4.ID);
                    cmd.Parameters.AddWithValue("@Team5ID", request.Team5.ID);
                    cmd.Parameters.AddWithValue("@IsCompleted", request.IsCompleted ? 1 : 0);

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

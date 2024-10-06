using Approve.API.Models;
using System.Data.SqlClient;

namespace Approve.API.Repositories
{
    public class REmail
    {
        public static Task<MEmail> GetEmailByID(int ID)
        {
            MEmail email;

            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.tblEmail where ID = @ID", connection);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                        email = new MEmail(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4) == 0 ? false : true);
                    else
                        return null;

                    reader.Close();
                }

                return Task.FromResult<MEmail>(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static Task<IEnumerable<MEmail>> GetEmailsByRequestID(int requestID)
        {
            List<MEmail> emails = new List<MEmail>();

            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.tblEmail where RequestID = @RequestID", connection);
                    cmd.Parameters.AddWithValue("@RequestID", requestID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        MEmail email = new MEmail(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4) == 0 ? false : true);
                        emails.Add(email);
                    }

                    reader.Close();
                }

                return Task.FromResult<IEnumerable<MEmail>>(emails);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static Task<Int32> GetNextEmailID()
        {
            int nextID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT TOP 1 ID FROM tblEmail ORDER BY ID DESC;", connection);
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

        public static void CreateEmail(MEmail email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO dbo.tblEmail VALUES (@ID, @RequestID, @TeamID, @EmployeeID, @IsApproved)", connection);
                    cmd.Parameters.AddWithValue("@ID", email.ID);
                    cmd.Parameters.AddWithValue("@RequestID", email.RequestID);
                    cmd.Parameters.AddWithValue("@TeamID", email.TeamID);
                    cmd.Parameters.AddWithValue("@EmployeeID", email.EmployeeID);
                    cmd.Parameters.AddWithValue("@IsApproved", email.IsApproved == false ? 0 : 1);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static bool UpdateEmail(int emailID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    MEmail email = REmail.GetEmailByID(emailID).Result;
                    SqlCommand cmd = new SqlCommand("UPDATE dbo.tblEmail SET RequestID = @RequestID, TeamID = @TeamID, EmployeeID = @EmployeeID, IsApproved = @IsApproved WHERE ID = @ID", connection);
                    cmd.Parameters.AddWithValue("@ID", email.ID);
                    cmd.Parameters.AddWithValue("@RequestID", email.RequestID);
                    cmd.Parameters.AddWithValue("@TeamID", email.TeamID);
                    cmd.Parameters.AddWithValue("@EmployeeID", email.EmployeeID);
                    cmd.Parameters.AddWithValue("@IsApproved",  true);

                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static void DeleteEmails(int requestID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("DATABASE_CONNECTION"))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM dbo.tblEmail WHERE RequestID = @RequestID", connection);
                    cmd.Parameters.AddWithValue("@RequestID", requestID);

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

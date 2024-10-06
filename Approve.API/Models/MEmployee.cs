namespace Approve.API.Models
{
    public class MEmployee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public MTeam Team { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        
        public MEmployee(int ID, string Name, string Email, MTeam Team, string Username, string Password)
        {
            this.ID = ID;
            this.Name = Name;
            this.Email = Email;
            this.Team = Team;
            this.Username = Username;
            this.Password = Password;
        }
    }
}

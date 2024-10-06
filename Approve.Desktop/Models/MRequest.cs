using Approve.Desktop;
using Approve.Desktop.Models;

namespace Approve.Desktop.Models
{
    public class MRequest
    {
        public int ID { get; set; }
        public MMachine Machine { get; set; }
        public string Description { get; set; }
        public MEmployee Poster { get; set; }
        public DateTime DatePosted { get; set; }
        public MTeam Team1 { get; set; }
        public MTeam Team2 { get; set;}
        public MTeam Team3 { get; set; }
        public MTeam Team4 { get; set; }
        public MTeam Team5 { get; set;}
        public bool IsCompleted { get; set; }

        public MRequest(int ID, MMachine Machine, string Description, MEmployee Poster, DateTime DatePosted, MTeam Team1, MTeam Team2, MTeam Team3, MTeam Team4, MTeam Team5, bool IsCompleted)
        {
            this.ID = ID;
            this.Machine = Machine;
            this.Description = Description;
            this.Poster = Poster;
            this.DatePosted = DatePosted;
            this.Team1 = Team1;
            this.Team2 = Team2;
            this.Team3 = Team3;
            this.Team4 = Team4;
            this.Team5 = Team5;
            this.IsCompleted = IsCompleted;
        }
    }
}

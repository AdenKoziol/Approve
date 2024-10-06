namespace Approve.API.Models
{
    public class MTeam
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public MTeam(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }
}

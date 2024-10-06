namespace Approve.Desktop.Models
{
    public class MMachine
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public MMachine(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }
}

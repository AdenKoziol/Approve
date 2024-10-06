using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Approve.Desktop.Models
{
    internal class MEmail
    {
        public int ID { get; set; }
        public int RequestID { get; set; }
        public int TeamID { get; set; }
        public int EmployeeID { get; set; }
        public bool IsApproved { get; set; }

        public MEmail(int ID, int RequestID, int TeamID, int EmployeeID, bool IsApproved)
        {
            this.ID = ID;
            this.RequestID = RequestID;
            this.TeamID = TeamID;
            this.EmployeeID = EmployeeID;
            this.IsApproved = IsApproved;
        }
    }
}

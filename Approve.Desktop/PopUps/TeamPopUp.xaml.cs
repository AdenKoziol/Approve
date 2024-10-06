using Approve.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Approve.Desktop.PopUps
{
    public partial class TeamPopUp : Window
    {
        bool UpdateFlag;
        int TeamID;
        public TeamPopUp()
        {
            InitializeComponent();
            UpdateFlag = false;
        }

        public TeamPopUp(MTeam team)
        {
            InitializeComponent();
            UpdateFlag = true;
            btnDelete.Visibility = Visibility.Visible;
            TeamID = team.ID;
            txtName.Text = team.Name;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                txtNameError.Visibility = Visibility.Visible;
                return;
            }

            if (UpdateFlag)
            {
                MTeam team = new MTeam(TeamID, txtName.Text);
                ApiHelper.UpdateModel(team);
                Close();
            }
            else
            {
                MTeam team = new MTeam(ApiHelper.GetModel<int>("Teams/GetNextTeamID"), txtName.Text);
                ApiHelper.PostModel(team);
                Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            MTeam team = new MTeam(TeamID, txtName.Text);
            ApiHelper.DeleteModel(team);
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            txtNameError.Visibility = txtName.Text.Trim() == "" ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}

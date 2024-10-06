using Approve.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class MachinePopUp : Window
    {
        bool UpdateFlag;
        int MachineID;
        public MachinePopUp()
        {
            InitializeComponent();
            UpdateFlag = false;
        }

        public MachinePopUp(MMachine machine)
        {
            InitializeComponent();
            UpdateFlag = true;
            MachineID = machine.ID;
            btnDelete.Visibility = Visibility.Visible;
            txtName.Text = machine.Name;
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
                MMachine machine = new MMachine(MachineID, txtName.Text);
                ApiHelper.UpdateModel(machine);
                Close();
            }
            else
            {
                MMachine machine = new MMachine(ApiHelper.GetModel<int>("Machines/GetNextMachineID"), txtName.Text);
                ApiHelper.PostModel(machine);
                Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            MMachine machine = new MMachine(MachineID, txtName.Text);
            ApiHelper.DeleteModel(machine);
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

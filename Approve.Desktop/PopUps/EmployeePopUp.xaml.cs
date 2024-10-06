using Approve.Desktop.Models;
using Org.BouncyCastle.Crypto.Generators;
using System.Collections.ObjectModel;
using System.Windows;

namespace Approve.Desktop
{
    public partial class EmployeePopUp : Window
    {
        public ObservableCollection<MTeam> TeamsList { get; set; }
        public MEmployee employee;
        bool UpdateFlag;

        public EmployeePopUp()
        {
            InitializeComponent();
            TeamsList = new ObservableCollection<MTeam>(ApiHelper.GetModelList<MTeam>("Teams"));
            TeamsList.RemoveAt(0);
            UpdateFlag = false;
            DataContext = this;
        }

        public EmployeePopUp(MEmployee employee)
        {
            InitializeComponent();
            TeamsList = new ObservableCollection<MTeam>(ApiHelper.GetModelList<MTeam>("Teams"));
            TeamsList.RemoveAt(0);
            UpdateFlag = true;
            DataContext = this;
            btnDelete.Visibility = Visibility.Visible;
            this.employee = employee;
            txtName.Text = employee.Name;
            txtEmail.Text = employee.Email;
            ddlTeam.SelectedItem = TeamsList.FirstOrDefault(t => t.ID == employee.Team.ID);
            spUsername.Visibility = Visibility.Collapsed;
            spPassword.Visibility = Visibility.Collapsed;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
                txtNameError.Visibility = Visibility.Visible;
            if (txtEmail.Text.Trim() == "")
                txtEmailError.Visibility = Visibility.Visible;
            if (ddlTeam.SelectedItem == null)
                txtTeamError.Visibility = Visibility.Visible;
            if (txtUsername.Text.Trim() == "" && employee == null)
                txtUsernameError.Visibility = Visibility.Visible;
            if (txtPassword.Text.Trim() == "" && employee == null)
                txtPasswordError.Visibility = Visibility.Visible;
            if (txtNameError.Visibility == Visibility.Visible || txtEmailError.Visibility == Visibility.Visible || txtTeamError.Visibility == Visibility.Visible || txtUsernameError.Visibility == Visibility.Visible || txtPasswordError.Visibility == Visibility.Visible)
                return;

            if (UpdateFlag)
            {
                MEmployee updateEmployee = new MEmployee(employee.ID, txtName.Text, txtEmail.Text, (MTeam)ddlTeam.SelectedValue, employee.Username, employee.Password);
                ApiHelper.UpdateModel(updateEmployee);
                Close();
            }
            else
            {
                if (employee == null)
                {
                    List<MEmployee> employeesList = (List<MEmployee>)ApiHelper.GetModelList<MEmployee>("Employees");
                    foreach (MEmployee otherEmployee in employeesList)
                    {
                        if (otherEmployee.Username == txtUsername.Text)
                        {
                            txtUsernameTakenError.Visibility = Visibility.Visible;
                            return;
                        }
                    }
                }

                MEmployee newEmployee = new MEmployee(ApiHelper.GetModel<int>("Employees/NextEmployeeID"), txtName.Text, txtEmail.Text, (MTeam)ddlTeam.SelectedItem, txtUsername.Text, txtPassword.Text);
                ApiHelper.PostModel(newEmployee);
                Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            MEmployee deleteEmployee = new MEmployee(employee.ID, txtName.Text, txtEmail.Text, (MTeam)ddlTeam.SelectedItem, employee.Username, employee.Password);
            ApiHelper.DeleteModel(deleteEmployee);
            Close();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            txtNameError.Visibility = txtName.Text.Trim() == "" ? Visibility.Visible : Visibility.Collapsed;
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            txtEmailError.Visibility = txtEmail.Text.Trim() == "" ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ddlTeam_SelectionChanged(object sender, EventArgs e)
        {
            txtTeamError.Visibility = ddlTeam.SelectedItem == null ? Visibility.Visible : Visibility.Collapsed;
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            txtUsernameError.Visibility = txtUsername.Text.Trim() == "" ? Visibility.Visible : Visibility.Collapsed;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtPasswordError.Visibility = txtPassword.Text.Trim() == "" ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            App.ComboBox_Loaded(sender, e);
        }
    }
}

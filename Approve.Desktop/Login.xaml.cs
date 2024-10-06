using Approve.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Approve.Desktop
{
    public partial class Login : Window
    {
        public ObservableCollection<MEmployee> EmployeesList { get; set; }
        public Login()
        {
            InitializeComponent();
            EmployeesList = new ObservableCollection<MEmployee>(ApiHelper.GetModelList<MEmployee>("Employees"));
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            foreach (MEmployee employee in EmployeesList)
            {
                if (txtUsername.Text == employee.Username)
                {
                    if (txtPassword.Password == employee.Password)
                    {
                        MEmployee loginEmployee = EmployeesList.Where(e => e.Username == txtUsername.Text && e.Password == txtPassword.Password).FirstOrDefault();
                        MainWindow mainWindow = new MainWindow(loginEmployee);
                        mainWindow.Show();
                        this.Close();
                    }
                }
            }
            lblError.Visibility = Visibility.Visible;
        }
    }
}

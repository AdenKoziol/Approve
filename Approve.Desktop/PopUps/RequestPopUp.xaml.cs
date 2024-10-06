using Approve.Desktop.Models;
using Newtonsoft.Json;
using NPOI.HSSF.Record;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;

namespace Approve.Desktop.PopUps
{
    public partial class RequestPopUp : Window
    {
        public MEmployee POSTER;
        public ObservableCollection<MMachine> MachinesList { get; set; }
        public ObservableCollection<MTeam> TeamsList { get; set; }
        public ObservableCollection<MTeam> AvailableTeams {  get; set; }
        bool UpdateFlag;
        MRequest request;
        public RequestPopUp(MEmployee user)
        {
            InitializeComponent();
            POSTER = user;
            MachinesList = new ObservableCollection<MMachine>(ApiHelper.GetModelList<MMachine>("Machines"));
            TeamsList = new ObservableCollection<MTeam>(ApiHelper.GetModelList<MTeam>("Teams").Where(team => team.ID != -1));
            InitItemsSource();
            DataContext = this;
            UpdateFlag = false;
            txtDatePosted.Text = DateTime.Now.ToString();
        }

        public RequestPopUp(MEmployee user, MRequest request)
        {
            InitializeComponent();
            POSTER = user;
            MachinesList = new ObservableCollection<MMachine>(ApiHelper.GetModelList<MMachine>("Machines"));
            TeamsList = new ObservableCollection<MTeam>(ApiHelper.GetModelList<MTeam>("Teams").Where(team => team.ID != -1));
            this.request = request;
            InitItemsSource();
            DataContext = this;
            UpdateFlag = true;
            if(!this.request.IsCompleted)
                btnDelete.Visibility = Visibility.Visible;
            ddlMachine.SelectedItem = MachinesList.FirstOrDefault(m => m.ID == request.Machine.ID);
            txtDescription.Text = request.Description;
            txtDatePosted.Text = request.DatePosted.ToString();

            //Disable Fields if request is completed
            if (request.IsCompleted)
            {
                ddlMachine.IsEnabled = false;
                txtDescription.IsEnabled = false;
                ddlTeam1.IsEnabled = false;
                ddlTeam2.IsEnabled = false;
                ddlTeam3.IsEnabled = false;
                ddlTeam4.IsEnabled = false;
                ddlTeam5.IsEnabled = false;
            }
        }

        private void InitItemsSource()
        {
            ddlTeam1.ItemsSource = TeamsList;
            ddlTeam2.ItemsSource = TeamsList;
            ddlTeam3.ItemsSource = TeamsList;
            ddlTeam4.ItemsSource = TeamsList;
            ddlTeam5.ItemsSource = TeamsList;

            if (request != null)
            {
                ddlTeam1.SelectedItem = TeamsList.FirstOrDefault(t => t.ID == request.Team1.ID);
                ddlTeam2.SelectedItem = TeamsList.FirstOrDefault(t => t.ID == request.Team2.ID);
                ddlTeam3.SelectedItem = TeamsList.FirstOrDefault(t => t.ID == request.Team3.ID);
                ddlTeam4.SelectedItem = TeamsList.FirstOrDefault(t => t.ID == request.Team4.ID);
                ddlTeam5.SelectedItem = TeamsList.FirstOrDefault(t => t.ID == request.Team5.ID);
            }
        }

        private void UpdateAvailableTeams()
        {
            AvailableTeams = new ObservableCollection<MTeam>(TeamsList);

            var selectedTeams = new List<MTeam>
            {
                (MTeam)ddlTeam1.SelectedItem,
                (MTeam)ddlTeam2.SelectedItem,
                (MTeam)ddlTeam3.SelectedItem,
                (MTeam)ddlTeam4.SelectedItem,
                (MTeam)ddlTeam5.SelectedItem
            }.Where(team => team != null).ToList();

            foreach (MTeam team in selectedTeams)
            {
                if(team.ID != -1) // Tests if team selected is None. Don't want to remove that
                    AvailableTeams.Remove(team);
            }

            SetItemsSource(ddlTeam1);
            SetItemsSource(ddlTeam2);
            SetItemsSource(ddlTeam3);
            SetItemsSource(ddlTeam4);
            SetItemsSource(ddlTeam5);
        }

        private void SetItemsSource(ComboBox comboBox)
        {
            if(comboBox.SelectedItem != null)
            {
                MTeam team = (MTeam)comboBox.SelectedItem;
                ObservableCollection<MTeam> updateAvailableTeams = new ObservableCollection<MTeam>(AvailableTeams);
                if(team.ID != -1)
                    updateAvailableTeams.Add(team);
                comboBox.ItemsSource = updateAvailableTeams;
            }
            else
            {
                comboBox.ItemsSource = AvailableTeams;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            txtMachineError.Visibility = ddlMachine.SelectedItem == null ? Visibility.Visible : Visibility.Collapsed;
            txtDescriptionError.Visibility = txtDescription.Text.Trim() == "" ? Visibility.Visible : Visibility.Collapsed;

            // Check if no teams selected
            if (ddlTeam1.SelectedItem == null &&
                ddlTeam2.SelectedItem == null &&
                ddlTeam3.SelectedItem == null &&
                ddlTeam4.SelectedItem == null &&
                ddlTeam5.SelectedItem == null)
            {
                txtTeamError.Visibility = Visibility.Visible;
            }

            if (txtMachineError.Visibility == Visibility.Visible ||
                txtDescriptionError.Visibility == Visibility.Visible ||
                txtTeamError.Visibility == Visibility.Visible)
            {
                return;
            }

            // This will set the team to none if the dropdown was empty
            MTeam none = ApiHelper.GetModel<MTeam>("Teams/ID/-1");
            MTeam Team1 = none;
            MTeam Team2 = none;
            MTeam Team3 = none;
            MTeam Team4 = none;
            MTeam Team5 = none;
            Team1 = ddlTeam1.SelectedItem != null ? (MTeam)ddlTeam1.SelectedItem : none;
            Team2 = ddlTeam2.SelectedItem != null ? (MTeam)ddlTeam2.SelectedItem : none;
            Team3 = ddlTeam3.SelectedItem != null ? (MTeam)ddlTeam3.SelectedItem : none;
            Team4 = ddlTeam4.SelectedItem != null ? (MTeam)ddlTeam4.SelectedItem : none;
            Team5 = ddlTeam5.SelectedItem != null ? (MTeam)ddlTeam5.SelectedItem : none;

            if (UpdateFlag)
            {
                MRequest updateRequest = new MRequest(request.ID, (MMachine)ddlMachine.SelectedValue, txtDescription.Text, POSTER, DateTime.Parse(txtDatePosted.Text), Team1, Team2, Team3, Team4, Team5, request.IsCompleted);
                ApiHelper.UpdateModel(updateRequest);
                Close();
            }
            else
            {
                MRequest saveRequest = new MRequest(ApiHelper.GetModel<int>("Requests/GetNextRequestID"), (MMachine)ddlMachine.SelectedValue, txtDescription.Text, POSTER, DateTime.Now, Team1, Team2, Team3, Team4, Team5, false);
                SendEmails(saveRequest);
                ApiHelper.PostModel(saveRequest);
                Close();
            }
        }

        private async Task SendEmails(MRequest saveRequest)
        {
            // Get all the employees from all teams
            List<MEmployee> employees = new List<MEmployee>();
            if (ddlTeam1.SelectedItem != null)
                employees.AddRange(ApiHelper.GetModelList<MEmployee>($"Employees/Team/{((MTeam)ddlTeam1.SelectedItem).Name}"));
            if (ddlTeam2.SelectedItem != null)
                employees.AddRange(ApiHelper.GetModelList<MEmployee>($"Employees/Team/{((MTeam)ddlTeam2.SelectedItem).Name}"));
            if (ddlTeam3.SelectedItem != null)
                employees.AddRange(ApiHelper.GetModelList<MEmployee>($"Employees/Team/{((MTeam)ddlTeam3.SelectedItem).Name}"));
            if (ddlTeam4.SelectedItem != null)
                employees.AddRange(ApiHelper.GetModelList<MEmployee>($"Employees/Team/{((MTeam)ddlTeam4.SelectedItem).Name}"));
            if (ddlTeam5.SelectedItem != null)
                employees.AddRange(ApiHelper.GetModelList<MEmployee>($"Employees/Team/{((MTeam)ddlTeam5.SelectedItem).Name}"));

            string emailSubject = "Request #: " + saveRequest.ID;

            foreach (MEmployee employee in employees)
            {
                MEmail email = new MEmail(ApiHelper.GetModel<int>("Emails/GetNextEmailID"), saveRequest.ID, ApiHelper.GetModel<MTeam>($"Teams/Name/{employee.Team}").ID, employee.ID, false);

                // Build Email Body
                string emailBody = "";
                emailBody += $"<h3>Request #: {saveRequest.ID}</h3>";
                emailBody += $@"
                <a href='https://localhost:44387/Emails/EmailClick?emailID={email.ID}' 
                   style='display: inline-block; padding: 10px 20px; font-size: 16px; font-weight: bold; color: #fff; 
                          background-color: #4CAF50; text-align: center; text-decoration: none; border-radius: 5px;'>
                   View Request
                </a>";

                EmailHelper.SendEmail(employee.Email, employee.Name, emailSubject, emailBody);
                await ApiHelper.PostModel(email);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            MRequest deleteRequest = new MRequest(request.ID, (MMachine)ddlMachine.SelectedValue, txtDescription.Text, POSTER, DateTime.Parse(txtDatePosted.Text), (MTeam)ddlTeam1.SelectedItem, (MTeam)ddlTeam2.SelectedItem, (MTeam)ddlTeam3.SelectedItem, (MTeam)ddlTeam4.SelectedItem, (MTeam)ddlTeam5.SelectedItem, request.IsCompleted);
            ApiHelper.DeleteModel(deleteRequest);
            Close();
        }

        private void ddlMachine_SelectionChanged(object sender, EventArgs e)
        {
            txtMachineError.Visibility = ddlMachine.SelectedItem == null ? Visibility.Visible : Visibility.Collapsed;
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            txtDescriptionError.Visibility = txtDescription.Text.Trim() == "" ? Visibility.Visible : Visibility.Collapsed;
        }

        private void teamSelectionChanged(object sender, EventArgs e)
        {
            UpdateAvailableTeams();
        }

        private void clearTeam1(object sender, EventArgs e)
        {
            ddlTeam1.SelectedItem = null;
        }

        private void clearTeam2(object sender, EventArgs e)
        {
            ddlTeam2.SelectedItem = null;
        }

        private void clearTeam3(object sender, EventArgs e)
        {
            ddlTeam3.SelectedItem = null;
        }

        private void clearTeam4(object sender, EventArgs e)
        {
            ddlTeam4.SelectedItem = null;
        }

        private void clearTeam5(object sender, EventArgs e)
        {
            ddlTeam5.SelectedItem = null;
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            App.ComboBox_Loaded(sender, e);
        }
    }
}

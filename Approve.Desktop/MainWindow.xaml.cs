using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Approve.Desktop.Models;
using Approve.Desktop.PopUps;
using Newtonsoft.Json;

namespace Approve.Desktop
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<MRequest> RequestList { get; set; }
        public ObservableCollection<MEmployee> EmployeesList { get; set; }
        public ObservableCollection<MMachine> MachinesList { get; set; }
        public ObservableCollection<MTeam> TeamsList { get; set; }
        public MEmployee User { get; set; }

        public MainWindow(MEmployee User)
        {
            InitializeComponent();
            this.User = User;
            LoadGrids();
        }

        private void LoadGrids()
        {
            if ((bool)chkCompleted.IsChecked)
                RequestList = new ObservableCollection<MRequest>(ApiHelper.GetModelList<MRequest>("Requests/GetAllRequests/true").OrderBy(request => request.ID));
            else
                RequestList = new ObservableCollection<MRequest>(ApiHelper.GetModelList<MRequest>("Requests/GetAllRequests/false").OrderBy(request => request.ID));
            EmployeesList = new ObservableCollection<MEmployee>(ApiHelper.GetModelList<MEmployee>("Employees").OrderBy(employee => employee.ID));
            MachinesList = new ObservableCollection<MMachine>(ApiHelper.GetModelList<MMachine>("Machines").OrderBy(machine => machine.ID));
            TeamsList = new ObservableCollection<MTeam>(ApiHelper.GetModelList<MTeam>("Teams").OrderBy(team => team.ID));
            TeamsList.RemoveAt(0);

            DataContext = this;

            requestGrid.ItemsSource = RequestList;
            employeeGrid.ItemsSource = EmployeesList;
            machineGrid.ItemsSource = MachinesList;
            teamGrid.ItemsSource = TeamsList;

            CheckApproved();
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            pnlRequests.Visibility = Visibility.Visible;
            pnlEmployees.Visibility = Visibility.Collapsed;
            pnlMachines.Visibility = Visibility.Collapsed;
            pnlTeams.Visibility = Visibility.Collapsed;
            CheckApproved();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            pnlRequests.Visibility = Visibility.Collapsed;
            pnlEmployees.Visibility = Visibility.Visible;
            pnlMachines.Visibility = Visibility.Collapsed;
            pnlTeams.Visibility = Visibility.Collapsed;
        }

        private void btnMachine_Click(object sender, EventArgs e)
        {
            pnlRequests.Visibility = Visibility.Collapsed;
            pnlEmployees.Visibility = Visibility.Collapsed;
            pnlMachines.Visibility = Visibility.Visible;
            pnlTeams.Visibility = Visibility.Collapsed;
        }

        private void btnTeam_Click(object sender, EventArgs e)
        {
            pnlRequests.Visibility = Visibility.Collapsed;
            pnlEmployees.Visibility = Visibility.Collapsed;
            pnlMachines.Visibility = Visibility.Collapsed;
            pnlTeams.Visibility = Visibility.Visible;
        }

        private void btnNewRequest_Click(object sender, EventArgs e)
        {
            RequestPopUp newRequest = new RequestPopUp(User);
            newRequest.ShowDialog();
            LoadGrids();
        }

        private void btnNewEmployee_Click(object sender, EventArgs e)
        {
            EmployeePopUp newEmployee = new EmployeePopUp();
            newEmployee.ShowDialog();
            LoadGrids();
        }

        private void btnNewMachine_Click(object sender, EventArgs e)
        {
            MachinePopUp newMachine = new MachinePopUp();
            newMachine.ShowDialog();
            LoadGrids();
        }

        private void btnNewTeam_Click(object sender, EventArgs e)
        {
            TeamPopUp newTeam = new TeamPopUp();
            newTeam.ShowDialog();
            LoadGrids();
        }

        private void Grid_RowClick(object sender, MouseButtonEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid != null)
            {
                var point = e.GetPosition(dataGrid);
                var hit = dataGrid.InputHitTest(point) as DependencyObject;

                while (hit != null && !(hit is DataGridRow))
                {
                    hit = VisualTreeHelper.GetParent(hit);
                }

                if (hit is DataGridRow row)
                {
                    var item = row.DataContext;

                    if (item.GetType() == typeof(MEmployee))
                    {
                        EmployeePopUp employee = new EmployeePopUp((MEmployee)item);
                        employee.btnSave.Content = "Update";
                        employee.ShowDialog();
                    }
                    else if (item.GetType() == typeof(MMachine))
                    {
                        MachinePopUp machine = new MachinePopUp((MMachine)item);
                        machine.btnSave.Content = "Update";
                        machine.ShowDialog();
                    }
                    else if (item.GetType() == typeof(MTeam))
                    {
                        TeamPopUp team = new TeamPopUp((MTeam)item);
                        team.btnSave.Content = "Update";
                        team.ShowDialog();
                    }
                    else if (item.GetType() == typeof(MRequest))
                    {
                        RequestPopUp request = new RequestPopUp(User, (MRequest)item);
                        request.btnSave.Content = "Update";
                        request.ShowDialog();
                    }

                    LoadGrids();
                }
            }
        }

        private void chkCompleted_CheckChanged(object sender, EventArgs e)
        {
            LoadGrids();
        }

        private void CheckApproved()
        {
            foreach (MRequest request in RequestList)
            {
                List<MEmail> requestEmails = (List<MEmail>)ApiHelper.GetModelList<MEmail>($"Emails/RequestID/{request.ID}");
                List<MTeam> requestTeams = new List<MTeam>() { request.Team1, request.Team2, request.Team3, request.Team4, request.Team5 };
                bool isCompleted = true;

                // Find the corresponding row in the DataGrid
                var rowIndex = RequestList.IndexOf(request);

                // Force the DataGrid to generate the row by scrolling it into view
                requestGrid.UpdateLayout(); // Ensure the layout is up-to-date
                requestGrid.ScrollIntoView(requestGrid.Items[rowIndex]);

                var dataGridRow = (DataGridRow)requestGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);

                // If still null, skip processing this row
                if (dataGridRow == null)
                    continue;

                foreach (MTeam team in requestTeams)
                {
                    if (team.ID == -1) //Don't want colors showing for None
                        continue;

                    string color;
                    List<MEmployee> teamEmployees = (List<MEmployee>)ApiHelper.GetModelList<MEmployee>($"Employees/Team/{team.Name}");

                    // Determine color based on approval status
                    if (teamEmployees.Count == 0 || request.IsCompleted)
                    {
                        color = "Green";
                    }
                    else if (requestEmails.Where(e => e.TeamID == team.ID && e.IsApproved).Count() == 0)
                    {
                        color = "Red";
                        isCompleted = false;
                    }
                    else if (requestEmails.Where(e => e.TeamID == team.ID && e.IsApproved).Count() < teamEmployees.Count)
                    {
                        color = "Yellow";
                        isCompleted = false;
                    }
                    else
                    {
                        color = "Green";
                    }

                    // Find the cell corresponding to this team
                    int columnIndex = requestTeams.IndexOf(team) + 5;
                    var dataGridCell = GetCell(dataGridRow, columnIndex);

                    // Set the background color of the cell
                    if (dataGridCell != null)
                    {
                        dataGridCell.BorderThickness = new Thickness(1);
                        dataGridCell.BorderBrush = new SolidColorBrush(Colors.Black);

                        if (color == "Green")
                        {
                            dataGridCell.Background = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                        }
                        else if (color == "Yellow")
                        {
                            dataGridCell.Background = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                            dataGridCell.Foreground = new SolidColorBrush(Colors.Black);
                        }
                        else if (color == "Red")
                        {
                            dataGridCell.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                        }
                    }
                }

                // Update if request is now completed
                if (!request.IsCompleted && isCompleted)
                {
                    request.IsCompleted = true;
                    ApiHelper.UpdateModel(request);

                    // Send confirmation email to poster
                    string emailBody = "";
                    emailBody += "<h2>Your request has been approved.</h2>";
                    emailBody += $"<h3>Request #: {request.ID}</h3>";
                    emailBody += $"<p>Machine: {request.Machine.Name}</p>";
                    emailBody += $"<p>Description: {request.Description}</p>";
                    emailBody += $"<p>Date Posted: {request.DatePosted}</p>";
                    EmailHelper.SendEmail(request.Poster.Email, request.Poster.Name, $"Request #{request.ID} has been approved", emailBody);
                }
            }
        }

        // Helper method to get the DataGridCell
        private DataGridCell GetCell(DataGridRow row, int columnIndex)
        {
            DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);
            DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
            if (cell == null)
            {
                requestGrid.ScrollIntoView(row, requestGrid.Columns[columnIndex]);
                cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
            }
            return cell;
        }

        // Helper method to get the visual child
        private T GetVisualChild<T>(DependencyObject parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
    }
}
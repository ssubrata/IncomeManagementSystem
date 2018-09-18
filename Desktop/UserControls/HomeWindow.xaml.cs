
using Desktop.Helper;
using Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Desktop.UserControls
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            Map.Initialize();
            LoginUser.Text = $"{ApplicationState.GetValue<User>("UserInfo").FullName.ToUpper()},{ApplicationState.GetValue<User>("UserInfo").Role.Name}";
            btnLogOut.Click += BtnLogOut_Click;
            DataContext = new LoginViewModel { IsAdmin = ApplicationState.GetValue<bool>("IsAdmin") };
            //Back Up
         
        }

      


        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            ApplicationState.RemoveKey("UserInfo");
            ApplicationState.RemoveKey("IsAdmin");

            MainWindow main = new MainWindow();
            main.Show();
            this.Close();

        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridName.Children.Clear();
            var selectedControl = ((System.Windows.Controls.ListViewItem)((System.Windows.Controls.ListView)sender).SelectedItem).Name;
       

            switch (selectedControl)
            {

                //case "Home":
                //    this.Show();
                //    break;
                case "Income":
                    TitleName.Text = "আয়ের সকল তথ্য";
                    GridName.Children.Add(new IncomeControl());
                    break;
                case "Spend":
                    TitleName.Text = "ব্যায়ের সকল তথ্য";

                    GridName.Children.Add(new SpendControl());
                    break;
                case "UserIncomeSpendSource":
                    TitleName.Text = "আয় ও ব্যায় এর খাত এবং ইউসার সকল তথ্য";

                    if (ApplicationState.GetValue<bool>("IsAdmin"))
                       GridName.Children.Add(new ManageControl());
                    else
                    {
                        status.Text = "আপনার অনুমতি নেই";
                        Dialog.IsOpen = true;
                    }
                    break;
                case "Report":
                    TitleName.Text = "রিপোর্ট এর সকল তথ্য";

                    if (ApplicationState.GetValue<bool>("IsAdmin"))
                        GridName.Children.Add(new ReportControl());
                     else
                    {
                        status.Text = "আপনার অনুমতি নেই";
                        Dialog.IsOpen = true;
                    }
                    break;
                default:
                    break;
            }
            MenuToggleButton.IsChecked = false;
        }


    }
}

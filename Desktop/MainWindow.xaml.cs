
using DAL.Repository.Implement;
using Desktop.Helper;
using Desktop.UserControls;
using System;
using System.Windows;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private  UnitOfWork _unitOfWork;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                log4net.Config.XmlConfigurator.Configure();
                _unitOfWork = DbSettingHelper.UnitOfWork;
                btnLogin.Click += Login;
                cbmServer.Items.Add(".");
                cbmServer.Items.Add("(local)");
                cbmServer.Items.Add(@".\SQLEXPRESS");
                cbmServer.Items.Add(string.Format(@"{0}\SQLEXPRESS", Environment.MachineName));
                cbmServer.Items.Add(string.Format(Environment.MachineName));

            }
            catch (System.Exception ex)
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: { ex.Message}";
                Dialog.IsOpen = true;
            }
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            try
            {
                var findUser = _unitOfWork.User.ExistUser(txtUserName.Text.ToString().Trim(), txtPassword.Password.Trim().ToString());
                if (findUser == null)
                {
                    status.Text = $"ইউসার নাম এবং পাসওয়ার্ড ভুল প্রদান করা হয়েছে।";
                    Dialog.IsOpen = true;
                    return;
                }
                    if (findUser.Role.Name == "Admin")
                        ApplicationState.SetValue("IsAdmin", true);
                    else
                        ApplicationState.SetValue("IsAdmin", false);

                    ApplicationState.SetValue("UserInfo", findUser);
                  
                    HomeWindow homeWindow = new HomeWindow();
                    homeWindow.Show();
                    this.Close();
                    return;

            }
            catch (System.Exception ex)
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: { ex.Message}";
                Dialog.IsOpen = true;
            }
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F1)
                DbSettingDialog.IsOpen = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.DataSource = cbmServer.Text;
                Properties.Settings.Default.Database = txtDatabase.Text;
                Properties.Settings.Default.Password = txtpassWord.Text;
                Properties.Settings.Default.UserId = txtuerName.Text;
                Properties.Settings.Default.Save();

                status.Text = $"ডাটাবেস সংরক্ষণ করা হয়েছে";
                Dialog.IsOpen = true;
                DbSettingDialog.IsOpen = false;

                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            catch(Exception ex)
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: { ex.Message}";
                Dialog.IsOpen = true;
            }

        }
      
    }
}

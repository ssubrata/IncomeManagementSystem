
using DAL.Repository.Implement;
using Desktop.DataModel;
using Desktop.Helper;
using Desktop.Reports;
using Desktop.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.UserControls
{
    /// <summary>
    /// Interaction logic for ReportControl.xaml
    /// </summary>
    public partial class ReportControl : UserControl
    {
        public ObservableCollection<VmIncomeSource> vmincomeSouceList = new ObservableCollection<VmIncomeSource>();
        public ObservableCollection<VmSpendSource> vmspendsourceList = new ObservableCollection<VmSpendSource>();
        private readonly UnitOfWork _unitOfWork;
        public ReportControl()
        {
            try
            {
        
                InitializeComponent();
              
                DataContext = new ReportViewModel();
                _unitOfWork = DbSettingHelper.UnitOfWork;
                cmbreportType.SelectionChanged += CmbreportType_SelectionChanged;
                btnReport.Click += BtnReport_Click;
                backup.Click += Backup_Click;
                browse.Click += Browse_Click;
                backup.Visibility = Visibility.Collapsed;
               
            }
            catch (Exception ex)
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: {ex.Message}";
                Dialog.IsOpen = true;
                var l = new Log { Date = DateTime.Now, Exception = ex.Message, Level = "Test", Logger = "Test", Thread = "Test", Message = ex.Message };
                _unitOfWork.Log.Add(l);
                _unitOfWork.Complete();
            }
           
        }

        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                SqlParameter from, to, id; DataTable dt = null;
                from = new SqlParameter { ParameterName = "@fromDate", Value = txtFromDate.SelectedDate };
                to = new SqlParameter { ParameterName = "@toDate", Value = txtToDate.SelectedDate };
                id = new SqlParameter { ParameterName = "@sourceId", Value = cmbSourceName.SelectedValue };
                String Path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                if (txtFromDate.SelectedDate == null && txtToDate.SelectedDate == null)
                {
                    from = new SqlParameter { ParameterName = "@fromDate", Value = DBNull.Value };
                    to = new SqlParameter { ParameterName = "@toDate", Value = DBNull.Value };

                }

                //income
                if (cmbreportType.SelectedIndex == 0 && GroupBy.IsChecked == false)
                {
                    var list = new TruckDbContext(DbSettingHelper.BuildConnectionString()).Database.SqlQuery<SpGetIncomeModel>("exec dbo.SpGetIncome @sourceId,@fromDate,@toDate", id, from, to).ToList<SpGetIncomeModel>();
                    dt = ApplicationState.ToDataTable(list);
                    IncomeReport incomeReport = new IncomeReport();
                    // Path = System.IO.Path.Combine(Path, @"Reports\IncomeReport.rpt");
                    var fi = System.AppDomain.CurrentDomain.BaseDirectory;
                    incomeReport.Load("~/Reports/IncomeReport.rpt");
                    incomeReport.SetDataSource(dt);
                    incomeReport.Refresh();
                    reportViewer.ViewerCore.ReportSource = incomeReport;
                }
                //income Group By

                if (cmbreportType.SelectedIndex == 0 && GroupBy.IsChecked == true)
                {
                    var list = new TruckDbContext(DbSettingHelper.BuildConnectionString()).Database.SqlQuery<SpGetIncomeModel>("exec dbo.SpGetIncome @sourceId,@fromDate,@toDate", id, from, to).ToList<SpGetIncomeModel>();
                    dt = ApplicationState.ToDataTable(list);
                    IncomeReportByGroup incomeReport = new IncomeReportByGroup();
                    Path = System.IO.Path.Combine(Path, @"Reports\IncomeReportByGroup.rpt");
                    incomeReport.Load(Path);
                    incomeReport.SetDataSource(dt);
                    incomeReport.Refresh();
                    reportViewer.ViewerCore.ReportSource = incomeReport;
                }

                //spend Group By

                if (cmbreportType.SelectedIndex == 1 && GroupBy.IsChecked == true)
                {
                    var list = new TruckDbContext(DbSettingHelper.BuildConnectionString()).Database.SqlQuery<SpGetSpendModel>("exec SpGetSpend @sourceId,@fromDate,@toDate", id, from, to).ToList<SpGetSpendModel>();
                    dt = ApplicationState.ToDataTable(list);
                    SpendReportByGroup spendReport = new SpendReportByGroup();
                    Path = System.IO.Path.Combine(Path, @"Reports\SpendReportByGroup.rpt");
                    spendReport.Load(Path);

                    spendReport.SetDataSource(dt);
                    spendReport.Refresh();

                    reportViewer.ViewerCore.ReportSource = spendReport;
                }
                //spend 
                if (cmbreportType.SelectedIndex == 1 && GroupBy.IsChecked == false)
                {
                    var list = new TruckDbContext(DbSettingHelper.BuildConnectionString()).Database.SqlQuery<SpGetSpendModel>("exec SpGetSpend @sourceId,@fromDate,@toDate", id, from, to).ToList<SpGetSpendModel>();
                    dt = ApplicationState.ToDataTable(list);
                    SpendReport spendReport = new SpendReport();
                    Path = System.IO.Path.Combine(Path, @"Reports\SpendReport.rpt");
                    spendReport.Load(Path);
                    spendReport.SetDataSource(dt);
                    spendReport.Refresh();
                    reportViewer.ViewerCore.ReportSource = spendReport;
                }
            }
            catch (Exception ex)
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: {ex.Message}";
                Dialog.IsOpen = true;
                _unitOfWork.Log.Add(new Log { Date = DateTime.Now, Exception = ex.Message, Level = "Test", Logger = "Test", Thread = "Test", Message = ex.Message });
                _unitOfWork.Complete();

            }
        }

        private void CmbreportType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cmbSourceName.Visibility = Visibility.Visible;
                if (cmbreportType.SelectedIndex == 0)
                {
                    var items = _unitOfWork.IncomeSource.GetAll().ToList();
                    items.Add(new IncomeSource { IncomeSourceId = 0, Name = "All" });
                    cmbSourceName.ItemsSource = items.OrderBy(f => f.IncomeSourceId);
                    cmbSourceName.DisplayMemberPath = "Name";
                    cmbSourceName.SelectedValuePath = "IncomeSourceId";
                }

                if (cmbreportType.SelectedIndex == 1)
                {
                    var items = _unitOfWork.SpendSource.GetAll().ToList();
                    items.Add(new SpendSource { SpendSourceId = 0, Name = "All" });
                    cmbSourceName.ItemsSource = items.OrderBy(f => f.SpendSourceId);
                    cmbSourceName.DisplayMemberPath = "Name";
                    cmbSourceName.SelectedValuePath = "SpendSourceId";
                }
            }
            catch (Exception ex)
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: {ex.Message}";
                Dialog.IsOpen = true;
                _unitOfWork.Log.Add(new Log { Date = DateTime.Now, Exception = ex.Message, Level = "Test", Logger = "Test", Thread = "Test", Message = ex.Message });
                _unitOfWork.Complete();

            }
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                dialog.ShowDialog();
                txtPath.Text = dialog.SelectedPath;
                backup.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: {ex.Message}";
                Dialog.IsOpen = true;
                _unitOfWork.Log.Add(new Log { Date = DateTime.Now, Exception = ex.Message, Level = "Test", Logger = "Test", Thread = "Test", Message = ex.Message });
                _unitOfWork.Complete();

            }

        }

        private void Backup_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                SqlConnection con = new SqlConnection(DbSettingHelper.RawBuildConnectionString());
                string database = con.Database.ToString();
                string cmd = "BACKUP DATABASE [" + database + "] TO DISK= '" + txtPath.Text + "\\" + "Database" + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'";
                using (SqlCommand command = new SqlCommand(cmd, con))
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    command.ExecuteNonQuery();
                    con.Close();

                }
                status.Text = $"ডাটাবেস ব্যাক আপ সংরক্ষণ করা হয়েছে।";
                Dialog.IsOpen = true;
                backup.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: {ex.Message}";
                Dialog.IsOpen = true;
                _unitOfWork.Log.Add(new Log {Date=DateTime.Now,Exception=ex.Message,Level= "Test", Logger= "Test", Thread="Test", Message = ex.Message });
                _unitOfWork.Complete();

            }

        }
    }
}

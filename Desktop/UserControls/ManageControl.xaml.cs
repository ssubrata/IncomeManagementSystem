
using AutoMapper;
using DAL.Repository.Implement;
using Desktop.DataModel;
using Desktop.Helper;
using Desktop.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desktop.UserControls
{
    /// <summary>
    /// Interaction logic for MangeControl.xaml
    /// </summary>

    /// <summary>
 
    public partial class ManageControl : UserControl
    {
        /// All Intial Property List ;
        /// </summary>
        private readonly UnitOfWork _unitOfWork;
        public ObservableCollection<VmIncomeSource> vmincomeSouceList = new ObservableCollection<VmIncomeSource>();
        public ObservableCollection<VmSpendSource> vmspendsourceList = new ObservableCollection<VmSpendSource>();
        public ObservableCollection<VmUser> VmUserList = new ObservableCollection<VmUser>();
        public ObservableCollection<VmRole> VmRoleList = new ObservableCollection<VmRole>();
        public ManageControl()
        {
            try
            {
                InitializeComponent();
                Loaded += IntialSetup;
                _unitOfWork = DbSettingHelper.UnitOfWork;
                DataContext = new ManageViewModel() { LoginViewModel = new LoginViewModel { IsAdmin = ApplicationState.GetValue<bool>("IsAdmin") } };
                if (ApplicationState.GetValue<bool>("IsAdmin"))

                    //Spend Source actions
                SpendSourceSave.Click += SpendSourceSave_Click;
                SpendSourceDelete.Click += SpendSourceDelete_Click;
                SpendSourceUpdate.Click += SpendSourceUpdate_Click;
                SpendSourceGridName.SelectedCellsChanged += SpendSourceGridName_SelectedCellsChanged;
                SpendSourceGridName.MouseDoubleClick += SpendSourceGridName_LostFocus;
                txtSpendSearch.SelectionChanged += TxtSpendSourceName_SelectionChanged;

                //Income Source actions
                IncomeSourceSave.Click += IncomeSourceSave_Click;
                IncomeSourceDelete.Click += IncomeSourceDelete_Click;
                IncomeSourceUpdate.Click += IncomeSourceUpdate_Click;
                IncomeSourceGridName.SelectedCellsChanged += IncomeSourceGridName_SelectedCellsChanged;
                IncomeSourceGridName.MouseDoubleClick += IncomeSourceGridName_LostFocus;
                txtIncomeSearch.SelectionChanged += TxtIncomeSearch_SelectionChanged;

                //User actions
                Save.Click += Save_Click;
                Update.Click += Update_Click;
                Delete.Click += Delete_Click;
                UserGridName.SelectedCellsChanged += UserGridName_SelectedCellsChanged;
                UserGridName.MouseDoubleClick += UserGridName_LostFocus;
                txtUserSearch.SelectionChanged += TxtUserSearch_SelectionChanged;

                Save.Visibility = Visibility.Visible;
                Update.Visibility = Visibility.Hidden;
                Delete.Visibility = Visibility.Hidden;

                
                IncomeSourceSave.Visibility = Visibility.Visible;
                IncomeSourceDelete.Visibility = Visibility.Hidden;
                IncomeSourceUpdate.Visibility = Visibility.Hidden;

                SpendSourceSave.Visibility = Visibility.Visible;
                SpendSourceDelete.Visibility = Visibility.Hidden;
                SpendSourceUpdate.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: { ex.Message}";
                Dialog.IsOpen = true;
            }
           


        }
        private void UserGridName_LostFocus(object sender, RoutedEventArgs e)
        {
           
            UserGridName.SelectedItem = null;
            ClearTextBoxes();
            Save.Visibility = Visibility.Visible;
            Update.Visibility = Visibility.Hidden;
            Delete.Visibility = Visibility.Hidden;

        }

        private void IncomeSourceGridName_LostFocus(object sender, RoutedEventArgs e)
        {
            IncomeSourceGridName.SelectedItem = null;
            txtIncomeSourcedate.SelectedDate = null;
            txtIncomeSourceName.Text = string.Empty;

            IncomeSourceSave.Visibility = Visibility.Visible;
            IncomeSourceDelete.Visibility = Visibility.Hidden;
            IncomeSourceUpdate.Visibility = Visibility.Hidden;

        }

        private void SpendSourceGridName_LostFocus(object sender, RoutedEventArgs e)
        {
            SpendSourceGridName.SelectedItem = null;
            txtSpendSourcedate.SelectedDate = null;
            txtSpendSourceName.Text = string.Empty;

            SpendSourceSave.Visibility = Visibility.Visible;
            SpendSourceDelete.Visibility = Visibility.Hidden;
            SpendSourceUpdate.Visibility = Visibility.Hidden;
        }



        private void TxtSpendSourceName_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtSpendSearch.Text == string.Empty)
                {
                    SpendSourceGridName.ItemsSource = vmspendsourceList;
                    return;


                }
                SpendSourceGridName.ItemsSource = vmspendsourceList.Where(f => f.Name.ToUpper() == txtSpendSearch.Text.ToUpper());

            }
            catch (Exception ex)
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: { ex.Message}";
                Dialog.IsOpen = true;
            }
          
        }

        private void SpendSourceGridName_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (SpendSourceGridName.SelectedItem == null) return;

                SpendSourceSave.Visibility = Visibility.Hidden;
                SpendSourceDelete.Visibility = Visibility.Visible;
                SpendSourceUpdate.Visibility = Visibility.Visible;
                var selected = SpendSourceGridName.SelectedItem as VmSpendSource;
                txtSpendSourceName.Text = selected.Name;
                txtSpendSourcedate.SelectedDate = selected.Date;
            }
            catch (Exception ex)
            {

                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: { ex.Message}";
                Dialog.IsOpen = true;
            }
        
        }

        private void SpendSourceUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = SpendSourceGridName.SelectedItem as VmSpendSource;
                var find = _unitOfWork.SpendSource.GetById(selected.SpendSourceId);
                if (find != null)
                {
                    find.Date = txtSpendSourcedate.SelectedDate;
                    find.Name = txtSpendSourceName.Text.ToString();
                    _unitOfWork.Complete();
                    var exist = vmspendsourceList.FirstOrDefault(f => f.SpendSourceId == find.SpendSourceId);
                    int i = vmspendsourceList.IndexOf(exist);
                    vmspendsourceList.RemoveAt(vmspendsourceList.IndexOf(exist));
                    vmspendsourceList.Add(Mapper.Map<VmSpendSource>(find));
                    SpendSourceGridName.SelectedItem = null;
                    txtSpendSourcedate.SelectedDate = null;
                    txtSpendSourceName.Text = string.Empty;
                    status.Text = $"ব্যায়ের খাত হালনাগাদ করা হয়েছে";
                    Dialog.IsOpen = true;

                    SpendSourceSave.Visibility = Visibility.Visible;
                    SpendSourceDelete.Visibility = Visibility.Hidden;
                    SpendSourceUpdate.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: { ex.Message}";
                Dialog.IsOpen = true;
            }
           
        }

        private void SpendSourceDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SpendSourceGridName.SelectedItem != null)
                {
                    var selected = SpendSourceGridName.SelectedItem as VmSpendSource;
                    var find = _unitOfWork.SpendSource.GetById(Convert.ToInt32(selected.SpendSourceId));
                    _unitOfWork.SpendSource.Remove(find);
                    _unitOfWork.Complete();
                    vmspendsourceList.Remove(SpendSourceGridName.SelectedItem as VmSpendSource);
                    SpendSourceGridName.SelectedItem = null;
                    txtSpendSourcedate.SelectedDate = null;
                    txtSpendSourceName.Text = string.Empty;
                    status.Text = $"ব্যায়ের খাত মুছে ফেলা হয়েছে";
                    Dialog.IsOpen = true;

                    SpendSourceSave.Visibility = Visibility.Visible;
                    SpendSourceDelete.Visibility = Visibility.Hidden;
                    SpendSourceUpdate.Visibility = Visibility.Hidden;

                }
            }
            catch ( Exception ex)
            { 
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: { ex.Message}";
                Dialog.IsOpen = true;
            }
            
        }

        private void SpendSourceSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var value = new VmSpendSource { Date = txtSpendSourcedate.SelectedDate.Value, Name = txtSpendSourceName.Text.ToString() };
                var existUser = _unitOfWork.SpendSource.GetByName(value.Name);
                if (existUser != null)
                {
                    status.Text = $"নকল ব্যায়ের খাত প্রদান করা যাবে না";
                    Dialog.IsOpen = true;
                    return;
                }

                _unitOfWork.SpendSource.Add(Mapper.Map<SpendSource>(value));
                _unitOfWork.Complete();
                vmspendsourceList.Add(Mapper.Map<VmSpendSource>(_unitOfWork.SpendSource.GetAll().Last()));
                SpendSourceGridName.SelectedItem = null;
                txtSpendSourcedate.SelectedDate = null;
                txtSpendSourceName.Text = string.Empty;
                status.Text = $"ব্যায়ের খাত সংরক্ষণ করা হয়েছে";
                Dialog.IsOpen = true;
            }
            catch (Exception ex)
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: { ex.Message}";
                Dialog.IsOpen = true;
            }
          

        }

        private void IncomeSourceGridName_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (IncomeSourceGridName.SelectedItem == null) return;
                IncomeSourceSave.Visibility = Visibility.Hidden;
                IncomeSourceDelete.Visibility = Visibility.Visible;
                IncomeSourceUpdate.Visibility = Visibility.Visible;
                var selected = IncomeSourceGridName.SelectedItem as VmIncomeSource;
                txtIncomeSourceName.Text = selected.Name;
                txtIncomeSourcedate.SelectedDate = selected.Date;
            }
            catch (Exception ex)
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: { ex.Message}";
                Dialog.IsOpen = true;
            }
        }

        private void TxtUserSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtUserSearch.Text == string.Empty)
                {
                    UserGridName.ItemsSource = VmUserList;
                    return;

                }
                UserGridName.ItemsSource = VmUserList.Where(f => f.FullName.ToUpper() == txtUserSearch.Text.ToUpper() || f.UserName.ToUpper() == txtUserSearch.Text.ToUpper());
            }
            catch (Exception ex)
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: { ex.Message}";
                Dialog.IsOpen = true;
            }
           
        }

        private void TxtIncomeSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtIncomeSearch.Text == string.Empty)
                {
                    IncomeSourceGridName.ItemsSource = vmincomeSouceList;
                    return;
                }
                IncomeSourceGridName.ItemsSource = vmincomeSouceList.Where(f => f.Name.ToUpper() == txtIncomeSearch.Text.ToUpper());
            }
            catch (Exception ex)
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: { ex.Message}";
                Dialog.IsOpen = true;
            }
           
        }

        private void IncomeSourceUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = IncomeSourceGridName.SelectedItem as VmIncomeSource;

                if (IncomeSourceGridName.SelectedItem != null)
                {
                    var find = _unitOfWork.IncomeSource.GetById(selected.IncomeSourceId);
                    if (find != null)
                    {
                        find.Date = txtIncomeSourcedate.SelectedDate;
                        find.Name = txtIncomeSourceName.Text.ToString();
                        _unitOfWork.Complete();
                        var exist = vmincomeSouceList.FirstOrDefault(f => f.IncomeSourceId == find.IncomeSourceId);
                        int i = vmincomeSouceList.IndexOf(exist);
                        vmincomeSouceList.RemoveAt(vmincomeSouceList.IndexOf(exist));
                        vmincomeSouceList.Add(Mapper.Map<VmIncomeSource>(find));
                        IncomeSourceGridName.SelectedItem = null;
                        txtIncomeSourcedate.SelectedDate = null;
                        txtIncomeSourceName.Text = string.Empty;
                        status.Text = $"আয়ের খাত হালনাগাদ করা হয়েছে";
                        Dialog.IsOpen = true;

                        IncomeSourceSave.Visibility = Visibility.Visible;
                        IncomeSourceDelete.Visibility = Visibility.Hidden;
                        IncomeSourceUpdate.Visibility = Visibility.Hidden;

                    }
                }
            }
            catch (Exception ex)
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: { ex.Message}";
                Dialog.IsOpen = true;
            }
          
        }

        private void IncomeSourceDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IncomeSourceGridName.SelectedItem != null)
                {
                    var selected = IncomeSourceGridName.SelectedItem as VmIncomeSource;
                    var find = _unitOfWork.IncomeSource.GetById(Convert.ToInt32(selected.IncomeSourceId));
                    _unitOfWork.IncomeSource.Remove(find);
                    _unitOfWork.Complete();
                    vmincomeSouceList.Remove(IncomeSourceGridName.SelectedItem as VmIncomeSource);
                  
                    IncomeSourceGridName.SelectedItem = null;
                    txtIncomeSourcedate.SelectedDate = null;
                    txtIncomeSourceName.Text = string.Empty;
                    status.Text = $"আয়ের খাত মুছে ফেলা হয়েছে";
                    Dialog.IsOpen = true;

                    IncomeSourceSave.Visibility = Visibility.Visible;
                    IncomeSourceDelete.Visibility = Visibility.Hidden;
                    IncomeSourceUpdate.Visibility = Visibility.Hidden;

                }
            }
            catch (Exception ex)
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: { ex.Message}";
                Dialog.IsOpen = true;
            }
       

        }

        private void IncomeSourceSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var value = new VmIncomeSource { Date = txtIncomeSourcedate.SelectedDate.Value, Name = txtIncomeSourceName.Text.ToString() };
                var existUser = _unitOfWork.IncomeSource.GetByName(value.Name);
                if (existUser != null)
                {
                    status.Text = $"নকল আয়ের খাত প্রদান করা যাবে না";
                    Dialog.IsOpen = true;
                    return;
                }
                _unitOfWork.IncomeSource.Add(Mapper.Map<IncomeSource>(value));
                _unitOfWork.Complete();
                vmincomeSouceList.Add(Mapper.Map<VmIncomeSource>(_unitOfWork.IncomeSource.GetAll().Last()));
                IncomeSourceGridName.SelectedItem = null;
                txtIncomeSourcedate.SelectedDate = null;
                txtIncomeSourceName.Text = string.Empty;
                status.Text = $"আয়ের খাত সংরক্ষণ করা হয়েছে";
                Dialog.IsOpen = true;

            }
            catch (Exception ex)
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: { ex.Message}";
                Dialog.IsOpen = true; 
            }
          
        }

        /// <summary>
        /// User List Selection From Grid Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void UserGridName_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (UserGridName.SelectedItem == null) return;
                Save.Visibility = Visibility.Hidden;
                Update.Visibility = Visibility.Visible;
                Delete.Visibility = Visibility.Visible;
                var selected = UserGridName.SelectedItem as VmUser;
                txtFullName.Text = selected.FullName;
                txtNationalId.Text = selected.NationalId;
                txtPassword.Text = selected.Password;
                txtUserName.Text = selected.UserName;
                txtAddress.Text = selected.Address;
                cmbRole.SelectedValue = selected.RoleId;
                cmbRole.SelectedItem = selected.VmRole;
            }
            catch (Exception ex)
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: { ex.Message}";
                Dialog.IsOpen = true; ;
            }
          
        }

        /// <summary>
        /// Seleted User Delete Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserGridName.SelectedItem == null) return;
                var selected = UserGridName.SelectedItem as VmUser;
                var find = _unitOfWork.User.GetById(Convert.ToInt32(selected.Id));
                _unitOfWork.User.Remove(find);
                _unitOfWork.Complete();
                VmUserList.Remove(UserGridName.SelectedItem as VmUser);
             
                status.Text = $"ইউসার তথ্য মুছে ফেলা হয়েছে";
                Dialog.IsOpen = true;
                UserGridName.SelectedItem = null;
                ClearTextBoxes();

                Save.Visibility = Visibility.Visible;
                Update.Visibility = Visibility.Hidden;
                Delete.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: { ex.Message}";
                Dialog.IsOpen = true; ;
            }
           

        }

        /// <summary>
        /// Sleected User Update Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserGridName.SelectedItem == null) return;
                var find = _unitOfWork.User.GetById((UserGridName.SelectedItem as VmUser).Id);
                find = MapDataFromControl(find);
                _unitOfWork.Complete();
                var exist = VmUserList.FirstOrDefault(f => f.Id == find.Id);
                int i = VmUserList.IndexOf(exist);
                VmUserList.RemoveAt(VmUserList.IndexOf(exist));
                VmUserList.Add(Mapper.Map<VmUser>(find));
                status.Text = $"ইউসার তথ্য হালনাগাদ করা হয়েছে";
                Dialog.IsOpen = true;
                UserGridName.SelectedItem = null;
                ClearTextBoxes();

                Save.Visibility = Visibility.Visible;
                Update.Visibility = Visibility.Hidden;
                Delete.Visibility = Visibility.Hidden;
            }
            catch (Exception ex) 
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: { ex.Message}";
                Dialog.IsOpen = true;
            }
            

        }

        /// <summary>
        /// New User Save Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var mapData = MapDataFromControl(null);

                var existUser = _unitOfWork.User.GetByUserName(mapData.UserName);
                if (existUser != null)
                {
                    status.Text = $"নকল ইউসার নাম প্রদান করা যাবে না";
                    Dialog.IsOpen = true;
                    return;
                }

                _unitOfWork.User.Add(mapData);
                _unitOfWork.Complete();
                VmUserList.Add(Mapper.Map<VmUser>(_unitOfWork.User.GetAll().Last()));
         
                status.Text = $"ইউসার তথ্য সংৰক্ষণ করা হয়েছে";
                UserGridName.SelectedItem = null;
                ClearTextBoxes();
            }
            catch (Exception ex)
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: { ex.Message}";
                Dialog.IsOpen = true;
            }
           
        }

        /// <summary>
        /// Clear All User Input Text Method
        /// </summary>
        private void ClearTextBoxes()
        {
            cmbRole.SelectedItem = null;
            txtFullName.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtNationalId.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPassword.Text = string.Empty;


        }

        /// <summary>
        /// User Input Text Map To User Object;
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        private User MapDataFromControl(User ob)
        {

            if (ob == null)
            {
                ob = new User();
            }

            ob.FullName = txtFullName.Text;
            ob.UserName = txtUserName.Text;
            ob.NationalId = txtNationalId.Text;
            ob.Address = txtAddress.Text;
            ob.Password = txtPassword.Text;
            ob.RoleId = (int)cmbRole.SelectedValue;


            return ob;
        }

        /// <summary>
        /// Intial Setup For Dashboard Control;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IntialSetup(object sender, System.Windows.RoutedEventArgs e)
        {

            vmincomeSouceList = new ObservableCollection<VmIncomeSource>(Mapper.Map<List<VmIncomeSource>>(_unitOfWork.IncomeSource.GetAll().ToList()));
            vmspendsourceList = new ObservableCollection<VmSpendSource>(Mapper.Map<List<VmSpendSource>>(_unitOfWork.SpendSource.GetAll().ToList()));
            VmUserList = new ObservableCollection<VmUser>(Mapper.Map<List<VmUser>>(_unitOfWork.User.GetAll().ToList()));
            VmRoleList = new ObservableCollection<VmRole>(Mapper.Map<List<VmRole>>(_unitOfWork.Role.GetAll().ToList()));

            IncomeSourceGridName.ItemsSource = vmincomeSouceList;
            SpendSourceGridName.ItemsSource = vmspendsourceList;
            UserGridName.ItemsSource = VmUserList;
            cmbRole.ItemsSource = VmRoleList;

        }

    }
}

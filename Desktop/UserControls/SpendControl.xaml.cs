
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
    /// Interaction logic for SpendControl.xaml
    /// </summary>
    public partial class SpendControl : UserControl
    {
        public ObservableCollection<VmSpend> vmSpendList = new ObservableCollection<VmSpend>();
        private readonly UnitOfWork _unitOfWork;

        public SpendControl()
        {
            try
            {
                InitializeComponent();
                _unitOfWork = DbSettingHelper.UnitOfWork;

                Loaded += IncomeLoaded;
                Save.Click += SaveSpend;
                Delete.Click += DeleteSpend;
                Update.Click += UpdateSpend;
                searchText.SelectionChanged += SpendText_SelectionChanged;
                SpendGridName.SelectedCellsChanged += DateGridSelectedCells;
                SpendGridName.MouseDoubleClick += SpendGridName_MouseDoubleClick;
                Update.Visibility = System.Windows.Visibility.Collapsed;
                Delete.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: { ex.Message}";
                Dialog.IsOpen = true;
            }
       
        }

        private void SpendGridName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SpendGridName.SelectedItem = null;
            ClearTextBoxes();
            Delete.Visibility = System.Windows.Visibility.Hidden;
            Update.Visibility = System.Windows.Visibility.Hidden;
            Save.Visibility = System.Windows.Visibility.Visible;

        }

        private void IncomeLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = new SpendViewModel
            {
                LoginViewModel = new LoginViewModel() { IsAdmin = ApplicationState.GetValue<bool>("IsAdmin") }
            };
            txtDate.SelectedDate = null;
            vmSpendList = new ObservableCollection<VmSpend>(Mapper.Map<List<Spend>, List<VmSpend>>(_unitOfWork.Spends.GetAll().ToList()));
            SpendGridName.ItemsSource = vmSpendList;
            cmbSpendSource.ItemsSource = _unitOfWork.SpendSource.GetAll().ToList();
        }

        private void UpdateSpend(object sender, System.Windows.RoutedEventArgs e)
        {
            if (SpendGridName.SelectedItem == null) return;
            var find = _unitOfWork.Spends.GetById((SpendGridName.SelectedItem as VmSpend).Id);
            find = MapDataFromControl(find);
            _unitOfWork.Complete();
            var exist = vmSpendList.FirstOrDefault(f => f.Id == find.SpendId);
            int i = vmSpendList.IndexOf(exist);
            vmSpendList.RemoveAt(vmSpendList.IndexOf(exist));
            vmSpendList.Add(Mapper.Map<VmSpend>(find));
            Dialog.IsOpen = true;
            status.Text = $"রিসিপ্ট নং: {find.ReciptNo} এবং বুক নং: {find.BookNo} হালনাগাদ করা হয়েছে ";
            SpendGridName.SelectedItem = null;
            ClearTextBoxes();
            Delete.Visibility = System.Windows.Visibility.Hidden;
            Update.Visibility = System.Windows.Visibility.Hidden;
            Save.Visibility = System.Windows.Visibility.Visible;

        }

        private void SaveSpend(object sender, System.Windows.RoutedEventArgs e)
        {
            var mapData = MapDataFromControl(null);
            _unitOfWork.Spends.Add(mapData);
            _unitOfWork.Complete();
            vmSpendList.Add(Mapper.Map<VmSpend>(mapData));
            status.Text = $"রিসিপ্ট নং: {mapData.ReciptNo} এবং বুক নং: {mapData.BookNo} সংরক্ষণ হয়েছে ";
            Dialog.IsOpen = true;
            SpendGridName.SelectedItem = null;
            ClearTextBoxes();
        }

        private void DeleteSpend(object sender, System.Windows.RoutedEventArgs e)
        {
            if (SpendGridName.SelectedItem == null) return;
            var selected = SpendGridName.SelectedItem as VmSpend;
            var find = _unitOfWork.Spends.GetById(Convert.ToInt32(selected.Id));
            _unitOfWork.Spends.Remove(find);
            vmSpendList.Remove(SpendGridName.SelectedItem as VmSpend);
            _unitOfWork.Complete();
            Dialog.IsOpen = true;
            status.Text = $"রিসিপ্ট নং: {selected.ReciptNo} এবং বুক নং: {selected.BookNo} মুছে ফেলা হয়েছে";
            ClearTextBoxes();
            SpendGridName.SelectedItem = null;
            Delete.Visibility = System.Windows.Visibility.Hidden;
            Update.Visibility = System.Windows.Visibility.Hidden;
            Save.Visibility = System.Windows.Visibility.Visible;

        }

        private void DateGridSelectedCells(object sender, SelectedCellsChangedEventArgs e)
        {
            if (SpendGridName.SelectedItem == null) return;
            Delete.Visibility = System.Windows.Visibility.Visible;
            Update.Visibility = System.Windows.Visibility.Visible;
            Save.Visibility = System.Windows.Visibility.Hidden;
            var selected = SpendGridName.SelectedItem as VmSpend;
            txtbookno.Text = selected.BookNo;
            txtReciptNo.Text = selected.ReciptNo;
            txtDescription.Text = selected.Description;
            txtDate.SelectedDate = selected.Date;
            txtName.Text = selected.Name;
            txtAddress.Text = selected.Address;
            cmbSpendSource.SelectedValue = selected.SourceId;
            txtAmount.Text = selected.Amount.ToString();
            cmbSpendSource.SelectedItem = selected.VmSpendSource;

        }

        private Spend MapDataFromControl(Spend ob)
        {
            if (ob == null)
            {
                ob = new Spend();
            }

            ob.Date = txtDate.SelectedDate;
            ob.BookNo = txtbookno.Text;
            ob.ReciptNo = txtReciptNo.Text;
            ob.Name = txtName.Text;
            ob.Address = txtAddress.Text;
            ob.Description = txtDescription.Text;
            ob.Amount = Convert.ToDecimal(txtAmount.Text);
            ob.SourceId = (int)cmbSpendSource.SelectedValue;
            return ob;
        }

        private void ClearTextBoxes()
        {
            txtDate.SelectedDate = null;
            txtbookno.Text = string.Empty;
            txtReciptNo.Text = string.Empty;
            txtName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtAmount.Text = string.Empty;
            cmbSpendSource.SelectedItem = null;

        }

        private void SpendText_SelectionChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (searchText.Text == string.Empty)
            {
                SpendGridName.ItemsSource = vmSpendList;
                return;
            }
            DateTime SearchDate;
            if (DateTime.TryParse(searchText.Text, out SearchDate))
            {
                SpendGridName.ItemsSource = vmSpendList.Where(f => f.Date == SearchDate);
                return;
            }
            SpendGridName.ItemsSource = vmSpendList.Where(f => f.BookNo.ToUpper() == searchText.Text || f.ReciptNo.ToUpper() == searchText.Text);

        }
    }
}

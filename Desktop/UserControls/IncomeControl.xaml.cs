
using AutoMapper;
using DAL.Repository.Implement;
using Desktop.DataModel;
using Desktop.Helper;
using Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace Desktop.UserControls
{
    /// <summary>
    /// Interaction logic for IncomeControl.xaml
    /// </summary>
    ///


    public partial class IncomeControl : UserControl
    {
       
        public ObservableCollection<VmIncome> vmIncomeList = new ObservableCollection<VmIncome>();
        private readonly UnitOfWork _unitOfWork;

        public IncomeControl()
        {
            try
            {
                InitializeComponent();
                _unitOfWork = DbSettingHelper.UnitOfWork;

                Loaded += IncomeLoaded;
                Save.Click += SaveIncome;
                Delete.Click += DeleteIncome;
                Update.Click += UpdateIncome;
                searchText.SelectionChanged += searchText_SelectionChanged;
                DateGridName.SelectedCellsChanged += DateGridSelectedCells;
                DateGridName.MouseDoubleClick += DateGridName_MouseDoubleClick;
                Update.Visibility = System.Windows.Visibility.Collapsed;
                Delete.Visibility = System.Windows.Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                status.Text = $"প্রোগ্রাম সম্পর্কিত সমস্যা: { ex.Message}";
                Dialog.IsOpen = true;
            }
         
        }

        private void DateGridName_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DateGridName.SelectedItem = null;
            ClearTextBoxes();
            Delete.Visibility = System.Windows.Visibility.Hidden;
            Update.Visibility = System.Windows.Visibility.Hidden;
            Save.Visibility = System.Windows.Visibility.Visible;

        }

        private void IncomeLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = new IncomeViewModel
            {
                LoginViewModel = new LoginViewModel() { IsAdmin = ApplicationState.GetValue<bool>("IsAdmin") }
            };
            txtDate.SelectedDate = null;
            vmIncomeList = new ObservableCollection<VmIncome>(Mapper.Map<List<Income>, List<VmIncome>>(_unitOfWork.Incomes.GetAll().ToList()));
            DateGridName.ItemsSource = vmIncomeList;
            cmbIncomeSource.ItemsSource = _unitOfWork.IncomeSource.GetAll().ToList();
        }

        private void UpdateIncome(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DateGridName.SelectedItem == null) return;
            var find = _unitOfWork.Incomes.GetById((DateGridName.SelectedItem as VmIncome).Id);
            find = MapDataFromControl(find);
            _unitOfWork.Complete();
            var exist = vmIncomeList.FirstOrDefault(f => f.Id == find.IncomeId);
            int i = vmIncomeList.IndexOf(exist);
            vmIncomeList.RemoveAt(vmIncomeList.IndexOf(exist));
            vmIncomeList.Add(Mapper.Map<VmIncome>(find));
            Dialog.IsOpen = true;
            status.Text = $"রিসিপ্ট নং: {find.ReciptNo} এবং বুক নং: {find.BookNo} হালনাগাদ করা হয়েছে ";
            DateGridName.SelectedItem = null;
            ClearTextBoxes();
            Delete.Visibility = System.Windows.Visibility.Hidden;
            Update.Visibility = System.Windows.Visibility.Hidden;
            Save.Visibility = System.Windows.Visibility.Visible;
        }

        private void SaveIncome(object sender, System.Windows.RoutedEventArgs e)
        {
            var mapData = MapDataFromControl(null);
            _unitOfWork.Incomes.Add(mapData);
            _unitOfWork.Complete();
             vmIncomeList.Add(Mapper.Map<VmIncome>(mapData));
            status.Text = $"রিসিপ্ট নং: {mapData.ReciptNo} এবং বুক নং: {mapData.BookNo}  সংরক্ষণ হয়েছে ";
            Dialog.IsOpen = true;
            DateGridName.SelectedItem = null;
            ClearTextBoxes();

        }

        private void DeleteIncome(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DateGridName.SelectedItem == null) return;
            var selected = DateGridName.SelectedItem as VmIncome;
            var find = _unitOfWork.Incomes.GetById(Convert.ToInt32(selected.Id));
            _unitOfWork.Incomes.Remove(find);
            vmIncomeList.Remove(DateGridName.SelectedItem as VmIncome);
            _unitOfWork.Complete();
            Dialog.IsOpen = true;
            status.Text = $"রিসিপ্ট নং: {selected.ReciptNo} এবং বুক নং: {selected.BookNo} মুছে ফেলা হয়েছে";
            ClearTextBoxes();
            DateGridName.SelectedItem = null;
            Delete.Visibility = System.Windows.Visibility.Hidden;
            Update.Visibility = System.Windows.Visibility.Hidden;
            Save.Visibility = System.Windows.Visibility.Visible;

        }

        private void DateGridSelectedCells(object sender, SelectedCellsChangedEventArgs e)
        {
            if (DateGridName.SelectedItem == null) return;
            Delete.Visibility = System.Windows.Visibility.Visible;
            Update.Visibility = System.Windows.Visibility.Visible;
            Save.Visibility = System.Windows.Visibility.Hidden;
            var selected = DateGridName.SelectedItem as VmIncome;
            txtbookno.Text = selected.BookNo;
            txtReciptNo.Text = selected.ReciptNo;
            txtDescription.Text = selected.Description;
            txtDate.SelectedDate = selected.Date;
            txtName.Text = selected.Name;
            txtAddress.Text = selected.Address;
            cmbIncomeSource.SelectedValue = selected.SourceId;
            txtAmount.Text = selected.Amount.ToString();
            cmbIncomeSource.SelectedItem = selected.VmIncomeSource;

        }
        
        private Income MapDataFromControl(Income ob)
        {
            if (ob == null)
            {
                ob = new Income();
            }
            ob.Date = txtDate.SelectedDate;
            ob.BookNo = txtbookno.Text;
            ob.ReciptNo = txtReciptNo.Text;
            ob.Name = txtName.Text;
            ob.Address = txtAddress.Text;
            ob.Description = txtDescription.Text;
            ob.Amount = Convert.ToDecimal(txtAmount.Text);
            ob.SourceId = (int)cmbIncomeSource.SelectedValue;


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
            cmbIncomeSource.SelectedItem = null;

        }

        private void searchText_SelectionChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (searchText.Text == string.Empty)
            {
                DateGridName.ItemsSource = vmIncomeList;
                return;
            }

            DateTime SearchDate;
            if (DateTime.TryParse(searchText.Text, out SearchDate))
            {
                DateGridName.ItemsSource = vmIncomeList.Where(f => f.Date == SearchDate);
                return;
            }
            DateGridName.ItemsSource = vmIncomeList.Where(f => f.BookNo.ToUpper() == searchText.Text.ToUpper() || f.ReciptNo.ToUpper() == searchText.Text.ToUpper());

        }
    }
}

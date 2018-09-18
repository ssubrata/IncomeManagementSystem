using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
    class ManageViewModel: INotifyPropertyChanged
    {
        public string _fullName { get; set; }
        public string _userName { get; set; }
        public string _roleName { get; set; }
        public string _nationalId { get; set; }
        public string _password { get; set; }

        public string _incomeSourceDate { get; set; }
        public string _spendSourcedDate { get; set; }
        public string _incomeSourceName { get; set; }
        public string _spendSourceName { get; set; }

        public LoginViewModel LoginViewModel { get; set; }
        public string IncomeSourceDate
        {
            get { return _incomeSourceDate; }
            set { if (_incomeSourceDate == value) return; _incomeSourceDate = value; OnPropertyChanged(); }
        }
        public string SpendSourceDate
        {
            get { return _spendSourcedDate; }
            set { if (_spendSourcedDate == value) return; _spendSourcedDate = value; OnPropertyChanged(); }
        }
        public string IncomeSourceName
        {
            get { return _incomeSourceName; }
            set { if (_incomeSourceName == value) return; _incomeSourceName = value; OnPropertyChanged(); }
        }
        public string SpendSourceName
        {
            get { return _spendSourceName; }
            set { if (_spendSourceName == value) return; _spendSourceName = value; OnPropertyChanged(); }
        }


        public string Password
        {
            get { return _password; }
            set { if (_password == value) return; _password = value; OnPropertyChanged(); }
        }
        public string RoleName
        {
            get { return _roleName; }
            set { if (_roleName == value) return; _roleName = value; OnPropertyChanged(); }
        }
        public string UserName
        {
            get { return _userName; }
            set { if (_userName == value) return; _userName = value; OnPropertyChanged(); }
        }
        public string FullName
        {
            get { return _fullName; }
            set { if (_fullName == value) return; _fullName = value; OnPropertyChanged(); }
        }
        
        public string NationalId
        {
            get { return _nationalId; }
            set { if (_nationalId == value) return; _nationalId = value; OnPropertyChanged(); }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

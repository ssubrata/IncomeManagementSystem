using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
   public class IncomeViewModel: INotifyPropertyChanged
    {
        private string _name;
        private string _bookNo;
        private string _reciptNo;
        private string _description;
        private string _amount;
        private DateTime _date;
        private string _address;
        private string _incomeSoruces;

        public LoginViewModel LoginViewModel { get; set; }
        public string IncomeSoruces
        {
            get { return _incomeSoruces; }
            set { if (_incomeSoruces == value) return; _incomeSoruces = value; OnPropertyChanged(); }
        }
        public string Address
        {
            get { return _address; }
            set { if (_address == value) return; _address = value; OnPropertyChanged(); }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }
        public string BookNo
        {
            get { return _bookNo; }
            set
            {
                if (_bookNo == value)
                    return;
                _bookNo = value;
                OnPropertyChanged();
            }
        }
        public string ReciptNO
        {
            get { return _reciptNo; }
            set
            {
                if (_reciptNo == value)
                    return;
                _reciptNo = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description == value)
                    return;
                _description = value;
                OnPropertyChanged();
            }
        }
        public string Amount
        {
            get { return _amount; }
            set
            {
                if (_amount == value)
                    return;
                _amount = value;
                OnPropertyChanged();
            }
        }
        public DateTime Date
        {
            get { return _date; }
            set
            {
                
                if (_date == value)
                    return;
                _date = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}


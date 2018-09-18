using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
   public class ReportViewModel:INotifyPropertyChanged
    {
        public string _reportType;
        public string _sourceName;
        public string ReportType { get { return _reportType; } set { if (_reportType == value) return; _reportType = value; OnPropertyChanged(); } }
        public string SourceName { get { return _sourceName; } set { if (_sourceName == value) return; _sourceName = value; OnPropertyChanged(); } }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

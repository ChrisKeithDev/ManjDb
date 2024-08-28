using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ManjReports
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private DateTime _selectedDate;
        private bool cbEmergencyChecked;
        private bool cbAllergiesChecked;
        private bool cbPickupChecked;
        private bool cbAgesChecked;
        private bool cbParentDirChecked;
        private bool cbMailingListChecked;
        private bool cbEcpDismissalChecked;
        private bool cbStudentZipCodesChecked;

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool CbEmergencyChecked
        {
            get { return cbEmergencyChecked; }
            set
            {
                cbEmergencyChecked = value;
                OnPropertyChanged(nameof(CbEmergencyChecked));
            }
        }

        public bool CbAllergiesChecked
        {
            get { return cbAllergiesChecked; }
            set
            {
                cbAllergiesChecked = value;
                OnPropertyChanged(nameof(CbAllergiesChecked));
            }
        }

        public bool CbPickupChecked
        {
            get { return cbPickupChecked; }
            set
            {
                cbPickupChecked = value;
                OnPropertyChanged(nameof(CbPickupChecked));
            }
        }

        public bool CbAgesChecked
        {
            get { return cbAgesChecked; }
            set
            {
                cbAgesChecked = value;
                OnPropertyChanged(nameof(CbAgesChecked));
            }
        }

        public bool CbParentDirChecked
        {
            get { return cbParentDirChecked; }
            set
            {
                cbParentDirChecked = value;
                OnPropertyChanged(nameof(CbParentDirChecked));
            }
        }

        public bool CbMailingListChecked
        {
            get { return cbMailingListChecked; }
            set
            {
                cbMailingListChecked = value;
                OnPropertyChanged(nameof(CbMailingListChecked));
            }
        }

        public bool CbEcpDismissalChecked
        {
            get { return cbEcpDismissalChecked; }
            set
            {
                cbEcpDismissalChecked = value;
                OnPropertyChanged(nameof(CbEcpDismissalChecked));
            }
        }

        public bool CbStudentZipCodesChecked
        {
            get { return cbStudentZipCodesChecked; }
            set
            {
                cbStudentZipCodesChecked = value;
                OnPropertyChanged(nameof(CbStudentZipCodesChecked));
            }
        }

        public MainWindowViewModel()
        {
            SelectedDate = DateTime.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

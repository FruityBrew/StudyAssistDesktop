using System;
using System.ComponentModel;

namespace StudyAssist.ViewModel
{
    public abstract class XBaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;

        protected void RaisePropertyChanged(Object sender, String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System;
using System.ComponentModel;

namespace StudyAssist.ViewModel
{
    /// <summary>
    /// Базовый функционал для вьюмодели.
    /// </summary>
    public abstract class XBaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = null;

        /// <summary>
        /// Возбуждает событие изменения свойства.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="propertyName">Имя свойства.</param>
        protected void RaisePropertyChanged(Object sender, String propertyName)
        {
            PropertyChanged?.Invoke(
                this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

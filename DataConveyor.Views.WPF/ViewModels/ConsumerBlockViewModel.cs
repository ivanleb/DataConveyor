using System;
using System.ComponentModel;

namespace DataConveyor.Views.WPF
{
    public class ConsumerBlockViewModel<T> : INotifyPropertyChanged
    {
        private ConsumerBlock<T> _consumerBlock;
        public String Name
        {
            get => _consumerBlock.Name;
            set
            {
                _consumerBlock.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public String Id => _consumerBlock.Id.ToString();
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}

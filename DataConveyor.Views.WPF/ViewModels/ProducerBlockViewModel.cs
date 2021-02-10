using System;
using System.ComponentModel;

namespace DataConveyor.Views.WPF
{
    public class ProducerBlockViewModel<T> : INotifyPropertyChanged
    {
        private ProducerBlock<T> _producerBlock;
        public String Name 
        {
            get => _producerBlock.Name;
            set 
            {
                _producerBlock.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public String Id => _producerBlock.Id.ToString();

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

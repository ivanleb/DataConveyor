using System;
using System.ComponentModel;

namespace DataConveyor.Views.WPF
{
    public class ProcessingBlockViewModel<Input, Output> : INotifyPropertyChanged
    {
        private ProcessingBlock<Input, Output> _processingBlock;
        public String Name
        {
            get => _processingBlock.Name;
            set
            {
                _processingBlock.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public String Id => _processingBlock.Id.ToString();

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

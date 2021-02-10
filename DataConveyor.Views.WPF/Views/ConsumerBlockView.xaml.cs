using System.Windows.Controls;

namespace DataConveyor.Views.WPF.Views
{
    public partial class ConsumerBlockView : UserControl
    {
        public ConsumerBlockView()
        {
            InitializeComponent();
        }

        public void SetDataContext<T>(ConsumerBlockViewModel<T> consumerBlock)
        {
            DataContext = consumerBlock;
        }
    }
}

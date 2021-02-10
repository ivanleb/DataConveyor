using System.Windows.Controls;

namespace DataConveyor.Views.WPF.Views
{
    public partial class ProducerBlockView : UserControl
    {
        public ProducerBlockView()
        {
            InitializeComponent();
        }

        public void SetDataContext<T>(ProducerBlockViewModel<T> producerBlock)
        {
            DataContext = producerBlock;
        }
    }
}

using System.Windows.Controls;

namespace DataConveyor.Views.WPF.Views
{
    public partial class ProcessingBlockView : UserControl
    {
        public ProcessingBlockView()
        {
            InitializeComponent();
        }

        public void SetDataContext<Input, Output>(ProcessingBlockViewModel<Input, Output> processingBlock)
        {
            DataContext = processingBlock;
        }
    }
}

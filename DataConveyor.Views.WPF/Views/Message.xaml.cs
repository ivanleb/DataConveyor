using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ReactiveUI;
using System;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Controls.Primitives;
using System.Reactive.Linq;
using System.Windows.Markup;
using DataConveyor.Views.WPF.ViewModels;
using DataConveyor.Views.WPF;
using DataConveyor.Views.WPF.Enums;
using DataConveyor.Views.WPF.Extensions;

namespace DataConveyor.Views.WPF.Views
{
    /// <summary>
    /// Interaction logic for ViewError.xaml
    /// </summary>
    public partial class Message : UserControl, IViewFor<MessageViewModel>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(MessageViewModel), typeof(Message), new PropertyMetadata(null));

        public MessageViewModel ViewModel
        {
            get { return (MessageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MessageViewModel)value; }
        }
        #endregion ViewModel
        public Message()
        {
            InitializeComponent();
            SetupBinding();
            SetupSubscriptions();
        }

        #region SetupBinding
        private void SetupBinding()
        {
            this.WhenActivated(disposable =>
            {
                this.OneWayBind(this.ViewModel, x => x.Text, x => x.TextBlockElement.Text).DisposeWith(disposable);
            });
        }
        #endregion SetupBinding

        #region Setup Subscriptions

        private void SetupSubscriptions()
        {
            this.WhenActivated(disposable =>
            {
                this.WhenAnyValue(x => x.ViewModel.TypeMessage).Where(value=>value!=TypeMessage.NotCorrect).Subscribe(value => UpdateIcon(value)).DisposeWith(disposable);
            });
        }
        private void UpdateIcon(TypeMessage type)
        {
            this.RectangleElement.Fill = Application.Current.Resources["Icon" + type.Name()] as DrawingBrush;
        }
        #endregion Setup Subscriptions
    }
}

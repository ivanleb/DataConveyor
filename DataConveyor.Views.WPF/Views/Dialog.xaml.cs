using ReactiveUI;
using DataConveyor.Views.WPF.Enums;
using DataConveyor.Views.WPF;
using DataConveyor.Views.WPF.ViewModels;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Forms;
using DataConveyor.Views.WPF.Extensions;

namespace DataConveyor.Views.WPF.Views
{
    /// <summary>
    /// Interaction logic for ViewDialog.xaml
    /// </summary>
    public partial class Dialog : System.Windows.Controls.UserControl, IViewFor<DialogViewModel>
    {
        #region ViewModel
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(DialogViewModel), typeof(Dialog), new PropertyMetadata(null));

        public DialogViewModel ViewModel
        {
            get { return (DialogViewModel)GetValue(ViewModelProperty) ?? new DialogViewModel(); }
            set { SetValue(ViewModelProperty, value); }
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (DialogViewModel)value; }
        }
        #endregion ViewModel
        public Dialog()
        {
            InitializeComponent();
            SetupBinding();
            SetupSubcriptions();
        }
        private void SetupBinding()
        {
            this.WhenActivated(disposable =>
            {
                this.Bind(this.ViewModel, x=>x.Visibility, x=>x.Visibility).DisposeWith(disposable);
            });
        }
        private void SetupSubcriptions()
        {
            this.WhenActivated(disposable =>
            {
                this.WhenAnyValue(x => x.Visibility).Where(x => x == Visibility.Visible).Subscribe(_ => Show()).DisposeWith(disposable);
            });
        }
        private void Show()
        {
            GetAction().Invoke();
            this.Visibility = Visibility.Collapsed;
        }

        private Action GetAction()
        {
          return ViewModel.Type switch
          {
              DialogType.MessageBox => ShowMessageBox,
              DialogType.SaveFileDialog => ShowSaveFileDialog,
              DialogType.OpenFileDialog => ShowOpenFileDialog,
              DialogType.noCorrect => throw new NotImplementedException(),
              _ => throw new NotImplementedException()
          };
        }

        private void ShowMessageBox()
        {
            ViewModel.Result = System.Windows.MessageBox.Show(ViewModel.MessageBoxText, ViewModel.Title, ViewModel.MessageBoxButtons).ToDialogResult();
        }
        private void ShowOpenFileDialog()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = ViewModel.Title;
            dlg.FileName = ViewModel.FileName;
            dlg.Filter = ViewModel.FileDialogFilter;

            ViewModel.Result = dlg.ShowDialog().ToDialogResult();
            ViewModel.FileName = dlg.FileName;

        }
        private void ShowSaveFileDialog()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = ViewModel.Title;
            dlg.FileName = ViewModel.FileName;
            dlg.Filter = ViewModel.FileDialogFilter;

            ViewModel.Result = dlg.ShowDialog().ToDialogResult();
            ViewModel.FileName = dlg.FileName;
        }
    }
}

using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using DataConveyor.Views.WPF.Enums;

namespace DataConveyor.Views.WPF.ViewModels 
{ 
    public class MessageViewModel : ReactiveObject
    {
        public TypeMessage TypeMessage { get; set; }
        [Reactive] public string Text { get; set; }
        public MessageViewModel(TypeMessage typeMessage, string text)
        {
            TypeMessage = typeMessage;
            Text = text;
        }
    }
}

using ReactiveUI;
using DataConveyor.Views.WPF.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataConveyor.Views.WPF.Extensions
{
    public static class ReactiveCommandExtension
    {
        public static IDisposable ExecuteWithSubscribe<TParam, TResult>(this ReactiveCommand<TParam, TResult> reactiveCommand, TParam parameter = default)
        {
            return reactiveCommand.Execute(parameter).Subscribe();
        }
    }
}

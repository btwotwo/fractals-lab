using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fractal.ViewModels
{
    public class ViewModelBase : ReactiveObject, IActivatableViewModel
    {
        public ViewModelActivator Activator { get; } = new ViewModelActivator();

    }
}

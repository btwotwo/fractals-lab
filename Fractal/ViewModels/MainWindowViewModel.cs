using Avalonia.Media;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fractal.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public IImage? Fractal { get; set; } = new FractalSet().Generate(new(-1f, -0.7f), new(0.7f, 1f));
    }
}

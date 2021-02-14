using System;
using System.Collections.Generic;
using System.Text;

namespace Fractal.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";

        public int MinX { get; set; }
        public int MinY { get; set; }

        public int MaxX { get; set; }
        public int MaxY { get; set; }
    }
}

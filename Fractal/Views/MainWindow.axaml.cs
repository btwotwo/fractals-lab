using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Fractal.ViewModels;
using ReactiveUI;

namespace Fractal.Views
{
    public class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public TextBox MinX => this.FindControl<TextBox>("MinX");
        public TextBox MinY => this.FindControl<TextBox>("MinY");
        public TextBox MaxX => this.FindControl<TextBox>("MaxX");
        public TextBox MaxY => this.FindControl<TextBox>("MaxY");

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            this.WhenActivated(d =>
            {
                this.Bind(ViewModel, x => x.MinX, x => x.MinX);
                this.Bind(ViewModel, x => x.MinY, x => x.MinY);
                this.Bind(ViewModel, x => x.MaxX, x => x.MaxX);
                this.Bind(ViewModel, x => x.MaxY, x => MaxY);
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

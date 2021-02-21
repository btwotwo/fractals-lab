using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using Fractal.ViewModels;
using ReactiveUI;

namespace Fractal.Views
{
    public class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public NumericUpDown MinX => this.FindControl<NumericUpDown>("MinX");
        public NumericUpDown MinY => this.FindControl<NumericUpDown>("MinY");
        public NumericUpDown MaxX => this.FindControl<NumericUpDown>("MaxX");
        public NumericUpDown MaxY => this.FindControl<NumericUpDown>("MaxY");

        public Image Image => this.FindControl<Image>("DisplayContainer");

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            this.WhenActivated(d =>
            {
                this.Bind(ViewModel, x => x.MinX, view => view.MinX.Value);
                this.Bind(ViewModel, x => x.MinY, view => view.MinY.Value);
                this.Bind(ViewModel, x => x.MaxX, view => view.MaxX.Value);
                this.Bind(ViewModel, x => x.MaxY, view => view.MaxY.Value);

                this.Bind(ViewModel, x => x.Fractal, view => view.Image.Source);
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

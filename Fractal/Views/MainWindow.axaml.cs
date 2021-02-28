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
        public Image Image => this.FindControl<Image>("DisplayContainer");
        public ContentControl ContentControl => this.FindControl<ContentControl>("Content");


        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            this.WhenActivated(d =>
            {
                this.Bind(ViewModel, x => x.Fractal, view => view.Image.Source);
                //this.BindCommand(ViewModel, x `=> x.DoZoom, view => view.ContentControl.Tapped);
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KolaWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Grid DrawCircles(double x, double y, params double[] radius)
        {
            var grid = new Grid();
            grid.RenderTransform = new RotateTransform();
            grid.RenderTransformOrigin = new Point(x / Width, y / Height);
            root.Children.Add(grid);
            for (int i = 0; i < radius.Length; i++)
            {
                Path circle = new Path();
                circle.Data = new EllipseGeometry() { Center = new Point(x, y), RadiusX = radius[i], RadiusY = radius[i] };
                circle.Stroke = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                circle.StrokeThickness = 1d;
                circle.RenderTransform = new RotateTransform();
                circle.RenderTransformOrigin = new Point(0.5, 0.5);

                grid.Children.Add(circle);
                x += radius[i];
            }
            return grid;
        }

        private void RotateRoot(Grid obj, double angle)
        {
            var rt = (obj.RenderTransform as RotateTransform);
            rt.Angle = angle;
        }

        private void ClearAll()
        {
            root.Children.Clear();
        }

        private async Task PrzerysujWszystko(double x, double y, double r1, double r2, double r3)
        {
            ClearAll();
            for (int d = 0; d <= 360; d += 8)
            {
                for (int d2 = 0; d2 < 360; d2 += 8)
                {
                    var a = DrawCircles(x, y, r1, r2, r3);
                    RotateRoot(a, d);
                    (a.Children[1].RenderTransform as RotateTransform).Angle = d2;
                    (a.Children[2].RenderTransform as RotateTransform).Angle = d2;
                    await Task.Delay(50);
                }
                await Task.Delay(100);
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await PrzerysujWszystko(Width / 2, Height / 2, 100, 60, 300);
        }


    }
}

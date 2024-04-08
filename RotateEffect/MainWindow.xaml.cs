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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RotateEffect
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

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(1000);
            Animation();
        }

        private void Animation()
        {
            var anim = Resources["animBorder"] as Storyboard;
            anim?.Stop();
            anim?.Begin();
        }

        private void TabMenuLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Button;
            if (button == null || !_isAnimCompleted) return;
            Task.Run(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    switch (button.Name)
                    {
                        case "button":
                            MoveToPosition(new Point(0, 0));
                            break;
                        case "button1":
                            MoveToPosition(new Point(0, 51 * 1));
                            break;
                        case "button2":
                            MoveToPosition(new Point(0, 51 * 2));
                            break;
                        case "button3":
                            MoveToPosition(new Point(0, 51 * 3));
                            break;
                        case "button4":
                            MoveToPosition(new Point(0, 51 * 4));
                            break;
                        case "button5":
                            MoveToPosition(new Point(0, 51 * 5));
                            break;
                        case "button6":
                            MoveToPosition(new Point(0, 51 * 6));
                            break;
                    }
                });
            });
        }

        private bool _isAnimCompleted = true;
        private void MoveToPosition(Point pos)
        {
            _isAnimCompleted = false;
            var transformGroup = animatedBorder.RenderTransform as TransformGroup;
            var translate = transformGroup.Children[3] as TranslateTransform;

            DoubleAnimation anim = new DoubleAnimation();
            anim.To = pos.Y;
            anim.Duration = new TimeSpan(0, 0, 0, 0, 350);
            anim.Completed += (s, args) => _isAnimCompleted = true;
            translate.BeginAnimation(TranslateTransform.YProperty, anim);
        }

        private void animation_Click(object sender, RoutedEventArgs e)
        {
            var anim = Resources["animBorder"] as Storyboard;
            anim?.Stop();
            anim?.Begin();
        }
    }
}

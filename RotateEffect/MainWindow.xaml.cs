using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace RotateEffect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, double> buttonPosition = new Dictionary<string, double>()
        {
            { "HomeButton", 0 },
            { "InvestButton", 51 * 1},
            { "CareerButton", 51 * 2 },
            { "WalletButton", 51 * 3 },
            { "NewButton", 51 * 4 },
            { "SettingButton", 51 * 5 },
            { "SupportButton", 51 * 6 }
        };

        private Dictionary<string, double> buttonKeyTimes = new Dictionary<string, double>()
        {
            { "HomeButton", 1.6 },
            { "InvestButton", 1.6 },
            { "CareerButton", 1.8 },
            { "WalletButton", 2 },
            { "NewButton", 2.2 },
            { "SettingButton", 2.4 },
            { "SupportButton", 2.6 }
        };

        private bool _isAnimCompleted = true;
        private Storyboard animStoryBoard;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            animStoryBoard = Resources["animBorder"] as Storyboard;
            await Task.Delay(1000);
            animStoryBoard?.Stop();
            animStoryBoard?.Begin();
        }

        private void TabMenuLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Button;
            if (button == null || !_isAnimCompleted) return;

            if (buttonPosition.TryGetValue(button.Tag.ToString(), out double position))
            {
                MoveToPosition(new Point(0, position));
            }
        }

        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && buttonKeyTimes.TryGetValue(button.Tag.ToString(), out double toKeyTime))
            {
                MenuButtonAnimation(toKeyTime, button);
            }
        }

        #region Animation
        private void animation_Click(object sender, RoutedEventArgs e)
        {
            animStoryBoard?.Stop();
            animStoryBoard?.Begin();
        }

        private void MenuButtonAnimation(double toKeyTime, Button button)
        {
            // Create the animation
            DoubleAnimationUsingKeyFrames animation = new DoubleAnimationUsingKeyFrames();

            // Define key frames
            EasingDoubleKeyFrame keyFrame1 = new EasingDoubleKeyFrame();
            keyFrame1.KeyTime = TimeSpan.FromSeconds(0);
            keyFrame1.Value = 0;

            EasingDoubleKeyFrame keyFrame2 = new EasingDoubleKeyFrame();
            keyFrame2.KeyTime = TimeSpan.FromSeconds(toKeyTime);
            keyFrame2.Value = 190;
            keyFrame2.EasingFunction = new QuinticEase() { EasingMode = EasingMode.EaseOut };

            // Add key frames to the animation
            animation.KeyFrames.Add(keyFrame1);
            animation.KeyFrames.Add(keyFrame2);

            // Apply animation to a UIElement
            button.RenderTransform = new TranslateTransform();
            Storyboard.SetTarget(animation, button);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));

            // Create a storyboard and add the animation
            animStoryBoard.Children.Add(animation);
        }

        private void MoveToPosition(Point pos)
        {
            _isAnimCompleted = false;
            var translate = animatedBorder.RenderTransform as TranslateTransform;

            DoubleAnimation anim = new DoubleAnimation();
            anim.To = pos.Y;
            anim.Duration = new TimeSpan(0, 0, 0, 0, 350);
            anim.Completed += (s, args) => _isAnimCompleted = true;
            translate.BeginAnimation(TranslateTransform.YProperty, anim);
        }

        #endregion
    }
}

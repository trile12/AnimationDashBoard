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

namespace RotateEffect
{
    /// <summary>
    /// Interaction logic for CustomCard.xaml
    /// </summary>
    public partial class CustomCard : Border
    {
        public CustomCard()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(CustomCard), new PropertyMetadata(string.Empty));


        public string ImageData
        {
            get { return (string)GetValue(ImageDataProperty); }
            set { SetValue(ImageDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageDataProperty =
            DependencyProperty.Register("ImageData", typeof(string), typeof(CustomCard), new PropertyMetadata(string.Empty));


        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Description.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(CustomCard), new PropertyMetadata(string.Empty));

    }
}

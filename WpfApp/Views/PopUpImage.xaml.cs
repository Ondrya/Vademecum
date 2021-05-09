using System.Windows;
using WpfApp.ViewModels;

namespace WpfApp.Views
{
    /// <summary>
    /// Interaction logic for PopUpImage.xaml
    /// </summary>
    public partial class PopUpImage : Window
    {
        public PopUpImage(byte[] rawImage)
        {
            InitializeComponent();
            ((PopUpImageViewModel)(this.DataContext)).RawImage = rawImage;
        }
    }
}

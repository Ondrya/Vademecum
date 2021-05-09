namespace WpfApp.ViewModels
{
    public class PopUpImageViewModel : BaseViewModel
    {
        public PopUpImageViewModel()
        {

        }

        
        private byte[] _rawImage;
        public byte[] RawImage
        {
            get => _rawImage;
            set
            {
                _rawImage = value;
                OnPropertyChanged(nameof(RawImage));
            }
        }
    }
}

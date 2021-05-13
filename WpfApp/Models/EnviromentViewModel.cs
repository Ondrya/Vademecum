using DataLayer;

namespace WpfApp.Models
{
    public class EnviromentViewModel : DataLayer.Enviroment
    {
        public EnviromentViewModel()
        {

        }
        public EnviromentViewModel(Enviroment envi, bool isSelected)
        {
            id_envi = envi.id_envi;
            name_envi = envi.name_envi;
            spec_envi = envi.spec_envi;
            IsSelected = isSelected;
        }

        public int id_envi { get; set; }
        public string name_envi { get; set; }
        public string spec_envi { get; set; }
        public bool IsSelected { get; set; }
    }
}

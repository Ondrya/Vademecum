using DataLayer;
using System.Collections.ObjectModel;
using WpfApp.Commands;

namespace WpfApp.ViewModels
{
    public interface IParamViewModel
    {
        RelayCommand AddCommand { get; }
        RelayCommand CreateCommand { get; }
        //ObservableCollection<Function> DataCollection { get; set; }
        RelayCommand DeleteCommand { get; }
        //Function NewItem { get; set; }
        //Function Selected { get; set; }
        RelayCommand UpdateCommand { get; }
        void Fill();
        bool ExistInDb();
        // {return Selected != null && Selected?.id_func > 0;}
        bool CheckItem();
        //{
        //    if (string.IsNullOrWhiteSpace(Selected?.name_func) || Selected?.id_func > 0) return false;
        //    return true;
        //}
}
}
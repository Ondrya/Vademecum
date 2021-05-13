using DataLayer;
namespace WpfApp.Models
{
    public class LiteratureViewModel : DataLayer.Literature
    {
        public LiteratureViewModel()
        {

        }
        public LiteratureViewModel(Literature item, bool isSelected)
        {
            id_lit = item.id_lit;
            lit_type = item.lit_type;
            lit_name = item.lit_name;
            lit_author = item.lit_author;
            lit_date = item.lit_date;
            lit_publish = item.lit_publish;
            lit_web = item.lit_web;
            file_name = item.file_name;
            lit_file = item.lit_file;
            IsSelected = isSelected;
        }

        //public int id_lit { get; set; }

        //[StringLength(100)]
        //public string lit_type { get; set; }

        //[StringLength(500)]
        //public string lit_name { get; set; }

        //[StringLength(200)]
        //public string lit_author { get; set; }

        //public int? lit_date { get; set; }

        //[StringLength(200)]
        //public string lit_publish { get; set; }

        //public string lit_web { get; set; }

        //[StringLength(200)]
        //public string file_name { get; set; }

        //public byte[] lit_file { get; set; }
        public bool IsSelected { get; set; }
    }
}

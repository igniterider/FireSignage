
namespace FireSignage.Models
{
	public class PreMadeSigns : BaseViewModel
	{

        private bool isVisible = false;


        public string Displaytext { get; set; }

        public string Displaytext2 { get; set; }
        
        public bool? Changename { get; set; }

        public string BackgroundColors { get; set; }
        
        public string Textcolor { get; set; }

        
        public string Textcolor2 { get; set; }

        public bool IsFavorite { get; set; }
        
        public string Business { get; set; }

        
        public string Category { get; set; }

        // public ObservableCollection<PreMadeSigns> CategoryGroup { get; set; }


        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                this.OnPropertyChanged("IsVisible");

            }
        }


    }


    public class CategoriesList : List<PreMadeSigns>
    {

        public string CategoryName { get; set; }
        public CategoriesList(string categoryname, List<PreMadeSigns> premadeSigns) : base(premadeSigns)
        {
            CategoryName = categoryname;

        }


    }



}
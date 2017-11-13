using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace shSpeak
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            DetailPage Detail = new DetailPage();
            this.Master = new MenuPage(this, Detail);
            this.Detail = new NavigationPage(Detail)// new DetailPage())
            {
                BarBackgroundColor = Color.FromHex("#63ACD5")
            };

            NavigationPage.SetHasNavigationBar(this, false);
        }      

    }
}
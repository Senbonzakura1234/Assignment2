using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using T1807EHello.Entity;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1807EHello.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home : Page
    {
        public Visibility LoginSuccessStatus;
        public Visibility LoginFailStatus;
        public Home()
        {
            InitializeComponent();
            var service = new MemberServiceImp();
            var response = service.LoginWithToken();
            if (response != null)
            {
                LoginSuccessStatus = response.email != null ? Visibility.Visible : Visibility.Collapsed;
                LoginFailStatus = response.email != null ? Visibility.Collapsed : Visibility.Visible;
                if (response.email != null)
                {
                    LoginSuccessText.Text = "Hi! " + response.lastName + " " + response.firstName;
                }
            }
            else
            {
                LoginSuccessStatus = Visibility.Collapsed;
                LoginFailStatus = Visibility.Visible;
            }
                
            
            
        }

        private void HyperlinkButton_Click_Login(Hyperlink sender, HyperlinkClickEventArgs args)
        {
            Frame.Navigate(typeof(Login));
        }

        private void HyperlinkButton_Click_Register(Hyperlink sender, HyperlinkClickEventArgs args)
        {
            Frame.Navigate(typeof(Register));
        }
    }
}

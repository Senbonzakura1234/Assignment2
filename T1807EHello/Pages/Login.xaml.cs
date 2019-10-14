using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using T1807EHello.Entity;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1807EHello.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public sealed partial class Login : Page
    {
        private MemberServiceImp _service;
        public bool Test;
        public Login()
        {
            InitializeComponent();

            //this._service = new MemberServiceImp();
            //var token = _service.ReadTokenFile("token.txt");
            //Debug.WriteLine(token);

            //var responseMember = _service.GetInformation(token);
            //if (responseMember == null) return;
            //Debug.WriteLine(responseMember.email);
        }
        private void LoginTrigger(object sender, RoutedEventArgs e)
        {
            _service = new MemberServiceImp();
            var token = _service.Login(Email.Text, Password.Password);
            if (token == null) return;
            Debug.WriteLine(token);
            Frame.Navigate(typeof(Home));

            //Email = "dungpath1805040@fpt.edu.vn";
            //Password = "12345";

            ////Email = "anhdungpham090@gmail.com";
            //Password = "123456";
        }

        private void ResetForm(object sender, RoutedEventArgs e)
        {
            this.Email.Text = string.Empty;
            this.Password.Password = string.Empty;
        }
    }
}

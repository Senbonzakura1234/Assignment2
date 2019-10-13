using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using T1807EHello.Entity;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1807EHello.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Profile : Page
    {
        
        public Profile()
        {
            InitializeComponent();
            var service = new MemberServiceImp();
            var response = service.LoginWithToken();
            
            Avatar.ImageSource = new BitmapImage(
                new Uri(response.avatar, UriKind.Absolute));
            FullName.Text = response.lastName + " " + response.firstName;
            Description.Text = response.introduction;
            Email.Text = response.email;
            Debug.WriteLine(response.email);
            Address.Text = response.address;
            Phone.Text = response.phone;
            Gender.Text = response.gender == "1" ? "Male" : "Female";

            var parsedDate = DateTime.Parse(response.birthday);
            Birthday.Text = parsedDate.ToString("dd-MM-yyyy");


        }
    }
}

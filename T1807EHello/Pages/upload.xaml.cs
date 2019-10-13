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
    public sealed partial class upload : Page
    {
        public upload()
        {
            InitializeComponent();
        }
        private void UploadTrigger(object sender, RoutedEventArgs e)
        {
            var song = new Song
            {
                name = Name.Text,
                description = Description.Text,
                singer = Singer.Text,
                author = Author.Text,
                thumbnail = Thumbnail.Text,
                link = Link.Text
            };
            Debug.WriteLine(song.name);
            Debug.WriteLine(song.description);
            Debug.WriteLine(song.singer);
            Debug.WriteLine(song.author);
            Debug.WriteLine(song.thumbnail);
            Debug.WriteLine(song.link);

            var songManager = new SongManagerImp();
            var songValidateData = songManager.Validation(song);
            VName.Text = songValidateData.name;
            VThumbnail.Text = songValidateData.thumbnail;
            VLink.Text = songValidateData.link;

            if (songValidateData.valid == false)
            {
                DialogService.ShowToast(string.Empty, "Form invalid");
            }
            else
            {
                var responseContent = songManager.Upload(song);
                Debug.WriteLine(responseContent.name);
            }
        }

        private void ResetForm(object sender, RoutedEventArgs e)
        {
            Name.Text = string.Empty;
            Description.Text = string.Empty;
            Singer.Text = string.Empty;
            Author.Text = string.Empty;
            Thumbnail.Text = string.Empty;
            Link.Text = string.Empty;
        }
    }
}

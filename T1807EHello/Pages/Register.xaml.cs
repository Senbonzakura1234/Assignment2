using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using T1807EHello.Entity;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1807EHello.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register : Page
    {
        private MemberServiceImp _service;
        private const string ImageUploadUri = "https://2-dot-backup-server-003.appspot.com/get-upload-token";
        private StorageFile _photo;
        private string _urlToPost;
        public Register()
        {
            InitializeComponent();
        }

        private void RegisterTrigger(object sender, RoutedEventArgs e)
        {
            if (Gender.SelectedItem != null)
            {
                var member = new Member
                {
                    firstName = Firstname.Text,
                    lastName = Lastname.Text,
                    email = Email.Text,
                    address = Address.Text,
                    avatar = _urlToPost,
                    birthday = Birthday.Date.ToString("yyyy-mm-dd"),
                    introduction = Introduction.Text,
                    phone = Phone.Text,
                    password = Password.Password,
                    gender = Gender.SelectedItem.ToString() == "Male" ? "1" : "0"
                };


                var rePass = RePassword.Password;
                if (!string.IsNullOrWhiteSpace(member.password) && !string.IsNullOrWhiteSpace(member.email) &&
                    !string.IsNullOrWhiteSpace(member.firstName) && !string.IsNullOrWhiteSpace(member.lastName) &&
                    !string.IsNullOrWhiteSpace(member.phone) && rePass == member.password)
                {
                    _service = new MemberServiceImp();
                    var responseMember = _service.Register(member);
                    if (responseMember != null)
                    {
                        Debug.WriteLine(responseMember.email);
                        Frame.Navigate(typeof(Home));
                    }
                    else
                    {
                        DialogService.ShowToast(string.Empty, "Register Fail");
                    }
                }
                else
                {
                    DialogService.ShowToast(string.Empty, "Form invalid");
                }
            }
            else
            {
                DialogService.ShowToast(string.Empty, "Form invalid");
            }
        }

        private async void AddAvatarTrigger(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();
            var uploadUrl = client.GetAsync(ImageUploadUri).Result.Content.ReadAsStringAsync().Result;
            Debug.WriteLine("Upload url: " + uploadUrl);

            var captureUi = new CameraCaptureUI();
            captureUi.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUi.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            _photo = await captureUi.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (_photo == null)
            {
                return;
            }

            HttpUploadFile(uploadUrl, "myFile", "image/png");
        }

        public async void HttpUploadFile(string url, string paramName, string contentType)
        {
            var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            var boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            var wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";

            var rs = await wr.GetRequestStreamAsync();
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            var header =
                $"Content-Disposition: form-data; name=\"{paramName}\"; filename=\"{"path_file"}\"\r\nContent-Type: {contentType}\r\n\r\n";
            var headerbytes = Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            // write file.
            var fileStream = await _photo.OpenStreamForReadAsync();
            var buffer = new byte[4096];
            var bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }

            var trailer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);

            WebResponse resp = null;
            try
            {
                resp = await wr.GetResponseAsync();
                var stream2 = resp.GetResponseStream();
                var reader2 = new StreamReader(stream2 ?? throw new InvalidOperationException());

                var imageUrl = reader2.ReadToEnd();
                AvatarImage.Source = new BitmapImage(new Uri(imageUrl, UriKind.Absolute));
                AvatarImage.Visibility = Visibility.Visible;
                //AvatarUrl.Text = imageUrl;
                Debug.WriteLine(imageUrl);
                _urlToPost = imageUrl;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error uploading file", ex.StackTrace);
                Debug.WriteLine("Error uploading file", ex.InnerException);
                if (resp != null)
                {
                    resp = null;
                }
            }
            finally
            {
                wr = null;
            }
        }

        private void ResetForm(object sender, RoutedEventArgs e)
        {
            Email.Text = string.Empty;
            Password.Password = string.Empty;
            RePassword.Password = string.Empty;
            Lastname.Text = string.Empty;
            Firstname.Text = string.Empty;
            Address.Text = string.Empty;
            Introduction.Text = string.Empty;
            Phone.Text = string.Empty;
            _urlToPost = null;
            AvatarImage.Visibility = Visibility.Collapsed;
        }
    }
}

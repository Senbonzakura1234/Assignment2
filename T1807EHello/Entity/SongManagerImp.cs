using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Data;
using Newtonsoft.Json;

namespace T1807EHello.Entity
{
    internal class SongManagerImp : ISongManager
    {
        private const string SongApi = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs";

        public Song Upload(Song member)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + ReadTokenFile());
            HttpContent content = new StringContent(JsonConvert.SerializeObject(member), Encoding.UTF8,
                "application/json");
            var responseContent = client.PostAsync(SongApi, content).Result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Song>(responseContent);
        }

        public ValidateData Validation(Song song)
        {
            var songValidateData = new ValidateData
            {

                name = !string.IsNullOrWhiteSpace(song.name)
                    ? song.name.Length <= 50 ? "Name is Valid" : "Name must less then 50 characters"
                    : "Name is required",
                thumbnail = !string.IsNullOrWhiteSpace(song.thumbnail) ? "Thumbnail is valid" : "Thumbnail is required",
                link = !string.IsNullOrWhiteSpace(song.link)
                    ? song.link.EndsWith(".mp3") ? "Link is valid" : "Link must end with .mp3"
                    : "Link is required",
                valid = song.name != null &&
                        song.thumbnail != null &&
                        song.link != null && song.name.Length <= 50 && song.link.EndsWith(".mp3")
            };


            return songValidateData;
        }

        public string GetDataFromServer(string songListUrl)
        {
            var client = new HttpClient();
            var responseContent = client.GetAsync(songListUrl).Result.Content.ReadAsStringAsync().Result;
            return responseContent;
        }

        public ObservableCollection<Song> LoadSongs()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + ReadTokenFile());
            var responseContent = client.GetAsync(SongApi).Result.Content.ReadAsStringAsync().Result;
            var songs = JsonConvert.DeserializeObject<ObservableCollection<Song>>(responseContent);
            
            return songs;
        }

        private static string ReadTokenFile()
        {
            const string fileName = "token.txt";
            var storageFolder = ApplicationData.Current.LocalFolder;
            var sampleFile = storageFolder.GetFileAsync(fileName).GetAwaiter().GetResult();
            var token = FileIO.ReadTextAsync(sampleFile).GetAwaiter().GetResult();
            return token;
        }
    }
}

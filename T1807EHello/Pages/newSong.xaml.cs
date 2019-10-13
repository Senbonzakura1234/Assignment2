using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using T1807EHello.Entity;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1807EHello.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewSong : Page
    {
        private ObservableCollection<Song> ListSongs { get; set; }
        private readonly ISongManager _songManager;
        private bool _isPlaying;
        private int _currentIndex;
        
        public NewSong()
        {
            
            //this._listSongs = new ObservableCollection<Song>();
            //var songManager = new SongManagerImp();
            //var responseContent = songManager.GetDataFromServer(SongListUrl);
            //var songs = JsonConvert.DeserializeObject<List<Song>>(responseContent);
            //foreach (var item in songs)
            //{
            //    this._listSongs.Add(new Song()
            //    {
            //        name = item.name,
            //        singer = item.singer,
            //        thumbnail = item.thumbnail,
            //        link = item.link,
            //    });
            //}

            InitializeComponent();
            _songManager = new SongManagerImp();
            LoadListSongs();
        }

        //private void Refresh(object sender, RoutedEventArgs e)
        //{
        //    songManager = new SongManagerImp();
        //    _listSongs = songManager.LoadSongs();
        //}

        private void LoadListSongs()
        {
            ListSongs = _songManager.LoadSongs();
            MyListSong.ItemsSource = ListSongs;
            _currentIndex = 0;
        }

        private void SelectSong(object sender, TappedRoutedEventArgs e)
        {
            var selectItem = sender as StackPanel;
            MyMediaPlayer.Pause();
            if (selectItem == null || !(selectItem.Tag is Song currentSong)) return;
            _currentIndex = ListSongs.IndexOf(currentSong);
            MyMediaPlayer.Source = new Uri(currentSong.link);
 
            Play();
        }

        private void StatusButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_isPlaying)
            {
                Pause();
            }
            else
            {
                Play();
            }
        }

        private void Play()
        {
            
            ControlLabel.Text = "Now Playing: " + ListSongs[_currentIndex].name;
            MyListSong.SelectedIndex = _currentIndex;
            MyMediaPlayer.Play();
            StatusButton.Icon = new SymbolIcon(Symbol.Pause);
            _isPlaying = true;
        }

        private void Pause()
        {
            ControlLabel.Text = "Pause";
            MyMediaPlayer.Pause();
            StatusButton.Icon = new SymbolIcon(Symbol.Play);
            _isPlaying = false;
        }

        private void PreviousButton_OnClick(object sender, RoutedEventArgs e)
        {
            _currentIndex--;
            if (_currentIndex < 0)
            {
                _currentIndex = ListSongs.Count - 1;
            }
            else if (_currentIndex >= ListSongs.Count)
            {
                _currentIndex = 0;
            }
            MyMediaPlayer.Source = new Uri(ListSongs[_currentIndex].link);

            Play();
        }

        private void NextButton_OnClick(object sender, RoutedEventArgs e)
        {
            _currentIndex++;
            if (_currentIndex >= ListSongs.Count || _currentIndex < 0)
            {
                _currentIndex = 0;
            }
            MyMediaPlayer.Source = new Uri(ListSongs[_currentIndex].link);
 
            Play();
        }


        //private void SeekToMediaPosition(object sender, RangeBaseValueChangedEventArgs rangeBaseValueChangedEventArgs)
        //{
        //    var sliderValue = (int)TimelineSlider.Value;
        //    var ts = new TimeSpan(0, 0, 0, sliderValue);
        //    MyMediaPlayer.Position = ts;
        //}
        private void Backward_OnClick(object sender, RoutedEventArgs e)
        {
            var ts = new TimeSpan(0, 0, 0, 5);
            MyMediaPlayer.Position -= ts;
        }

        private void Forward_OnClick(object sender, RoutedEventArgs e)
        {
            var ts = new TimeSpan(0, 0, 0, 5);
            MyMediaPlayer.Position += ts;
        }
    }
}

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using T1807EHello.Entity;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace T1807EHello.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class mySong : Page
    {
        private ObservableCollection<Song> ListSongs { get; set; }

        
        //private IMemberService _service;
        //private string memberID;
        private readonly ISongManager _songManager;
        private bool _isPlaying;
        private int _currentIndex;
        
        public mySong()
        {
            InitializeComponent();
            _songManager = new SongManagerImp();
            //_service = new MemberServiceImp();
            //memberID = _service.LoginWithToken().id.ToString();
            //Debug.WriteLine(memberID);
            LoadListSongs();
        }
        private void LoadListSongs()
        {
            ListSongs = _songManager.LoadSongs(1);
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


        public void MyMediaPlayer_OnMediaEnded(object sender, RoutedEventArgs e)
        {
            _currentIndex++;
            if (_currentIndex >= ListSongs.Count || _currentIndex < 0)
            {
                _currentIndex = 0;
            }
            MyMediaPlayer.Source = new Uri(ListSongs[_currentIndex].link);

            Play();
        }
    }
}

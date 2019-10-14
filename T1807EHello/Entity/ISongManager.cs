using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1807EHello.Entity
{
    internal interface ISongManager
    {

        Song Upload(Song member);

        ValidateData Validation(Song song);
        string GetDataFromServer(string songListUrl);
        ObservableCollection<Song> LoadSongs(int type);
    }
}

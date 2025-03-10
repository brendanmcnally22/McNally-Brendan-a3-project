namespace MohawkGame2D
{
    public class MusicManager
    {
        private string[] songPaths;
        private string[] songTitles;
        private Music[] musicTracks;
        private int currentSongIndex;

        public MusicManager(string[] paths, string[] titles)
        {
            songPaths = paths;
            songTitles = titles;
            currentSongIndex = 0;

            // Load the music files
            musicTracks = new Music[songPaths.Length];
            for (int i = 0; i < songPaths.Length; i++)
            {
                musicTracks[i] = Audio.LoadMusic(songPaths[i]);
            }
        }

        public void PlayMusic(int index)
        {
            // Stop any currently playing music
            Audio.Stop(musicTracks[currentSongIndex]);

            // Update the index and play
            currentSongIndex = index % musicTracks.Length;
            Audio.Play(musicTracks[currentSongIndex]);
        }

        public void NextSong() // Play the Next Song
        {
            PlayMusic(currentSongIndex + 1);
        }

        public string CurrentSongTitle // Get the Current Song Title
        {
            get { return songTitles[currentSongIndex]; }
        }
    }
}


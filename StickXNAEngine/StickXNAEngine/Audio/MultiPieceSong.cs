using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StickXNAEngine.Utility;

namespace StickXNAEngine.Audio {
    public class MultiPieceSong : IUpdating {
        private int i = -1;
        private List<Song> songs = new List<Song>();
        private bool play = false;

        public MultiPieceSong(params string[] songs) {
            foreach(string alpha in songs) {
                this.songs.Add(new Song(alpha));
            }
            PostProcessing.Add((IUpdating)this);
        }

        public void Update(Microsoft.Xna.Framework.GameTime gt) {
            if(!play) { i = -1; } else {
                if(i == -1) {
                    songs[0].Playing = true;
                    i = 0;
                } else {
                    if(!songs[i].Playing) {
                        songs[++i].Playing = true;
                    }
                }
                StickXNAEngine.Audio.Song.Repeat = (i == songs.Count - 1);
            }
        }

        public bool Play {
            get { return play; }
            set { play = value; }
        }
    }
}

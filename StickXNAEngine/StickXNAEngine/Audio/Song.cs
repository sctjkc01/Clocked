using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using XNAVanillaSong = Microsoft.Xna.Framework.Media.Song;
using StickXNAEngine.Utility;

namespace StickXNAEngine.Audio {
    public class Song : IRequireResource {
        private string resname;
        private XNAVanillaSong song;
        private static Song currsong = null;

        public Song(string resname) {
            this.resname = resname;
            PostProcessing.Add((IRequireResource)this);
        }

        public void LoadRes(Microsoft.Xna.Framework.Content.ContentManager cm) {
            song = cm.Load<XNAVanillaSong>(resname);
        }

        public bool Playing {
            get { return currsong == this && MediaPlayer.State == MediaState.Playing; }
            set {
                if(value) {
                    if(currsong == this) {
                        switch(MediaPlayer.State) {
                            case MediaState.Paused:
                                MediaPlayer.Resume();
                                break;
                            case MediaState.Playing:
                                break;
                            case MediaState.Stopped:
                                MediaPlayer.Play(song);
                                break;
                        }
                    } else {
                        MediaPlayer.Play(song);
                    }
                    currsong = this;
                } else {
                    if(currsong == this) MediaPlayer.Stop();
                }
            }
        }

        public static bool Repeat {
            get { return MediaPlayer.IsRepeating; }
            set { MediaPlayer.IsRepeating = value; }
        }

        public static Song CurrentSong {
            get { return currsong; }
        }

        public XNAVanillaSong InternalSong {
            get { return song; }
        }
    }
}

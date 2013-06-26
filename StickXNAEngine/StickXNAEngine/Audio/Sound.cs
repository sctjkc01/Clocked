using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using StickXNAEngine.Utility;

namespace StickXNAEngine.Audio {
    public class Sound : IRequireResource {
        private string resname;
        private SoundEffect effect;
        private List<SoundEffectInstance> insts = new List<SoundEffectInstance>();

        public Sound(string resname) {
            this.resname = resname;
        }

        public void LoadRes(Microsoft.Xna.Framework.Content.ContentManager cm) {
            effect = cm.Load<SoundEffect>(resname);
        }

        public void Play() {
            SoundEffectInstance newinst = effect.CreateInstance();
            newinst.Play();
        }

        public void ShutUp() {
            foreach(SoundEffectInstance alpha in insts) {
                alpha.Stop(true);
            }
        }
    }
}

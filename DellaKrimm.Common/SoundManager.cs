using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace DellaKrimm.Common
{
    public class SoundManager
    {
        private SoundManager()
        {
            this.soundEffects = new Dictionary<string, SoundEffect>();
        }

        private static SoundManager manager;
        public static SoundManager Manager
        {
            get
            {
                if (null == manager)
                {
                    manager = new SoundManager();
                }
                return manager;
            }
        }

        public void SetContentManager(ContentManager content)
        {
            Content = content;
        }

        private Dictionary<string, SoundEffect> soundEffects;

        private SoundEffect backgroundSoundEffect;

        private SoundEffectInstance backgroundSoundEffectInstance;

        //public SoundManager()
        //{
        //    this.soundEffects = new Dictionary<string, SoundEffect>();
        //}

        public void Initialize()
        {
        }

        public SoundEffectInstance SetBackgroundSound(string contentName, bool loop)
        {
            this.backgroundSoundEffect = Content.Load<SoundEffect>(contentName);
            this.backgroundSoundEffectInstance = this.backgroundSoundEffect.CreateInstance();
            this.backgroundSoundEffectInstance.IsLooped = loop;
            return this.backgroundSoundEffectInstance;
        }

        public void AddSoundEffect(string eventName, string contentName)
        {
            if (string.IsNullOrEmpty(eventName))
            {
                throw new ArgumentNullException("eventName");
            }

            if (string.IsNullOrEmpty(eventName))
            {
                throw new ArgumentNullException("contentName");
            }

            SoundEffect soundEffect = Content.Load<SoundEffect>(contentName);
            if (soundEffect == null)
            {
                throw new KeyNotFoundException("content was not found");
            }

            this.soundEffects.Add(eventName, soundEffect);
        }

        public void Start()
        {
            if (this.backgroundSoundEffectInstance != null)
            {
                this.backgroundSoundEffectInstance.Play();
            }
        }

        public void Stop()
        {
            if (this.backgroundSoundEffectInstance != null)
            {
                this.backgroundSoundEffectInstance.Stop();
            }
        }

        public void TriggerSoundEffect(string action)
        {
            // Verify while in debug, don't let the retail build
            // hit this bug tho.
            Debug.Assert(!string.IsNullOrEmpty(action));
            if (string.IsNullOrEmpty(action))
            {
                return;
            }

            if (this.soundEffects.ContainsKey(action))
            {
                SoundEffect effect = this.soundEffects[action];
                effect.Play();
            }
        }

        public float Volume 
        {
            get { return backgroundSoundEffectInstance.Volume; }
            set { backgroundSoundEffectInstance.Volume = value; }
        }

        public float Pitch
        {
            get { return backgroundSoundEffectInstance.Pitch; }
            set { backgroundSoundEffectInstance.Pitch = value; }
        }

        protected ContentManager Content { get; private set; }
    }
}

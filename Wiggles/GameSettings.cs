using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Wiggles
{
    public sealed class VideoSettings
    {
        public VideoSettings()
        {
            ColorDepth = 32;
            Width = 640;
            Height = 800;
        }

        public int ColorDepth { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public sealed class GameSettings
    {
        public GameSettings()
        {
            MusicVolume = .5f;
            MusicEnabled = true;

            SoundVolume = .5f;
            SoundEnabled = true;
        }

        public static GameSettings LoadSettings()
        {
            GameSettings settings = null;
            if (File.Exists(FileName))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameSettings));
                using (Stream settingsFileStream = File.OpenRead(FileName))
                {
                    settings = xmlSerializer.Deserialize(settingsFileStream) as GameSettings;
                    return settings;
                }
            }
            else
            {
                // HACKPOLOGY:  If there is no save, create one.  This logic should be pulled out of
                // here.
                settings = new GameSettings();
                settings.WriteSettings();
            }
            return settings;
        }

        //todo: build out write / save patterns once the load or create problem
        //is solved
        public void WriteSettings()
        {
            const string old = ".old";
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GameSettings));
                if (File.Exists(FileName))
                {
                    File.Move(FileName, FileName + old);
                }

                using (Stream gameSettings = File.OpenWrite(FileName))
                {
                    serializer.Serialize(gameSettings, this);
                }

                if (File.Exists(FileName + old))
                {
                    File.Delete(FileName + old);
                }
            }
            catch
            {
                if (File.Exists(FileName + old))
                {
                    File.Move(FileName + old, FileName);
                }
            }
        }

        /// <summary>
        /// Controls game music properties
        /// </summary>
        public float MusicVolume { get; set; }

        /// <summary>
        /// Set method employed over a property so calcs can be performed
        /// to properly enforce max and min range. Property setter logic was
        /// not taking the value passed and applying it but was instead applying
        /// propval + incrementalval which caused float math errors
        /// </summary>
        public void IncrementMusicVolume(float incrementValue)
        {
            if (MusicVolume + incrementValue >= 0.0f && 
                MusicVolume + incrementValue <= 1.0f)
            {
                this.MusicVolume += incrementValue;
            }
        }

        public void IncrementSoundEffectVolume(float incrementValue)
        {
            if (SoundVolume + incrementValue >= 0.0f &&
                MusicVolume + incrementValue <= 1.0f)
            {
                this.SoundVolume += incrementValue;
            }
        }

        public bool MusicEnabled { get; set; }
        
        /// <summary>
        /// Controls game sound effects properties
        /// </summary>
        public float SoundVolume { get; set; }

        public bool SoundEnabled { get; set; }

        public VideoSettings VideoSettings { get; set; }

        private const string FileName = "settings.dkp";
    }
}
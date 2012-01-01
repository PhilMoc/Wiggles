using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using DellaKrimm.Common;
using DellaKrimm.Common.Interfaces;

namespace Wiggles.Menus
{

    /// <summary>
    /// Controls the MusicSettings element in the main game
    /// </summary>
    public class ChangeMusicVolumeControl : MenuControl, IKeyboardControllable
    {
        public ChangeMusicVolumeControl(Game game)
            : base(game)
        {
            Text = "Music Volume";
            //hackpology: just testing mouse driven behavior
            OnSelected = new OnSelected(() =>
                {
                    ;
                });
        }

        public bool RequiresNewKey { get { return true; } }

        public void HandleKeys(KeyboardState keyboard, bool isNewKey)
        {
            //track if settings are changed; we may add additional inputs later
            //and really only want to write settings when they're changed for
            //performance reasons
            bool settingChanged = false; 

            if (RequiresNewKey == isNewKey)
            {
                if (keyboard.IsKeyDown(Keys.Right))
                {
                    // .5f is a temporary dev interval; will update with something
                    // more concrete after I learn if there's a max volume level
                    TowerAssault.Settings.IncrementMusicVolume(.1f);
                    settingChanged = true;
                }
                if (keyboard.IsKeyDown(Keys.Left))
                {
                    TowerAssault.Settings.IncrementMusicVolume(-.1f);
                    settingChanged = true;
                }
            }

            if (settingChanged)
            {
                TowerAssault.Settings.WriteSettings();
            }
        }

    }
}

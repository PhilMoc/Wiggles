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

namespace Wiggles.Menus
{
    public class AudioMenu : MenuScreen
    {
        public AudioMenu(Game game)
            : base(game)
        {
            //loads the same menu background for now, but could load any
            //BackgroundImage = Content.Load<Texture2D>("Images/menubackground");
            SelectedItemIndex = AddControl(new ChangeMusicVolumeControl(game));
            AddControl(new PreviousMenuControl(game));
            SelectedControl = Controls[SelectedItemIndex];
        }
    }
}

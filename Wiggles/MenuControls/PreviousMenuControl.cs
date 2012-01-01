using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using DellaKrimm.Common.Interfaces;
using DellaKrimm.Common;

namespace Wiggles.Menus
{
    public class PreviousMenuControl : MenuControl
    {
        public PreviousMenuControl(Game game)
            : base(game)
        {
            Text = "Back";

            OnSelected = new OnSelected(() =>
                {
                    //remove the parent screen from the stack; returning
                    //player to previous game screen
                    ScreenManager.Manager.Pop();
                });
        }
    }
}

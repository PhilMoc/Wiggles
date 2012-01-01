using System;
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
    public class ResumeGameControl : MenuControl
    {
        public ResumeGameControl(Game game)
            : base(game)
        {
            Text = "Resume Game";
            OnSelected = new OnSelected(
            () =>
            {
                ScreenManager.Manager.Pop();
            });
        }
    }
}

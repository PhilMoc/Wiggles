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

namespace Wiggles.MenuScreens
{
    public class QuitGameControl : MenuControl
    {
        public QuitGameControl(Game game, Vector2 position)
            : base(game, position)
        {
            Text = "Quit Game";
            Font = GameInstance.Content.Load<SpriteFont>("Fonts/MenuFont");

            OnSelected = new OnSelected(() =>
                    {
                        TowerAssault.GameInstance.Exit();
                    });
        }

    }
}

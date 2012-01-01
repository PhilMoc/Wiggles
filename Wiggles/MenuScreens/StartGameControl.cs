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

namespace Wiggles.MenuScreens
{
    public class StartGameControl : MenuControl
    {
        public StartGameControl(Game game, Vector2 position)
            : base(game, position)
        {
            Text = "Start Game";
            Font = GameInstance.Content.Load<SpriteFont>("fonts/MenuFont");
        }
    }
}

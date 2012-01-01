using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DellaKrimm.Common;
using DellaKrimm.Common.Interfaces;
using Wiggles.Menus;

namespace Wiggles
{
    public class BoilerScreen : GameScreen
    {
        public BoilerScreen(Game game)
            :base(game)
        {
            gameObjects = new List<DellaKrimm.Common.Interfaces.IDrawable>();
            gameObjects.Add(new Triangle(game.GraphicsDevice));
        }

        public void AddRenderable(DellaKrimm.Common.Interfaces.IDrawable drawable)
        {
            gameObjects.Add(drawable);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (DellaKrimm.Common.Interfaces.IDrawable r in gameObjects)
            {
                r.Draw(gameTime);
            }
        }

        public override void HandleKeys(Microsoft.Xna.Framework.Input.KeyboardState keyboard, bool isNewKey)
        {
            
        }

        public override bool RequiresNewKey
        {
            get { return false; }
        }

        protected override void Close()
        {
            ScreenManager.Manager.Pop(this);
        }

        protected List<DellaKrimm.Common.Interfaces.IDrawable> gameObjects;
    }
}

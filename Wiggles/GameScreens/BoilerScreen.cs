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
    public class BoilerScreen : GameScreen, IMouseControllable
    {
        public BoilerScreen(Game game)
            :base(game)
        {
            SoundManager.Manager.SetBackgroundSound("Music/yahhoo", false);
            gameObjects = new List<DellaKrimm.Common.Interfaces.IDrawable>();
            gameObjects.Add(new Wheel());
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

        public void HandleInput(MouseState currentState, MouseState previousState)
        {
            Vector3 cameraPos = TowerAssault.GameInstance.Camera.Position;

            if (currentState.ScrollWheelValue > previousState.ScrollWheelValue)
            {
                TowerAssault.GameInstance.Camera.Position = new Vector3(cameraPos.X, cameraPos.Y, cameraPos.Z + 100);
            }
            else if (currentState.ScrollWheelValue < previousState.ScrollWheelValue)
            {
                TowerAssault.GameInstance.Camera.Position = new Vector3(cameraPos.X, cameraPos.Y, cameraPos.Z - 100);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);



        }

        protected List<DellaKrimm.Common.Interfaces.IDrawable> gameObjects;

        public bool RequiresNewMouseState { get { return false; } }
    }
}

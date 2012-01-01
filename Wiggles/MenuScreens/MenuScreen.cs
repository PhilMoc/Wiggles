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

namespace Wiggles.MenuScreens
{
    public class MenuScreen : GameScreen
    {
        public MenuScreen(Game game)
            : base(game)
        {
            Controls = new List<MenuControl>();
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected List<MenuControl> Controls
        {
            get;
            set;
        }

        public override void Draw(GameTime gameTime)
        {
            Rectangle boundingRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            SpriteBatch.Begin();

            SpriteBatch.Draw(BackgroundImage, boundingRect, Color.White);

            SpriteBatch.End();

            foreach (MenuControl control in Controls)
            {
                control.Draw(gameTime);
            }
        }

        public override void HandleKeys(KeyboardState keyboardState, bool isNewKey)
        {
            if (RequiresNewKey == isNewKey)
            {
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    MoveSelectionUp();
                }
                else if (keyboardState.IsKeyDown(Keys.Down))
                {
                    MoveSelectionDown();
                }
                else if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    CurrentItemSelected();
                }
            }
        }

        public void MoveSelectionUp()
        {
            this.SelectedItemIndex = (this.SelectedItemIndex - 1) % this.Controls.Count;
            if (this.SelectedItemIndex == -1) this.SelectedItemIndex = this.Controls.Count - 1;
            this.SelectedControl = this.Controls[this.SelectedItemIndex];
        }

        public void MoveSelectionDown()
        {
            this.SelectedItemIndex = (this.SelectedItemIndex + 1) % this.Controls.Count;
            this.SelectedControl = this.Controls[this.SelectedItemIndex];
        }

        public void CurrentItemSelected()
        {
            if (this.SelectedControl != null)
            {
                if (SelectedControl.OnSelected != null)
                {
                    SelectedControl.OnSelected();
                }
            }

        }

        /// <summary>
        /// Adds a control and returns its index. Intended for when inheriting classes
        /// are constructing, so they can set a default selected item
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        protected int AddControl(MenuControl control)
        {
            Controls.Add(control);
            return Controls.Count - 1;
        }

        public override bool RequiresNewKey
        {
            get { return true; }
        }

        protected override void Close()
        {
            BackgroundSound.Stop();
            Dispose(true);
            ScreenManager.Manager.Pop(this);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (MenuControl control in Controls)
            {
                if (control == SelectedControl)
                {
                    control.Color = Color.OrangeRed;
                }
                else
                {
                    control.Color = Color.LightBlue;
                }
            }
            base.Update(gameTime);
        }

        protected MenuControl SelectedControl { get; set; }

        public int SelectedItemIndex { get; set; }

        protected string Label { get; set; }

        protected Texture2D BackgroundImage { get; set; }
        protected SpriteBatch SpriteBatch { get; set; }
        protected SpriteFont Font { get; set; }
    }
}

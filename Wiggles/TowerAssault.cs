/*TowerAssault.cs
 * Author: Brandon McMullin and Justin Dellamore ( DellaKrimm )
 * 
 * Description: A work in progress game that is specifically intended to be our resume
 * in a future application to Bungie Studios. The intent is that the player ( a Bungie 
 * emloyee ) would be required to play through a series of levels in a simple game to
 * obtain resume content beyond contact information.
 * 
 * At the time of writing this, the high level goal of the game is to breech a tower
 * guarded by a variety of weapon placements using a fixed number of "servant" NPCs of
 * various classes. Entry into the tower yields a highly coveted "script of rsme'e"
 * 
 * <3 Brandon and Justin
 * 
 * Per Audica Ad Astra
 */

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
using Wiggles.Menus;

namespace Wiggles
{
    /// <summary>
    /// Singleton instance intended to be the "View" component of a loosely implemented
    /// MVC pattern. With the exception of Update(), the game loop will not effect
    /// game behavior and is otherwise responsible for drawing the dominant screen.
    /// </summary>
    public class TowerAssault : Microsoft.Xna.Framework.Game
    {
        internal TowerAssault()
        {

            wiggles = this;
            Graphics = new GraphicsDeviceManager(GameInstance);
            Content.RootDirectory = "Content";
            Settings = new GameSettings();
            MenuScreen.Fontname = "Fonts/menufont";
            SoundManager.Manager.SetContentManager(Content);
            Settings = GameSettings.LoadSettings();
            Camera = new Camera(
                this,
                new Vector3(750, 750, 2500),
                Vector3.Zero,
                Vector3.Up);
        }

        private static TowerAssault wiggles;
        public static TowerAssault GameInstance
        {
            get
            {
                if (null == wiggles)
                {
                    wiggles = new TowerAssault();
                }
                return wiggles;
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            this.IsMouseVisible = true;
            mouseSelectZone = new Rectangle(-1, -1, 0, 0);
            prevMouseState = Mouse.GetState();

            ScreenManager.Manager.Push(new StartMenu(this));
            MenuScreen.Font = Content.Load<SpriteFont>("Fonts/menufont");
        }

        /// <summary>
        /// Asks the dominant "Model" component at the top of the state manager
        /// to update its internal state.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            ScreenManager.Manager.UpdateCurrent(gameTime);
            SoundManager.Manager.Volume = Settings.MusicVolume;
            HandleKeys();
            HandleMouse();
            
            base.Update(gameTime);
        }

        /// <summary>
        /// Asks the screen at the top of the screen manager to draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            ScreenManager.Manager.DrawCurrent(gameTime);
        }
                
        private void HandleKeys()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            bool isNewKey = previousKeyboardState != keyboardState;

            ScreenManager.Manager.HandleInput(keyboardState, isNewKey);

            previousKeyboardState = keyboardState;
        }

        MouseState previousMouseState;

        private void HandleMouse()
        {
            MouseState mouseState = Mouse.GetState();
            bool isNewState = previousMouseState == mouseState;

            ScreenManager.Manager.HandleMouseInput(mouseState, previousMouseState);

            previousMouseState = mouseState;
        }

        #region Properties

        public Camera Camera { get; set; }

        public GraphicsDeviceManager Graphics { get; private set; }

        public static GameSettings Settings { get; set; }

        protected KeyboardState previousKeyboardState;

        protected Rectangle mouseSelectZone;

        protected MouseState prevMouseState;

        #endregion Properties
    }
}
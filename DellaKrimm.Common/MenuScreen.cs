using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using DellaKrimm.Common.Interfaces;

using DellaKrimm.Common;

namespace DellaKrimm.Common
{
    public class MenuScreen : GameScreen
    {
        public MenuScreen(Game game)
            : base(game)
        {
            if (!Initialized)
            {
                SpriteBatch = new SpriteBatch(GraphicsDevice);
                controlMargin = GraphicsDevice.Viewport.Width / 8f;
                ControlDescription = "Hello, Kevin!";

                //place option descriptions near the bottom
                DescPosition = new Vector2( ( GraphicsDevice.Viewport.Width / 8f ) * 7 );
                Initialized = true; 
            }
            Controls = new List<MenuControl>();
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
                    SoundManager.Manager.TriggerSoundEffect(MoveSelectionTag);
                }
                else if (keyboardState.IsKeyDown(Keys.Down))
                {
                    MoveSelectionDown();
                    SoundManager.Manager.TriggerSoundEffect(MoveSelectionTag);
                }
                else if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    CurrentItemSelected();
                }

                // some controls are keyboard controllable for certain reasons,
                // like changing volume controls
                else if (SelectedControl is IKeyboardControllable)
                {
                    (SelectedControl as IKeyboardControllable).HandleKeys(keyboardState, isNewKey);
                }
            }
        }

        public void MoveSelectionUp()
        {
            this.SelectedItemIndex = (this.SelectedItemIndex - 1) % this.Controls.Count;
            if (this.SelectedItemIndex == -1) this.SelectedItemIndex = this.Controls.Count - 1;
            this.SelectedControl = this.Controls[this.SelectedItemIndex];
            this.ControlDescription = SelectedControl.Description;
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
        /// 
        /// TODO: update this control to manage placing menu controls
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        protected int AddControl(MenuControl control)
        {
            Controls.Add(control);

            //since vector2 is a struct, a new Vector2 must be assigned
            //rather than simply updating its value
            control.Position = new Vector2(controlMargin, CalculateVerticalPosition(Controls.Count - 1, control.Height)); 

            return Controls.Count - 1;
        }

        /// <summary>
        /// calculates the vertical position of a newly added control based on what 
        /// number the control is rather than its actual index
        /// </summary>
        /// <param name="itemIndex"></param>
        /// <returns></returns>
        private float CalculateVerticalPosition(int itemIndex, float controlHeight)
        {
            float yPosition = controlMargin;

            if (itemIndex >= 1)
            {
                //set new item position to be 7 points below the previous menu control
                yPosition += ( itemIndex * ( Controls[itemIndex - 1].Height + 7f));
            }

            return yPosition;
        }

        protected override void Close()
        {
            //BackgroundSound.Stop();
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
                control.Update(gameTime);
            }

            base.Update(gameTime);
        }

        #region properties


        public override bool RequiresNewKey
        {
            get { return true; }
        }

        public int SelectedItemIndex { get; set; }

        public string SelectionDescription
        {
            get;
            set;
        }

        public static string Fontname
        {
            get;
            set;
        }

        public static SpriteFont Font
        {
            get;
            set;
        }

        protected MenuControl SelectedControl { get; set; }

        protected string ControlDescription { get; set; }

        protected static Texture2D BackgroundImage { get; set; }

        protected static SpriteBatch SpriteBatch { get; set; }

        protected static string MoveSelectionTag { get; set; }

        /// <summary>
        /// Controls the horizontal position of all added menu controls
        /// to set them to a common margin line
        /// </summary>
        private static float controlMargin;

        protected static bool Initialized
        {
            get;
            set;
        }

        private Vector2 DescPosition
        {
            get;
            set;
        }

        #endregion
    }
}

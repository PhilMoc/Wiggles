using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using DellaKrimm.Common.Interfaces;

namespace DellaKrimm.Common
{
    public delegate void OnSelected();

    /// <summary>
    /// Generic base class for any game that needs to employ a menu with actionable
    /// menu controls. For the moment this class assumes menu controls will be in a 2D
    /// space.
    /// </summary>
    public abstract class MenuControl : DellaKrimm.Common.Interfaces.IDrawable
    {
        public MenuControl(Game game)
        {
            if (!Initialized)
            {
                GameInstance = game;
                GraphicsService = (IGraphicsDeviceService)GameInstance.Services.GetService(typeof(IGraphicsDeviceService));
                GraphicsDevice = GraphicsService.GraphicsDevice;
                SpriteBatch = new SpriteBatch(GraphicsDevice);
                Font = MenuScreen.Font;

                Initialized = true;
            }

        }

        /// <summary>
        /// Represents the title of the menu control and what will be printed
        /// </summary>
        private string text;
        protected string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        protected void SetBoundingRect()
        {
            BoundingRect = new Rectangle((int)Position.X, (int)Position.Y, (int)Font.MeasureString(Text).X, (int)Font.MeasureString(Text).Y);
        }

        public string Description
        {
            get;
            protected set;
        }

        public static SpriteFont Font
        {
            get;
            set;
        }

        private Vector2 position;
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                SetBoundingRect();
            }
        }

        public float Height
        {
            get { return Font.MeasureString(Text).Y; }
        }

        public virtual void Draw(GameTime gameTime)
        {
            Texture2D dummyTexture = new Texture2D(GraphicsDevice, 1, 1);
            dummyTexture.SetData(new Color[] { Color.White });
            SpriteBatch.Begin();

            SpriteBatch.DrawString(Font, Text, Position, Color);
            //SpriteBatch.Draw(dummyTexture, BoundingRect, Color.Yellow);
            
            SpriteBatch.End();
        }

        public virtual void Update(GameTime gameTime)
        {
            MouseState currentMouseState = Mouse.GetState();
            Point mousePos = new Point(currentMouseState.X, currentMouseState.Y);
            Rectangle mouseRect = new Rectangle(mousePos.X, mousePos.Y, 1, 1);
            
            if (this.BoundingRect.Intersects(mouseRect)) // && Mouse.GetState().LeftButton == ButtonState.Pressed )
            {
                this.Color = Color.Purple;
                System.Diagnostics.Trace.WriteLine("In debug mouse click catcher for " + Text + " control...");
                //debug
                if (currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    System.Diagnostics.Trace.WriteLine("got click...");
                    System.Diagnostics.Trace.WriteLine(string.Format("states - prev: {0} | curr: {1}", prevMouseState.LeftButton.ToString(), currentMouseState.LeftButton.ToString()));
                    this.Color = Color.SandyBrown;
                }

                if ( currentMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released )  
                {

                    System.Diagnostics.Trace.WriteLine("got distinct click...");
                    System.Diagnostics.Trace.WriteLine(string.Format("states - prev: {0} | curr: {1}", prevMouseState.LeftButton.ToString(), currentMouseState.LeftButton.ToString()));
                    this.OnSelected();
                }
            }
            if (prevMouseState.LeftButton != currentMouseState.LeftButton)
            {
                System.Diagnostics.Trace.WriteLine(string.Format("states - prev: {0} | curr: {1}", prevMouseState.LeftButton.ToString(), currentMouseState.LeftButton.ToString()));
            }
             prevMouseState = currentMouseState;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool disposeAll)
        { }

        protected static Game GameInstance
        {
            get;
            private set;
        }

        protected static GraphicsDevice GraphicsDevice
        {
            get;
            private set;
        }

        protected static IGraphicsDeviceService GraphicsService
        {
            get;
            private set;
        }

        protected static SpriteBatch SpriteBatch
        {
            get;
            private set;
        }

        /// <summary>
        /// Event handler that allows each MenuControl to execute the currently selected mmenu control
        /// </summary>
        public OnSelected OnSelected { get; protected set; }

        protected bool Initialized
        {
            get;
            private set;
        }

        public Color Color
        {
            get;
            set;
        }

        private Rectangle BoundingRect { get; set; }

        protected static MouseState prevMouseState;
    }
}

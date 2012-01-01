/* GameScreen.cs
 * Author: Brandon McMullin, 10/31/2011
 * Description: Universal base class to manage common resources for GameScreen objects
 * such as menu screens, loading screens and game play screens. Each deriving game screen
 * is responsible for its own Draw() override so that underlying Draw logic is agnostic
 */

using System;
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
    /// <summary>
    /// Relatively generic, re-usable base class for renderable screens. Intended to be re-usable
    /// for other game projects.
    /// </summary>
    public abstract class GameScreen : DrawableGameComponent, IKeyboardControllable
    {
        public GameScreen(Game game)
            : base(game)
        {
            GraphicsService = (IGraphicsDeviceService)Game.Services.GetService(typeof(IGraphicsDeviceService));
            Initialize();
        }

        protected IGraphicsDeviceService GraphicsService
        {
            get;
            private set;
        }

        protected ContentManager Content
        {
            get { return Game.Content; }
        }

        //public static SoundEffectInstance BackgroundSound
        //{
        //    get;
        //    protected set;
        //}

        /// <summary>
        /// Stipulates that all GameScreens should be able to close themselves
        /// and do some minimal cleanup ( like stop playing sound effects ). Intended 
        /// for each implementing class to call it on itself when its own exit condition
        /// is met
        /// </summary>
        protected abstract void Close();

        /// <summary>
        /// Determines whether or not a game screen requires distinct keypresses as valid input ( true )
        /// or if a continuous keypress is valid ( false )
        /// </summary>
        public abstract bool RequiresNewKey { get; }

        /// <summary>
        /// Forces each inheriting class to be responsible for listening for its own input. To make
        /// decisions based on input as simple as possible without clashing with expected inputs of other
        /// game screen states
        /// </summary>
        /// <param name="keyboard"></param>
        public abstract void HandleKeys(KeyboardState keyboard, bool isNewKey);

        public override void Update(GameTime gameTime)
        {
            //if (BackgroundSound.State != SoundState.Playing)
            //{
            //    BackgroundSound.Play();
            //}
        }
    }
}

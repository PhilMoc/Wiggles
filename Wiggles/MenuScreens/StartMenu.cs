using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using DellaKrimm.Common;

namespace Wiggles.Menus
{
    public class StartMenu : MenuScreen
    {
        public StartMenu(Game game)
            : base(game)
        {
            BackgroundImage = Content.Load<Texture2D>("Images/menubackground");

            MoveSelectionTag = SoundEvents.MoveEvent;
            SoundManager.Manager.AddSoundEffect(MoveSelectionTag, "Sounds/" + MoveSelectionTag);

            SoundManager.Manager.SetBackgroundSound("Music/backgroundsoundloop", true);
            SoundManager.Manager.Volume = 0.05f;
            SoundManager.Manager.Pitch = -0.2f;

            //I'm not happy with this; might have the game ctor create a menufont that gets passed
            //when needed but I haven't finished thinking that part through
            MenuScreen.Font = Content.Load<SpriteFont>("Fonts/menufont");
            SelectedItemIndex = AddControl(new StartGameControl(game));
            SelectedControl = Controls[SelectedItemIndex];
            AddControl(new AudioMenuControl(game));
            AddControl(new QuitGameControl(game));
        }
    }
}

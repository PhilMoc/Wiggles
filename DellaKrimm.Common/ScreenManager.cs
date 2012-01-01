/*
 * ScreenManager.cs
 * Author: Brandon McMullin, 10/31/2011
 * Description: ScreenManager is a singleton class that is responsible for maintaining a stack
 * of stateful GameScreen objects. It will update and draw the GameScreen on the top of this stack
 * and call Cleanup on GameScreen objects as they are popped off the stack
 */

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DellaKrimm.Common
{
    /// <summary>
    /// Screen manager is effectively half the "View" layer in a roughly implemented
    /// MVC pattern. All possible screen states are placed in this singleton stack to provide 
    /// quick return to previous screen states. The top-most screen state is the active
    /// one and will get calls for update and draw. 
    /// </summary>
    public class ScreenManager
    {
        private ScreenManager()
        {
            screens = new List<GameScreen>();
        }
                
        /// <summary>
        /// Adds a GameScreen object to the top of the list, which makes
        /// it the de-facto "dominant" GameScreen object
        /// </summary>
        public void Push(GameScreen screen)
        {
            screens.Add(screen);
        }

        public void Pop()
        {
            screens.Remove(CurrentScreen);
        }
        
        /// <summary>
        /// Removes the provided Screen from the collection, removing any
        /// Screen objects sitting on top of it. Talk about this with Justin
        /// </summary>
        public void Pop(GameScreen screen)
        {
            screens.RemoveRange(screens.IndexOf(screen), screens.Count - 1);
        }

        /// <summary>
        /// Updates the current Screen ( top most ) while ignoring the rest. At no
        /// point does the ScreenManager give direct access to any of the states
        /// it manages.
        /// </summary>
        public void UpdateCurrent(GameTime gameTime)
        {
            CurrentScreen.Update(gameTime);
        }

        /// <summary>
        /// Draws the current Screen ( top most ) while ignoring the rest. At no
        /// point does the ScreenManager give direct access to any of the states
        /// it manages.
        /// </summary>
        public void DrawCurrent(GameTime gameTime)
        {
            CurrentScreen.Draw(gameTime);
        }

        public void HandleInput(KeyboardState keyBoard, bool isNewKey)
        {
            CurrentScreen.HandleKeys(keyBoard, isNewKey);
        }

        private static ScreenManager manager;
        public static ScreenManager Manager
        {
            get
            {
                if (null == manager)
                {
                    manager = new ScreenManager();
                }
                return manager;
            }
        }

        protected GameScreen CurrentScreen
        {
            get
            {
                if (screens.Count > 0)
                {
                    return screens[screens.Count - 1];
                }
                else
                {
                    throw new IndexOutOfRangeException("There are no GameScreens being managed by the ScreenManager at this time.");
                }
            }
        }

        /// <summary>
        /// The actual collection of GameScreen objects. Made private to prevent index
        /// math errors that might cause confusing errors.
        /// </summary>
        private List<GameScreen> screens;
    }
}

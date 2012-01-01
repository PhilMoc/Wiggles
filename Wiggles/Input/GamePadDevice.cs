/*
 * This code, however, is written by myself as an extension of the InnovationEngine 
 * input utility code.
 */


using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using InnovationEngine.Input;

namespace Wiggles.Input
{
    public class GamePadDevice : InputDevice<GamePadState>
    {
        //Each game instance will only have to track one player index at any given time.
        //Adding support for multiple players would likely take shape in a turn based system
        //on the same game instance or more likely multiple distinct clients connecting to a
        //host
        public override GamePadState State
        {
            get { return GamePad.GetState(PlayerIndex.One); }
        }
    }
}

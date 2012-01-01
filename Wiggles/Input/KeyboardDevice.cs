// Credit goes to Sean James for the InputDevice code, courtesy of his blog.
// http://www.innovativegames.net/blog/blog/2008/11/16/engine-tutorial-6/

using System;
using Microsoft.Xna.Framework.Input;

namespace InnovationEngine.Input
{
    public delegate void InputEventHandler<O, S>(object sender, InputDeviceEventArgs<O, S> e);

    public class KeyboardDevice : InputDevice<KeyboardState>
    {
        public KeyboardDevice()
        {
            currentState = Keyboard.GetState();
            Update();
        }

        KeyboardState previousState;
        KeyboardState currentState;

        Keys[] currentKeys;

        public override KeyboardState State
        {
            get { return currentState; }
        }

        public Keys[] PressedKeys 
        { 
            get { return currentKeys; } 
        }

        public event InputEventHandler<Keys, KeyboardState> KeyPressed;
        public event InputEventHandler<Keys, KeyboardState> KeyReleased;
        public event InputEventHandler<Keys, KeyboardState> KeyHeld;

        public void Update()
        {
            // Set the last state to the current one and update the
            // current state
            previousState = currentState;
            currentState = Keyboard.GetState();

            // Set the currently pressed keys to the keys defined in
            // the current state
            currentKeys = currentState.GetPressedKeys();
        }
    }
}

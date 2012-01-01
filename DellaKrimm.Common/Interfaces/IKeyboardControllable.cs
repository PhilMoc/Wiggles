using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Input;

namespace DellaKrimm.Common.Interfaces
{
    /// <summary>
    /// In the update pump, check to see if the current scene will handle key state
    /// </summary>
    public interface IKeyboardControllable
    {
        /// <summary>
        /// Requires a new state to be sent to the keyboard (soft enforcement)
        /// </summary>
        bool RequiresNewKey { get; }

        /// <summary>
        /// Handles the key
        /// </summary>
        void HandleKeys(KeyboardState keyboardState, bool isNewKey);
    }

}

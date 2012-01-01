using System;
using Microsoft.Xna.Framework.Input;

namespace DellaKrimm.Common.Interfaces
{
    interface IMouseControllable
    {
        bool RequiresNewClick { get; }
        void HandleInput(MouseState mouseState, bool isNewClick);
    }
}

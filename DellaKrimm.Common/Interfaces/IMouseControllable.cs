using System;
using Microsoft.Xna.Framework.Input;

namespace DellaKrimm.Common.Interfaces
{
    public interface IMouseControllable
    {
        bool RequiresNewMouseState { get; }
        void HandleInput(MouseState currentState, MouseState previousState);
    }
}

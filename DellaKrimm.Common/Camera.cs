using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace DellaKrimm.Common
{
    public class Camera : IGameComponent
    {
        public Matrix View { get; protected set; }

        public Matrix Projection { get; protected set; }

        public Camera(Game game, Vector3 position, Vector3 target, Vector3 up)
        {
            this.Position = position;
            this.View = Matrix.CreateLookAt(position, target, up);

            this.Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                (float)game.Window.ClientBounds.Width / (float)game.Window.ClientBounds.Height,
                1.0f,
                100.0f);
        }

        public void Initialize()
        {
        }

        public Vector3 Position { get; set; }
    }
}

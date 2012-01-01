using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DellaKrimm.Common.Interfaces
{
    public interface IDrawable : IDisposable
    {
        void Draw(GameTime gameTime);

        void Update(GameTime gameTime);
    }
}

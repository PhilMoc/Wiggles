using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DellaKrimm.Common;
using DellaKrimm.Common.Interfaces;

namespace Wiggles
{
    public class Wheel : DellaKrimm.Common.Interfaces.IDrawable
    {
        public Wheel()
        {
            model = TowerAssault.GameInstance.Content.Load<Model>("Models/steering_wheel/steering_wheel");
        }

        public void Draw(GameTime time)
        {
            Vector3 pos = Vector3.Zero;
            TowerAssault.GameInstance.GraphicsDevice.Clear(Color.CornflowerBlue);
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] *
                        Matrix.CreateRotationY(0f)
                        * Matrix.CreateTranslation(pos);
                    effect.View = Matrix.CreateLookAt(TowerAssault.GameInstance.Camera.Position,
                        Vector3.Zero, Vector3.Up);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                        MathHelper.ToRadians(45.0f), TowerAssault.GameInstance.GraphicsDevice.Viewport.AspectRatio,
                        1.0f, 10000.0f);
                }
                // Draw the mesh, using the effects set above.
                mesh.Draw();
            }

        }
        public void Update(GameTime time)
        { }
        public void Dispose()
        {
 
        }

        Model model;
    }
}

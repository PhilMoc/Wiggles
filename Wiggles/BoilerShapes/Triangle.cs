using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DellaKrimm.Common;
using DellaKrimm.Common.Interfaces;

namespace Wiggles
{
    public class Triangle : DellaKrimm.Common.Interfaces.IDrawable
    {
        private VertexBuffer vertexBuffer;
        private GraphicsDevice graphicsDevice;

        public BasicEffect Effect { get; set; }
        public VertexPositionColor[] Vertices { get; set; }

        public Triangle(GraphicsDevice graphicsDevice)
            : this(graphicsDevice, new Vector3(0, 1, 0), new Vector3(1, -1, 0), new Vector3(-1, -1, 0))
        {
            Initialize();
        }

        public Triangle(GraphicsDevice graphicsDevice, Vector3 x, Vector3 y, Vector3 z)
        {
            this.graphicsDevice = graphicsDevice;
            this.Vertices = new VertexPositionColor[3];
            this.Vertices[0] = new VertexPositionColor(x, Color.Red);
            this.Vertices[1] = new VertexPositionColor(y, Color.White);
            this.Vertices[2] = new VertexPositionColor(z, Color.Blue);

            vertexBuffer = new VertexBuffer(
                this.graphicsDevice,
                typeof(VertexPositionColor),
                this.Vertices.Length,
                BufferUsage.None);
            vertexBuffer.SetData<VertexPositionColor>(this.Vertices);
        }

        public void Initialize()
        {
            this.Effect = new BasicEffect(this.graphicsDevice);
        }

        public void Draw(GameTime gameTime)
        {
            this.Effect.AmbientLightColor = new Vector3(0, 0, 0);

            Camera camera = TowerAssault.GameInstance.Camera;

            this.Effect.VertexColorEnabled = true;
            this.Effect.World = Matrix.Identity;
            this.Effect.View = camera.View;
            this.Effect.Projection = camera.Projection;

            foreach (EffectPass pass in this.Effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                this.graphicsDevice.DrawUserPrimitives<VertexPositionColor>(
                    PrimitiveType.TriangleStrip,
                    this.Vertices,
                    0,
                    1);
            }
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Dispose()
        { }
    }
}
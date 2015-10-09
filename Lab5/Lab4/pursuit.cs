using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lab4
{
    class pursuit : Tank
    {
        public pursuit(Model model, GraphicsDevice device, Camera camera) : base(model,device,camera)
        {
            tankBox = new BoundingBox(MIN, MAX);
            currentPosition= new Vector3(-500, 0, -500);
            translation = Matrix.CreateTranslation(currentPosition);
            velocity = new Vector3(1, 0, 1);
            new Tank(model, device, camera);
      }

        public override void Update(GameTime gametime)
        {
            min = MIN + currentPosition;
            max = MAX + currentPosition;
            tankBox = new BoundingBox(min, max);
            turretRorationValue = (float)Math.Sin(gametime.TotalGameTime.TotalSeconds);
        }

        public override void Draw(GraphicsDevice device, Camera camera)
        {
            base.Draw(device, camera);
        }

        protected override Matrix GetWorld()
        {
            return Matrix.CreateScale(0.1f) * rotation * translation;
        }
    }
}

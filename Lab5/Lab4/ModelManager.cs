using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lab4
{
    class ModelManager:Microsoft.Xna.Framework.DrawableGameComponent
    {
        List<BasicModel> models = new List<BasicModel>();
        Ground ground;
        Tank tank;
        pursuit tank2;
        Steering steer;
        public string Speed { get { return speed; } }
        private string speed;
        public string Distance { get { return distance.ToString(); } }
        private float distance;
        public ModelManager(Game game):base(game)
        {

        }

        public override void Initialize()
        { 
           ground=new Ground(Game.Content.Load<Model>(@"Models/Ground/Ground"));         
            models.Add(ground);
      // models.Add(new SkyBox(
        //     Game.Content.Load<Model>(@"Models/Sky/skybox")));
              tank=new Tank(Game.Content.Load<Model>(@"Models/Tank/tank"),(((Game1)Game).GraphicsDevice),((Game1)Game).camera);
            tank2 = new pursuit(Game.Content.Load<Model>(@"Models/Tank/tank"), (((Game1)Game).GraphicsDevice), ((Game1)Game).camera);
            models.Add(tank2);
            models.Add(tank);
            steer=new Steering(150f, 300f);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            
            base.LoadContent();
        }
        public override void Update(GameTime gametime)
        {
            foreach (BasicModel model in models)
            {
                model.Update(gametime);
            }
            
           speed = tank2.speed.ToString();
            distance = Vector3.Subtract(tank.currentPosition, tank2.currentPosition).Length();
            if (distance> Tank.atDestinationLimit&&tank2.tankBox.Contains(tank.tankBox)==ContainmentType.Disjoint)
            {
               tank2.speed = tank2.velocity.Length();
               tank2.rotationAngle = (float)Math.Atan2(tank2.velocity.Z, tank2.velocity.X);
                tank2.moveAngle = tank2.rotationAngle - tank2.currentAngle;
               tank2.rotation = Matrix.CreateRotationY(-tank2.moveAngle);
                tank2.wheelRotationValue = (float)gametime.TotalGameTime.TotalSeconds * 10;
                tank2.canonRotationValue = (float)Math.Sin(gametime.TotalGameTime.TotalSeconds * 0.25f) * 0.333f - 0.333f;
               tank2.hatchRotationValue = MathHelper.Clamp((float)Math.Sin(gametime.TotalGameTime.TotalSeconds * 2) * 2, -1, 0);
                tank2.velocity += steer.pursue(tank.currentPosition,tank.velocity,tank2.currentPosition,tank2.velocity) * (float)gametime.ElapsedGameTime.TotalSeconds*1.5f;
               tank2.currentPosition += tank2.velocity * (float)gametime.ElapsedGameTime.TotalSeconds;
                tank2.translation = Matrix.CreateTranslation(tank2.currentPosition);
            }
            base.Update(gametime);
        }
        public override void Draw(GameTime gameTime)
        {
            foreach (BasicModel model in models)
            {
                model.Draw(((Game1)Game).device,((Game1)Game).camera);             
            }
            base.Draw(gameTime);
        }
    }
}

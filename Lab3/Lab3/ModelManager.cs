using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lab3
{
    class ModelManager:Microsoft.Xna.Framework.DrawableGameComponent
    {
        List<BasicModel1> models = new List<BasicModel1>();
        
        public ModelManager(Game game):base(game)
        {

        }
        public override void Initialize()
        {
            //models.Add(new BasicModel1(Game.Content.Load<Model>(@"Models/Ground/Ground")));
            models.Add(new Ground(
                Game.Content.Load<Model>(@"Models/Ground/Ground")));
           models.Add(new SkyBox(
                Game.Content.Load<Model>(@"Models/Sky/skybox")));
            base.Initialize();
        }
        protected override void LoadContent()
        {
            
            base.LoadContent();
        }
        public override void Update(GameTime gametime)
        {
            foreach (BasicModel1 model in models)
            {
                model.Update();
            }
            base.Update(gametime);
        }
        public override void Draw(GameTime gameTime)
        {
            foreach (BasicModel1 model in models)
            {
                model.Draw(((Game1)Game).device,((Game1)Game).camera);
                
            }
            base.Draw(gameTime);
        }
    }
}

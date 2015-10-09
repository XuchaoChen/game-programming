using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace lab2
{
    class Camera:Microsoft.Xna.Framework.GameComponent
    {
        public Matrix view { get; protected set;}
        public Matrix projection { get; protected set; }

        public Camera(Game game,Vector3 pos,Vector3 target,Vector3 up):base(game)
        {
            view = Matrix.CreateLookAt(pos,target,up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                (float)Game.Window.ClientBounds.Width / (float)Game.Window.ClientBounds.Height,
                1,100);
        }

        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Update(GameTime gameTime) 
        {
            base.Update(gameTime);
        }
    }

}

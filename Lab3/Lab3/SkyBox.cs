using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lab3
{
    class SkyBox:BasicModel1
    {
        Matrix skybox;
        public SkyBox(Model model) : base(model)
        {

        }
        public override void Update()
        {
            
            base.Update();
        }
        public override void Draw(GraphicsDevice device, Camera camera)
        {

            device.SamplerStates[0] = SamplerState.LinearClamp;
            skybox= Matrix.CreateScale(300f)* Matrix.CreateTranslation(camera.cameraPosition.X,0,camera.cameraPosition.Z);
            base.Draw(device, camera);
        }
        protected override Matrix GetWorld()
        {
            return skybox ;
        }
    }
}

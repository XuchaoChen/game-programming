﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lab3
{
    class Ground:BasicModel1
    {
        public Ground(Model model) : base(model)
        {

        }
        public override void Draw(GraphicsDevice device, Camera camera)
        {
            device.SamplerStates[0] = SamplerState.LinearWrap;
            base.Draw(device, camera);
        }
    }
}
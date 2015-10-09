using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lab3
{
    class BasicModel1
    {
        public Model model { get; protected set; }
        protected Matrix world = Matrix.Identity;
        public BasicModel1(Model model)
        {
            this.model = model;
        }
        public virtual void Update()
        {

        }
        public virtual void Draw(GraphicsDevice device,Camera camera)
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = mesh.ParentBone.Transform*GetWorld();
                    effect.View = camera.view;
                    effect.Projection = camera.projection;
                    effect.TextureEnabled = true;
                }
                mesh.Draw();
            }
        }
        protected virtual Matrix GetWorld()
        {
            return world;
        }
    }
}

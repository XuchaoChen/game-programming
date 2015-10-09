using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Lab3
{
    public class Camera:Microsoft.Xna.Framework.GameComponent
    {
        public Matrix view { get; protected set; }
        public Matrix projection { get; protected set; }
        public Vector3 cameraPosition { get; protected set; }
        Vector3 cameraDirection;
        Vector3 cameraUp;
        Vector3 moveDirection;
        float speed = 3;
        bool jumping;
        float jumpspeed = 0;
        Vector3 startPosition;
        MouseState preMouseState;
        public Camera(Game game, Vector3 pos, Vector3 target, Vector3 up) : base(game)
        {
            cameraPosition = pos;
            cameraDirection = target - pos;
            cameraDirection.Normalize();
            cameraUp = up;
            cameraUp.Normalize();
            CreateLookAt();
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                (float)game.Window.ClientBounds.Width/(float)game.Window.ClientBounds.Height,
                1,3000);
        }
        public override void Initialize()
        {
            Mouse.SetPosition(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height / 2);
            preMouseState = Mouse.GetState();
            startPosition = cameraPosition;
            jumping = false;
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
        
            moveDirection = new Vector3(cameraDirection.X, 0, cameraDirection.Z);
            moveDirection.Normalize();
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                cameraPosition += moveDirection * speed;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                cameraPosition -= moveDirection * speed;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                cameraPosition += Vector3.Cross(cameraUp, cameraDirection) * speed;
             if (Keyboard.GetState().IsKeyDown(Keys.D))
                cameraPosition -= Vector3.Cross(cameraUp, cameraDirection) * speed;
            if (jumping)
            {
               cameraPosition -=Vector3.UnitY* jumpspeed;
                jumpspeed += 1;
                if (cameraPosition.Y<=startPosition.Y)
                {
                    cameraPosition = startPosition;
                    jumping = false;
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    jumping = true;
                    jumpspeed -= 15;
                }
            }
           float YawAngle = (-MathHelper.PiOver4 / 150) *(Mouse.GetState().X - preMouseState.X);
                cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(cameraUp,YawAngle));
       float PitchAngle = (-MathHelper.PiOver4 / 150) * (Mouse.GetState().Y - preMouseState.Y);
            Vector3 normalisecross = Vector3.Cross(cameraUp, cameraDirection);
            normalisecross.Normalize();
               cameraDirection = Vector3.Transform(cameraDirection,Matrix.CreateFromAxisAngle(normalisecross, PitchAngle));
           //cameraUp = Vector3.Transform(cameraUp,
                   //Matrix.CreateFromAxisAngle(Vector3.Cross(cameraUp, cameraDirection), PitchAngle));
                preMouseState = Mouse.GetState();
  
            CreateLookAt();
            base.Update(gameTime);
            
        }
        private void CreateLookAt()
        {
            view = Matrix.CreateLookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
        }
    }
}

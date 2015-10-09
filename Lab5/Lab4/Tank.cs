using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab4
{
    class Tank:BasicModel
    {
      public  Matrix translation = Matrix.Identity;
     public    Matrix rotation = Matrix.Identity;
       public MousePick mousepick;
        Steering steer = new Steering(100f, 300f);

        public readonly Vector3 MIN = new Vector3(-40, 0, -40);
        public readonly Vector3 MAX = new Vector3(40, 50, 40);
        public Vector3 min;
        public Vector3 max;
        public BoundingBox tankBox;
       public ModelBone turretBone;
       public ModelBone rightfrontwheel;
      public  ModelBone rightbackwheel;
       public ModelBone leftfrontwheel;
      public  ModelBone leftbackwheel;
        public ModelBone rightsteergeo;
        public ModelBone leftsteergeo;
        public ModelBone canongeo;
        public ModelBone hatchgeo;

        public Matrix leftBackWheelTransform;
        public Matrix rightBackWheelTransform;
        public Matrix leftFrontWheelTransform;
        public Matrix rightFrontWheelTransform;
        public Matrix leftSteerTransform;
        public Matrix rightSteerTransform;
        public Matrix turretTransform;
        public Matrix canonTransform;
        public Matrix hatchTransform;
        public Vector3 velocity = Vector3.Zero;
        public Vector3 currentPosition;
        Vector3 pickPosition;
        public float speed;
        public float wheelRotationValue;
        public float steerRotationValue;
        public float hatchRotationValue;
        public float canonRotationValue;
        public float turretRorationValue;
        public float rotateSpeed = 0.05f;
        public float distanceTopickPosition;
        public const float atDestinationLimit=2f;
        public float rotationAngle;
        public float currentAngle;
        public float AngleLimit=0.03f;
        public float moveAngle;
        public Tank(Model model,GraphicsDevice device,Camera camera):base(model)
        {
            mousepick = new MousePick(device, camera);
            turretBone = model.Bones["turret_geo"];
            rightfrontwheel = model.Bones["r_front_wheel_geo"];
            rightbackwheel = model.Bones["r_back_wheel_geo"];
            leftfrontwheel = model.Bones["l_front_wheel_geo"];
            leftbackwheel = model.Bones["l_back_wheel_geo"];
            rightsteergeo = model.Bones["r_steer_geo"];
            leftsteergeo = model.Bones["l_steer_geo"];
            hatchgeo = model.Bones["hatch_geo"];
            canongeo = model.Bones["canon_geo"];
            leftBackWheelTransform = leftbackwheel.Transform;
            rightBackWheelTransform = rightbackwheel.Transform;
            leftFrontWheelTransform = leftfrontwheel.Transform;
            rightFrontWheelTransform = rightfrontwheel.Transform;
            leftSteerTransform = leftsteergeo.Transform;
            rightSteerTransform = rightsteergeo.Transform;
            turretTransform = turretBone.Transform;
            hatchTransform = hatchgeo.Transform;
            canonTransform = canongeo.Transform;
            currentAngle = MathHelper.PiOver2;
            currentPosition = new Vector3(0, 0, 0);
            tankBox = new BoundingBox(MIN, MAX);
    
        }

        public override void Update(GameTime gametime)
        {
            //    steerRotationValue = (float)Math.Sin(gametime.TotalGameTime.TotalSeconds * 0.75f) * 0.5f;
            min = MIN + currentPosition;
            max = MAX + currentPosition;
            tankBox = new BoundingBox(min, max);
            turretRorationValue = (float)Math.Sin(gametime.TotalGameTime.TotalSeconds);
            if ( Mouse.GetState().LeftButton == ButtonState.Pressed&&mousepick.GetCollisionPosition().HasValue==true)
            {
                pickPosition = mousepick.GetCollisionPosition().Value;
            }
              distanceTopickPosition = Vector3.Distance(pickPosition, currentPosition);
            if (distanceTopickPosition > atDestinationLimit)
            {
                speed = velocity.Length();
                rotationAngle =(float)Math.Atan2(velocity.Z, velocity.X);
                moveAngle = rotationAngle - currentAngle;
                rotation = Matrix.CreateRotationY(-moveAngle);
                wheelRotationValue = (float)gametime.TotalGameTime.TotalSeconds * 10;
                canonRotationValue = (float)Math.Sin(gametime.TotalGameTime.TotalSeconds * 0.25f) * 0.333f - 0.333f;
                hatchRotationValue = MathHelper.Clamp((float)Math.Sin(gametime.TotalGameTime.TotalSeconds * 2) * 2, -1, 0);
                velocity += steer.seek(pickPosition, currentPosition, velocity)* (float)gametime.ElapsedGameTime.TotalSeconds;
                currentPosition += velocity * (float)gametime.ElapsedGameTime.TotalSeconds ;
                translation = Matrix.CreateTranslation(currentPosition);
            }
            }
        
        
        public override void Draw(GraphicsDevice device, Camera camera)
        {
            Matrix wheelRotation = Matrix.CreateRotationX(wheelRotationValue);
             Matrix steerRotation = Matrix.CreateRotationY(steerRotationValue);
             Matrix turretRotation = Matrix.CreateRotationY(turretRorationValue);
             Matrix canonRotation = Matrix.CreateRotationX(canonRotationValue);
             Matrix hatchRotation = Matrix.CreateRotationX(hatchRotationValue);
             leftbackwheel.Transform = wheelRotation * leftBackWheelTransform;
             rightbackwheel.Transform = wheelRotation * rightBackWheelTransform;
             leftfrontwheel.Transform = wheelRotation * leftFrontWheelTransform;
             rightfrontwheel.Transform = wheelRotation * rightFrontWheelTransform;
             leftsteergeo.Transform = steerRotation * leftSteerTransform;
             rightsteergeo.Transform = steerRotation * rightSteerTransform;
             hatchgeo.Transform = hatchRotation * hatchTransform;
             turretBone.Transform = turretRotation * turretTransform;
            base.Draw(device, camera);
        }
        protected override Matrix GetWorld()
        {
         
            return Matrix.CreateScale(0.1f)*rotation*translation;
        }
    }
}

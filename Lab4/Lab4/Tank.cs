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
        Matrix translation = Matrix.Identity;
        Matrix rotation = Matrix.Identity;
        MousePick mousepick;

        
        ModelBone turretBone;
        ModelBone rightfrontwheel;
        ModelBone rightbackwheel;
        ModelBone leftfrontwheel;
        ModelBone leftbackwheel;
        ModelBone rightsteergeo;
        ModelBone leftsteergeo;
        ModelBone canongeo;
        ModelBone hatchgeo;

        Matrix leftBackWheelTransform;
        Matrix rightBackWheelTransform;
        Matrix leftFrontWheelTransform;
        Matrix rightFrontWheelTransform;
        Matrix leftSteerTransform;
        Matrix rightSteerTransform;
        Matrix turretTransform;
        Matrix canonTransform;
        Matrix hatchTransform;
        Vector3 prePosition=Vector3.Zero;
        Vector3 moveDirection;
        public Vector3 CurrentPosition { get { return currentPosition; } }
        Vector3 currentPosition;
        public Vector3 PickPosition { get { return pickPosition; } }
        Vector3 pickPosition;

        float wheelRotationValue;
        float steerRotationValue;
        float hatchRotationValue;
        float canonRotationValue;
        float turretRorationValue;
        float speed=10f;
        float rotateSpeed = 0.05f;
        float distanceTopickPosition;
        float atDestinationLimit=2f;
        float rotationAngle;
        float currentAngle;
        float AngleLimit=0.03f;
        float moveAngle;
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
            currentPosition = prePosition;
        }

        public override void Update(GameTime gametime)
        {
           // steerRotationValue = (float)Math.Sin(gametime.TotalGameTime.TotalSeconds * 0.75f) * 0.5f;
          //  turretRorationValue = (float)Math.Sin(gametime.TotalGameTime.TotalSeconds);
            if ( Mouse.GetState().LeftButton == ButtonState.Pressed&&mousepick.GetCollisionPosition().HasValue==true)
            {
                pickPosition = mousepick.GetCollisionPosition().Value;
            }
                            distanceTopickPosition = Vector3.Distance(pickPosition, currentPosition);
            if (distanceTopickPosition > atDestinationLimit)
            {
                wheelRotationValue = (float)gametime.TotalGameTime.TotalSeconds * speed;
                canonRotationValue = (float)Math.Sin(gametime.TotalGameTime.TotalSeconds * 0.25f) * 0.333f - 0.333f;
                hatchRotationValue = MathHelper.Clamp((float)Math.Sin(gametime.TotalGameTime.TotalSeconds * 2) * 2, -1, 0);
                moveDirection = pickPosition - prePosition;
                moveDirection.Normalize();
                if (moveDirection.Z <=0)
                    rotationAngle = (float)Math.Atan(moveDirection.X / moveDirection.Z) + MathHelper.Pi;
                else
                    rotationAngle = (float)Math.Atan(moveDirection.X / moveDirection.Z);
                moveAngle = rotationAngle - currentAngle;
                if (moveAngle > AngleLimit)
                {
                    currentAngle += rotateSpeed;
                    rotation = Matrix.CreateRotationY(currentAngle);
                }
                else if (moveAngle < 0 && Math.Abs(moveAngle) > AngleLimit)
                {
                    currentAngle -= rotateSpeed;
                    rotation = Matrix.CreateRotationY(currentAngle);
                }
                else
                {
                    currentPosition = moveDirection * speed * (float)gametime.ElapsedGameTime.TotalSeconds + prePosition;
                    prePosition = currentPosition;
                    translation = Matrix.CreateTranslation(currentPosition);

                }
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
            canongeo.Transform = canonRotation * canonTransform;
                base.Draw(device, camera);
        }
        protected override Matrix GetWorld()
        {
            return Matrix.CreateScale(.05f)*rotation*translation;
        }
    }
}

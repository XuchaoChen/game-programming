using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lab2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;
        //VertexPositionColor[] verts;
        VertexPositionNormalTexture[] verts;
        short[] indices;
        VertexBuffer vertexbuffer;
        IndexBuffer indexBuffer;
        BasicEffect effect;
        MouseState currentMouse;
        Matrix translation = Matrix.Identity;
        Matrix rotation = Matrix.Identity;
        Matrix scale = Matrix.Identity;
        Texture2D texture;
        float prevWheelValue=0;
        float currWheelValue;
        float scaleCount;
        float prePositionX = 0;
        float currPositionX;
        float PositionXCount;
        float prePositionY = 0;
        float currPositionY;
        float PositionYCount;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            camera = new Camera(this,new Vector3(0,0,5),Vector3.Zero,new Vector3(0,1,0));
            Components.Add(camera);
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        { 
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //position
            Vector3 FrontTopLeft = new Vector3(-1, 1, 1);
            Vector3 FrontTopRight = new Vector3(1, 1, 1);
            Vector3 FrontBottomRight = new Vector3(1, -1, 1);
            Vector3 FrontBottomLeft = new Vector3(-1, -1, 1);
            Vector3 BackTopLeft = new Vector3(-1,1,-1);
            Vector3 BackTopRight = new Vector3(1, 1, -1);
            Vector3 BackBottomRight = new Vector3(1, -1, -1);
            Vector3 BackBottomLeft = new Vector3(-1, -1, -1);

            //normal
            Vector3 frontNormal = new Vector3(0, 0, 1);
            Vector3 backNormal = new Vector3(0, 0, -1);
            Vector3 rightNormal = new Vector3(1, 0, 0);
            Vector3 leftNormal = new Vector3(-1, 0, 0);
            Vector3 topNormal = new Vector3(0, 1, -1);
            Vector3 bottomNormal = new Vector3(0, -1, 0);

            //textureCoordinate front
            Vector2 front_topleft = new Vector2(0, 0);
            Vector2 front_topright = new Vector2(0.5f, 0);
            Vector2 front_bottomright = new Vector2(0.5f, 0.5f);
            Vector2 front_bottomleft = new Vector2(0, 0.5f);

            //textureCoordinate back
            Vector2 back_topleft = new Vector2(0.5f, 0);
            Vector2 back_topright = new Vector2(1.0f, 0);
            Vector2 back_bottomright = new Vector2(1.0f, 0.5f);
            Vector2 back_bottomleft = new Vector2(0.5f, 0.5f);

            //textureCoordinate right
            Vector2 right_topleft = new Vector2(0.5f, 0);
            Vector2 right_topright = new Vector2(1.0f, 0);
            Vector2 right_bottomright = new Vector2(1.0f, 0.5f);
            Vector2 right_bottomleft = new Vector2(0.5f, 0.5f);

            //textureCoordinate left
            Vector2 left_topleft = new Vector2(0, 0);
            Vector2 left_topright = new Vector2(0.5f, 0);
            Vector2 left_bottomright = new Vector2(0.5f, 0.5f);
            Vector2 left_bottomleft = new Vector2(0, 0.5f);
            //textureCoordinate top
            Vector2 top_topleft = new Vector2(0, 0.5f);
            Vector2 top_topright = new Vector2(0.5f, 0.5f);
            Vector2 top_bottomright = new Vector2(0.5f, 1.0f);
            Vector2 top_bottomleft = new Vector2(0, 1.0f);
            //textureCoordinate bottom
            Vector2 bottom_topleft = new Vector2(0.5f, 0.5f);
            Vector2 bottom_topright = new Vector2(1.0f, 0.5f);
            Vector2 bottom_bottomright = new Vector2(1.0f, 1.0f);
            Vector2 bottom_bottomleft = new Vector2(0.5f, 1.0f);


            verts = new VertexPositionNormalTexture[24];
            //front plane
            verts[0] = new VertexPositionNormalTexture(FrontTopLeft, frontNormal,front_topleft);
            verts[1] = new VertexPositionNormalTexture(FrontTopRight, frontNormal,front_topright);
            verts[2] = new VertexPositionNormalTexture(FrontBottomRight,frontNormal,front_bottomright);
            verts[3] = new VertexPositionNormalTexture(FrontBottomLeft, frontNormal, front_bottomleft);

            //right plane
            verts[4] = new VertexPositionNormalTexture(FrontTopRight, rightNormal, right_topleft);
            verts[5] = new VertexPositionNormalTexture(BackTopRight, rightNormal, right_topright);
            verts[6] = new VertexPositionNormalTexture(BackBottomRight, rightNormal, right_bottomright);
            verts[7] = new VertexPositionNormalTexture(FrontBottomRight, rightNormal, right_bottomleft);

            //top plane
            verts[8] = new VertexPositionNormalTexture(BackTopLeft, topNormal, top_topleft);
            verts[9] = new VertexPositionNormalTexture(BackTopRight, topNormal, top_topright);
            verts[10] = new VertexPositionNormalTexture(FrontTopRight, topNormal, top_bottomright);
            verts[11] = new VertexPositionNormalTexture(FrontTopLeft, topNormal, top_bottomleft);

            //back plane
            verts[12] = new VertexPositionNormalTexture(BackBottomLeft,backNormal,back_topleft);
            verts[13] = new VertexPositionNormalTexture(BackBottomRight, backNormal, back_topright);
            verts[14] = new VertexPositionNormalTexture(BackTopRight, backNormal,back_bottomright);
            verts[15] = new VertexPositionNormalTexture(BackTopLeft, backNormal, back_bottomleft);

            //bottom plane
            verts[16] = new VertexPositionNormalTexture(FrontBottomLeft, bottomNormal, bottom_topleft);
            verts[17] = new VertexPositionNormalTexture(FrontBottomRight, bottomNormal, bottom_topright);
            verts[18] = new VertexPositionNormalTexture(BackBottomRight, bottomNormal, bottom_bottomright);
            verts[19] = new VertexPositionNormalTexture(BackBottomLeft, bottomNormal, bottom_bottomleft);

            //left plane
            verts[20] = new VertexPositionNormalTexture(BackTopLeft, leftNormal, left_topleft);
            verts[21] = new VertexPositionNormalTexture(FrontTopLeft, leftNormal, left_topright);
            verts[22] = new VertexPositionNormalTexture(FrontBottomLeft, leftNormal, left_bottomright);
            verts[23] = new VertexPositionNormalTexture(BackBottomLeft, leftNormal, left_bottomleft);
            indices = new short[36];
            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            indices[3] = 0;
            indices[4] = 2;
            indices[5] = 3;
            indices[6] = 4;
            indices[7] = 5;
            indices[8] = 6;
            indices[9] = 4;
            indices[10] = 6;
            indices[11] = 7;
            indices[12] = 8;
            indices[13] = 9;
            indices[14] = 10;
            indices[15] = 8;
            indices[16] = 10;
            indices[17] = 11;
            indices[18] = 12;
            indices[19] = 13;
            indices[20] = 14;
            indices[21] = 12;
            indices[22] = 14;
            indices[23] = 15;
            indices[24] = 16;
            indices[25] = 17;
            indices[26] = 18;
            indices[27] = 16;
            indices[28] = 18;
            indices[29] = 19;
            indices[30] = 20;
            indices[31] = 21;
            indices[32] = 22;
            indices[33] = 20;
            indices[34] = 22;
            indices[35] = 23;
            vertexbuffer = new VertexBuffer(GraphicsDevice,typeof(VertexPositionNormalTexture),verts.Length,BufferUsage.None);
            vertexbuffer.SetData(verts);

            indexBuffer = new IndexBuffer(GraphicsDevice, IndexElementSize.SixteenBits, 
                sizeof(short) * indices.Length, BufferUsage.None);
            indexBuffer.SetData(indices);
            GraphicsDevice.Indices = indexBuffer;
            effect = new BasicEffect(GraphicsDevice);
            texture = Content.Load<Texture2D>(@"Texture/crate(1)");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            currentMouse = Mouse.GetState();
            prePositionX = currPositionX;
            prePositionY = currPositionY;
            currPositionX = currentMouse.Position.X;
            currPositionY = currentMouse.Position.Y;
          if (currentMouse.LeftButton == ButtonState.Pressed&&currPositionX>prePositionX)
            {
                rotation *= Matrix.CreateRotationY(MathHelper.PiOver4/5);
            }
            if (currentMouse.LeftButton == ButtonState.Pressed && currPositionX<prePositionX)
            {
                rotation *= Matrix.CreateRotationY(-MathHelper.PiOver4/5);
            }
           if (currentMouse.LeftButton == ButtonState.Pressed && currPositionY>prePositionY)
            {
                rotation *= Matrix.CreateRotationX(MathHelper.PiOver4/5);
            }
            if (currentMouse.LeftButton == ButtonState.Pressed && currPositionY< prePositionY)
            {
                rotation *= Matrix.CreateRotationX(-MathHelper.PiOver4/5);
            }
            prevWheelValue = currWheelValue;
            currWheelValue =currentMouse.ScrollWheelValue;
            scaleCount = currWheelValue - prevWheelValue;
            if (currWheelValue > prevWheelValue)
            {
                scale += Matrix.CreateScale(scaleCount/80f);

            }
            else if(currWheelValue<prevWheelValue)
            {
                scale += Matrix.CreateScale(scaleCount/80f);
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GraphicsDevice.SetVertexBuffer(vertexbuffer);

            //effect.World = Matrix.Identity;
            effect.World = scale * rotation * translation;
            effect.View = camera.view;
            effect.Projection = camera.projection;
            effect.VertexColorEnabled = false;
            effect.Texture = texture;
            effect.TextureEnabled = true;
            effect.EnableDefaultLighting();
            effect.LightingEnabled = true;
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList,0,0,8,0,12);
            }
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

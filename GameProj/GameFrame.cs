using GameProj.Core;
using GameProj.Core.Core;
using GameProj.Core.Environment;
using GameProj.Core.Graphics;
using GameProj.Core.UIRoot;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameProj
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameFrame : Game
    {
        GraphicsDeviceManager graphics { get; set; }
        public static ExtendedSpriteBatch spriteBatch { get; set; }
       
        World World { get; set; }

        public GameFrame()
        {
            
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
         //   graphics.ToggleFullScreen();
            this.IsMouseVisible = true;

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
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new ExtendedSpriteBatch(GraphicsDevice);
             
            CameraManager.Init(GraphicsDevice.Viewport);
            GameCore.Initialize(Content, spriteBatch);
            World = new World(spriteBatch);
            // CameraManager.AddToPosition(-200, -200);
    

     
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            GameCore.OnExit(World);
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            InputHandler.Handle(World, Mouse.GetState(), Keyboard.GetState());
            CameraManager.Update(gameTime);
            World.Update(gameTime);
            GameCore.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.White);
            World.Draw(gameTime);
            GameCore.Draw(gameTime);


            GameCore.Batch.Begin();
            GameCore.Batch.Draw(GameCore.Load("back").Gfx, spriteBatch.GraphicsDevice.Viewport.Bounds, Color.White*tr);
            GameCore.Batch.End();
            tr -= 0.005f;
            base.Draw(gameTime);

        }
        public float tr = 1;
    }
}


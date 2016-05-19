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
            spriteBatch = new ExtendedSpriteBatch(GraphicsDevice); // ExtendedSpriteBatch permet de dessiner des objets à l'écran

            CameraManager.Init(GraphicsDevice.Viewport);  // Charge la caméra
            GameCore.Initialize(Content, spriteBatch); // Charge les fichiers du jeu
            World = new World(spriteBatch); // Instancie la variable de type World, qui gère tout l'environement du jeu



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

            GraphicsDevice.Clear(Color.CornflowerBlue);
            World.Draw(gameTime);
            GameCore.Draw(gameTime);
            GameCore.Batch.Begin();
            GameCore.Batch.Draw(GameCore.Load("back").Gfx, spriteBatch.GraphicsDevice.Viewport.Bounds, Color.White * tr);
            GameCore.Batch.End();
            tr -= 0.005f;
            base.Draw(gameTime);

        }
        public float tr = 1;
    }
}


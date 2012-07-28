using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Protogame;
using Tychaia.Title;

namespace Tychaia
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class RuntimeGame : CoreGame<TitleWorld, IsometricWorldManager>
    {
        public static GraphicsDevice DeviceForStateValidationOutput = null;
        public static GameContext ContextForStateValidationOutput = null;
        public static object LockForStateValidationOutput = new object();

        public RuntimeGame()
        {
            this.m_GameContext.Graphics.PreferredBackBufferWidth = 1600;
            this.m_GameContext.Graphics.PreferredBackBufferHeight = 800;

            // Set the Ogmo Editor to focus on this object.
            //OgmoConnect.FocusedObject = new OgmoState(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            this.Window.Title = "Tychaia";
            this.IsMouseVisible = true;
            DeviceForStateValidationOutput = this.GraphicsDevice;
            ContextForStateValidationOutput = this.m_GameContext;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Load protogame's content.
            base.LoadContent();

            // Load all the textures.
            this.m_GameContext.LoadFont("Arial");
            this.m_GameContext.LoadFont("SmallArial");
            this.m_GameContext.LoadFont("TitleFont");
            this.m_GameContext.LoadFont("SubtitleFont");
            this.m_GameContext.LoadFont("ButtonFont");
            this.m_GameContext.LoadTexture("tiles.water");
            this.m_GameContext.LoadTexture("tiles.grass");
            this.m_GameContext.LoadTexture("tiles.grass_back");
            this.m_GameContext.LoadTexture("tiles.snow");
            this.m_GameContext.LoadTexture("tiles.lava");
            this.m_GameContext.LoadTexture("tiles.stone");
            this.m_GameContext.LoadTexture("tiles.dirt");
            this.m_GameContext.LoadTexture("tiles.sand");
            this.m_GameContext.LoadTexture("tiles.trunk");
            this.m_GameContext.LoadTexture("tiles.leaf");
            this.m_GameContext.LoadTexture("tiles.leafgrey");
            this.m_GameContext.LoadTexture("tiles.grassleaf");
            this.m_GameContext.LoadTexture("tiles.sandgrass");
            this.m_GameContext.LoadTexture("chars.player.player");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            base.UnloadContent();
        }

        protected override void Draw(GameTime gameTime)
        {
            lock (LockForStateValidationOutput)
            {
 	            base.Draw(gameTime);
            }
        }
    }
}

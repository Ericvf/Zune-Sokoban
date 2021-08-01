using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Zune_Sokoban
{
    public class GameScreen 
    {
        /// <summary>
        /// Private fields
        /// </summary>
        protected InputManager input;
        protected ScreensManager screenManager;
        protected SpriteBatch spriteBatch;

        protected ContentManager contentManager;
        protected GraphicsDevice graphicsDevice;

        /// <summary>
        /// Properties
        /// </summary>
        public ScreensManager ScreenManager
        {
            get
            {
                return this.screenManager;
            }
            set
            {
                this.screenManager = value;
                this.input = (value.Game.Services.GetService(typeof(IInputService)) as IInputService).input;
            }
        }

        /// <summary>
        /// Activates the screen
        /// </summary>
        public void Activate()
        {
            // Check for valid manager
            if (this.ScreenManager == null)
                throw new ApplicationException("ScreenManager cannot be null");

            // Activate this screen in the manager
            this.ScreenManager.ActivateScreen(this);
        }

        public virtual void LoadContent()
        {
            // Create a new spriteBatch
            this.spriteBatch = new SpriteBatch(this.graphicsDevice);
        }

        /// <summary>
        /// Initializes the screen
        /// </summary>
        public virtual void Initialize(ContentManager content, GraphicsDevice graphics)
        {
            // Save the content and graphics
            this.contentManager = content;
            this.graphicsDevice = graphics;   
        }
        
        /// <summary>
        /// Updates the screen
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Draws the screen
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zune.Games.Sokoban.WindowsGame.Transitions
{
    public class Transition
    {
        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D tile)
        {
        }
    }
}

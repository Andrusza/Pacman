using System.Windows.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SilverlightApplication3
{
    public class Game
    {
        private readonly SpriteBatch spriteBatch;
        private readonly ContentManager contentManager;

        public Game()
        {
            contentManager = new ContentManager(null, "Content");
            spriteBatch = new SpriteBatch(GraphicsDeviceManager.Current.GraphicsDevice);

            //contentManager.Load<Texture2D>("s");
        }
    }
}
using System.Windows.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Silverlight3dApp
{
    public class Coin
    {
        private Texture2D texture;
        private Rectangle bounds;

        public int pointValue = 30;

        public Coin(Texture2D texture, Vector2 position, Vector2 size)
        {
            this.texture = texture;
            int width = (int)size.X / 2;
            int height = (int)size.Y / 2;
            position += new Vector2(width / 2, height / 2);

            bounds = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        public void Draw(DrawEventArgs drawEventArgs, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, bounds, Color.White);
        }
    }
}
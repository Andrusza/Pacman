using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Silverlight3dApp.Base;

namespace Silverlight3dApp.Ghosts
{
    public class DumpEnemy : GhostBase
    {
        private System.Random rand = new System.Random();

        public DumpEnemy(Tile curentTile, ContentManager content)
            : base(curentTile, content)
        {
            texture = content.Load<Texture2D>("pac");
            font = content.Load<SpriteFont>("font");
        }

        public void Update()
        {
            if (this.direction.X == 0)
            {
                this.direction.X = rand.Next(-1, 2);
            }
            if (this.direction.Y == 0)
            {
                this.direction.Y = rand.Next(-1, 2);
            }

            UpdateColisionTiles();

            if (movingMode)
            {
                Move(ref horizontal, ref vertical, ref bounds.X, ref bounds.Y, ref direction.X, ref direction.Y, ref position.X, ref position.Y);
            }
            else
            {
                Move(ref vertical, ref horizontal, ref bounds.Y, ref bounds.X, ref direction.Y, ref direction.X, ref position.Y, ref position.X);
            }
        }
    }
}
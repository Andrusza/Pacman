using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Silverlight3dApp.Base;
using Silverlight3dApp.Pacman;

namespace Silverlight3dApp.Ghosts
{
    public class SmaerEnemy : GhostBase
    {
        private System.Random rand = new System.Random();

        public SmaerEnemy(Tile curentTile, ContentManager content)
            : base(curentTile, content)
        {
            texture = content.Load<Texture2D>("pac");
            font = content.Load<SpriteFont>("font");
        }

        public void HuntPlayer(Player p)
        {

        }

        public void Update()
        {
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
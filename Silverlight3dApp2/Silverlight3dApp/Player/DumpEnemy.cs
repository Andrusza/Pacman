using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Silverlight3dApp.Base;

namespace Silverlight3dApp.Ghosts
{
    public class DumpEnemy : GhostBase
    {
        private static System.Random rand = new System.Random();

        public DumpEnemy(Tile curentTile, ContentManager content)
            : base(curentTile, content)
        {
            font = content.Load<SpriteFont>("font");
            htexture = content.Load<Texture2D>("pac3");
            vtexture = content.Load<Texture2D>("pac4");
            directionTexture = htexture;
            Maze.PositionInMaze(this);
        }

        public void RandomDir()
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
        }

        public override void Update()
        {
            RandomDir();

            base.Update();
        }
    }
}
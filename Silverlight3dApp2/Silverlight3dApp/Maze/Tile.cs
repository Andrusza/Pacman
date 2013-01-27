using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Silverlight3dApp.Utility;

namespace Silverlight3dApp
{
    public enum TileCollision
    {
        Passable = 0,
        Impassable = 1,
    }

    //Param. for A* alghoritm
    public class AstarParam
    {
        public Tile parent;
        public bool open = false;
        public bool closed = false;
        public int g = 0;
        public int h = 0;
        public int f = int.MaxValue;
    }

    public class Tile
    {
        public Texture2D texture;
        public TileCollision tileType;
        public Rectangle bounds; //bounding box
        public Vector2 position; //position in grid
        public AstarParam pathfindingParm;
        public Coin coin;

        public static int Width { get; set; }

        public static int Height { get; set; }

        public static Vector2 Size { get; set; }

        public Tile(Texture2D texture, TileCollision collision, Rectangle bounds, Vector2 position)
        {
            this.texture = texture;
            this.tileType = collision;
            this.bounds = bounds;
            this.position = position;
            this.pathfindingParm = new AstarParam();
        }
    }
}
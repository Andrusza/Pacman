using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SilverlightApplication3
{
    internal enum TileCollision
    {
        Passable = 0,
        Impassable = 1,
    }

    internal struct Tile
    {
        public Texture2D Texture;
        public TileCollision Collision;

        public const int XCount = 14;
        public const int YCount = 12;

        public static int Width { get; set; }

        public static int Height { get; set; }

        public static Vector2 Size { get; set; }

        public Tile(Texture2D texture, TileCollision collision)
        {
            Texture = texture;
            Collision = collision;
        }
    }
}
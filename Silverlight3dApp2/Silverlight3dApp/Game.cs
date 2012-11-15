using System.Windows.Controls;
using System.Windows.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Silverlight3dApp.Pacman;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;
using Silverlight3dApp.Ghosts;
using Silverlight3dApp.Pathfinding;

namespace Silverlight3dApp
{
    public class Game
    {
        private readonly SpriteBatch spriteBatch;
        private readonly ContentManager contentManager;
        private Maze maze;
        private Player player;
        private DumpEnemy enemy;

        public Game()
        {
            contentManager = new ContentManager(null, "Content");
            spriteBatch = new SpriteBatch(GraphicsDeviceManager.Current.GraphicsDevice);

            maze = new Maze();
            player = new Player(maze.GetTile(12, 1), contentManager);   
            player.maze = maze;
            maze.PositionInMaze(player);

            enemy = new DumpEnemy(maze.GetTile(12, 2), contentManager);
            enemy.maze = maze;
            maze.PositionInMaze(enemy);
            Astar a = new Astar(maze, maze.GetTile(12, 1), maze.GetTile(15, 1));
            a = new Astar(maze, maze.GetTile(12, 1), maze.GetTile(15, 1));
        }

        public void Update(DrawEventArgs drawEventArgs)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            player.Update(keyboardState);
            enemy.Update();
        }

        public void Draw(DrawEventArgs drawEventArgs)
        {
            GraphicsDeviceManager.Current.GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend);

            maze.Draw(drawEventArgs, spriteBatch);
            //player.Draw(spriteBatch);
            enemy.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
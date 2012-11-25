using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Silverlight3dApp.Base;
using Silverlight3dApp.Ghosts;
using Silverlight3dApp.Pacman;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;

namespace Silverlight3dApp
{
    public class Game
    {
        private readonly SpriteBatch spriteBatch;
        private readonly ContentManager contentManager;
        private Maze maze;

        private List<GhostBase> listOfEnemies;

        public Game()
        {
            contentManager = new ContentManager(null, "Content");
            spriteBatch = new SpriteBatch(GraphicsDeviceManager.Current.GraphicsDevice);
            listOfEnemies = new List<GhostBase>();

            maze = new Maze();
            Player.CreatInstance(maze.GetTile(19, 1), contentManager);

            listOfEnemies.Add(new DumpEnemy(maze.GetTile(1, 1), contentManager));
            listOfEnemies.Add(new DumpEnemy(maze.GetTile(15, 1), contentManager));
            listOfEnemies.Add(new DumpEnemy(maze.GetTile(26, 1), contentManager));
            listOfEnemies.Add(new DumpEnemy(maze.GetTile(5, 1), contentManager));
            listOfEnemies.Add(new DumpEnemy(maze.GetTile(12, 1), contentManager));
            listOfEnemies.Add(new DumpEnemy(maze.GetTile(20, 27), contentManager));
            listOfEnemies.Add(new DumpEnemy(maze.GetTile(8, 8), contentManager));
            listOfEnemies.Add(new DumpEnemy(maze.GetTile(10, 15), contentManager));

            listOfEnemies.Add(new SmartEnemy(maze.GetTile(15, 1), contentManager));
            listOfEnemies.Add(new SmartEnemy(maze.GetTile(5, 26), contentManager));
        }

        public void Update(DrawEventArgs drawEventArgs)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            Player.GetInstance.Update(keyboardState);
            foreach (GhostBase x in listOfEnemies)
            {
                x.Update();
                if (Player.GetInstance.CheckPlayerMazeCollision(x.CurrentTile))
                {
                    int lol = 1;
                }
            }
        }

        public void Draw(DrawEventArgs drawEventArgs)
        {
            GraphicsDeviceManager.Current.GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            maze.Draw(drawEventArgs, spriteBatch);
            Player.GetInstance.Draw(spriteBatch);

            foreach (GhostBase x in listOfEnemies)
            {
                x.Draw(spriteBatch);
            }

            spriteBatch.End();
        }
    }
}
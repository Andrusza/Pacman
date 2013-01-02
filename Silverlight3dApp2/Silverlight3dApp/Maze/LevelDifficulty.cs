using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Silverlight3dApp.Base;

namespace Silverlight3dApp
{
    public enum Difficulty
    {
        Easy = 0,
        Normal = 1,
        Hard = 2
    }

    public class LevelDifficulty
    {
        public List<GhostBase> listOfEnemies = new List<GhostBase>();
        public int numberOfDumpEnemies = 0;
        public int numberOfSmartEnemies = 0;
        public Difficulty difficult;

        public LevelDifficulty(Difficulty difficult)
        {
            switch (difficult)
            {
                case Difficulty.Easy:
                    {
                        Easy();
                        break;
                    }
                case Difficulty.Normal:
                    {
                        Normal();
                        break;
                    }
                case Difficulty.Hard:
                    {
                        Hard();
                        break;
                    }
            }
        }

        private void Easy()
        {
            numberOfDumpEnemies = 5;
        }

        private void Normal()
        {
            numberOfDumpEnemies = 5;
            numberOfSmartEnemies = 2;
        }

        private void Hard()
        {
            numberOfSmartEnemies = 5;
        }
    }
}
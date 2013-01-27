using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Silverlight3dApp.Utility
{
    public class Pair
    {
        int score;

        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        string playerName;

        public string PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }

        public Pair(int score, string playerName)
        {
            this.score = score;
            this.playerName = playerName;
        }
    }
}

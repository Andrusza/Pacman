using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;
using Silverlight3dApp.Utility;
using Silverlight3dApp.Pacman;

namespace Silverlight3dApp.Xaml
{
    public partial class GameOver
    {
        public GameOver()
        {
            InitializeComponent();
            label2.Content = "Player score: "+Player.Score.ToString();
            FillTextBox();
        }

        private void FillTextBox()
        {
            Hscore.ReadFromIsolatedStorage();
            textBlock1.Text = "";
            textBlock1.Inlines.Add("**HIGHSCORE**");
            textBlock1.Inlines.Add(new LineBreak());
            int num = 0;
            foreach (Pair x in Hscore.Highscore)
            {
                textBlock1.Inlines.Add(x.PlayerName + ": " + x.Score.ToString());
                textBlock1.Inlines.Add(new LineBreak());
                num++;
            }
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new Start();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                Hscore.WriteToIsolatedStorage(Player.Score, textBox1.Text);
                FillTextBox();
            }
        }
    }
}
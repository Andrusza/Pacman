using System.Windows;
using System.Windows.Navigation;
using Microsoft.Xna.Framework.Input;

namespace Silverlight3dApp.Xaml
{
    public partial class Start
    {
        public Start()
        {
            Mouse.RootControl = this;
            Keyboard.RootControl = this;
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void bStart_Click(object sender, RoutedEventArgs e)
        {
            Difficulty difficulty;

            if (radioButton1.IsChecked == true)
            {
                difficulty = Difficulty.Easy;
            }

            else if (radioButton1.IsChecked == true)
            {
                difficulty = Difficulty.Normal;
            }

            else
            {
                difficulty = Difficulty.Hard;
            }

            this.Content = new MainPage(difficulty);
            //this.Content = new GameOver();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new GameOver();
        }
    }
}
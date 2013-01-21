using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;

namespace Silverlight3dApp.Utility
{
    public class Hscore
    {
        private static List<int> highscore = new List<int>();

        public static List<int> Highscore
        {
            get { return Hscore.highscore; }
            set { Hscore.highscore = value; }
        }

        public static void WriteToIsolatedStorage(int score)
        {
            IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream("highscore.txt", FileMode.Create, isolatedStorageFile);

            Highscore.Add(score);
            Highscore.Sort();
            Highscore.Reverse();
            if (Highscore.Count > 10) Highscore.RemoveAt(Highscore.Count - 1);

            StreamWriter streamWriter = new StreamWriter(isolatedStorageFileStream);
            foreach (int x in Highscore)
            {
                streamWriter.WriteLine(x);
            }

            //close everything
            streamWriter.Close();
            isolatedStorageFileStream.Close();
        }

        public static void ReadFromIsolatedStorage()
        {
            IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();

            string[] fileNames = isolatedStorageFile.GetFileNames("highscore.txt");
            if (fileNames.Length == 0)
            {
                Console.WriteLine("User has saved no data yet.");
            }
            else
            {
                Console.WriteLine("Contents of UserSettings.set:");
                IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream("highscore.txt", FileMode.Open, isolatedStorageFile);
                StreamReader streamReader = new StreamReader(isolatedStorageFileStream);

                while (streamReader.Peek() >= 0)
                {
                    int x = Int32.Parse(streamReader.ReadLine());
                    Highscore.Add(x);
                }

                streamReader.Close();
                isolatedStorageFileStream.Close();
            }
        }
    }
}
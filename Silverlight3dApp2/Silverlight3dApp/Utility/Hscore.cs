using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;

namespace Silverlight3dApp.Utility
{
    public class Hscore
    {
        private static List<Pair> highscore = new List<Pair>();

        public static List<Pair> Highscore
        {
            get { return Hscore.highscore; }
            set { Hscore.highscore = value; }
        }

        public static void WriteToIsolatedStorage(int score, string name)
        {
            IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream("highscore2.txt", FileMode.Create, isolatedStorageFile);

            Highscore.Add(new Pair(score, name));
            Highscore.Sort((a, b) => a.Score.CompareTo(b.Score));
            Highscore.Reverse();
            if (Highscore.Count > 10) Highscore.RemoveAt(Highscore.Count - 1);

            StreamWriter streamWriter = new StreamWriter(isolatedStorageFileStream);
            foreach (Pair x in Highscore)
            {
                streamWriter.WriteLine(x.PlayerName);
                streamWriter.WriteLine(x.Score);
            }

            //close everything
            streamWriter.Close();
            isolatedStorageFileStream.Close();
        }

        public static void ReadFromIsolatedStorage()
        {
            IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();
            highscore = new List<Pair>();

            string[] fileNames = isolatedStorageFile.GetFileNames("highscore2.txt");
            if (fileNames.Length == 0)
            {
                Console.WriteLine("User has saved no data yet.");
            }
            else
            {
                IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream("highscore2.txt", FileMode.Open, isolatedStorageFile);
                StreamReader streamReader = new StreamReader(isolatedStorageFileStream);

                while (streamReader.Peek() >= 0)
                {
                    string n = streamReader.ReadLine();
                    int x = Int32.Parse(streamReader.ReadLine());
                    Highscore.Add(new Pair(x, n));
                }

                streamReader.Close();
                isolatedStorageFileStream.Close();
            }
        }
    }
}
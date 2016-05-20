using System;

namespace KKP
{
    class Program
    {
        static void Main()
        {
            Problem2();
            Problem3();
        }

        private static void Problem2()
        {
            Console.WriteLine("*** Problem 2 ***");

            var twoTrains = new TwoTrains();

            twoTrains.CalculateTimeAndDistance(70m, 60m, 260m);
            twoTrains.PrintResults();

            twoTrains.CalculateTimeAndDistance(1150.75m, 88.88m, 97530.254m);
            twoTrains.PrintResults();

            Console.ReadLine();
        }

        private static void Problem3()
        {
            Console.WriteLine("*** Problem 3 ***");

            var matchWords = new MatchWords();

            matchWords.Search("StartBurst");
            matchWords.PrintMatchingWords();

            matchWords.Search("RedEye");
            matchWords.PrintMatchingWords();

            Console.ReadLine();
        }
    }
}

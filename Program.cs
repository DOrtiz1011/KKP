﻿using System;
using KKP.WordSearch;

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
            twoTrains.CalculateTimeAndDistance(150.75m, 88.88m, 753.254m);

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

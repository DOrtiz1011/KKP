using System;
using KKP.WordSearch;

namespace KKP
{
    class Program
    {
        static void Main()
        {
            var matchWords = new MatchWords();

            var wordList = matchWords.Search("StartBurst");

            foreach (var word in wordList)
            {
                Console.WriteLine(word);
            }

            Console.ReadLine();
        }
    }
}

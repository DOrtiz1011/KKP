using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KKP
{
    internal sealed class MatchWords
    {
        #region Properties

        private List<string> MatchingWordList { get; set; }

        private string SearchWord { get; set; }
        private Dictionary<char, int> SearchWordCharCount { get; set; }

        private Dictionary<string, List<string>> WordHash
        {
            get
            {
                if (_wordHash == null)
                {
                    _wordHash = new Dictionary<string, List<string>>();
                    AddTestWordsToHash();
                }

                return _wordHash;
            }
        }

        #endregion Properties

        #region Fields

        private Dictionary<string, List<string>> _wordHash;

        #endregion Fields

        #region Public Methods

        public void Search(string searchWord)
        {
            if (string.IsNullOrEmpty(searchWord))
            {
                throw new ApplicationException("Word is invalid.");
            }

            SearchWord = searchWord.ToLower();
            SearchWordCharCount = GetCharCountFromString(SearchWord);

            GetMatchingWords();
        }

        public void PrintMatchingWords()
        {
            if (MatchingWordList == null || MatchingWordList.Count < 1)
            {
                Console.WriteLine($"\n\nNo matches found for {SearchWord}.");
            }
            else
            {
                Console.WriteLine($"\n\nMatches found for {SearchWord}:");

                foreach (var word in MatchingWordList)
                {
                    Console.WriteLine(word);
                }

                Console.WriteLine($"\n\nEnd of list for {SearchWord}");
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void ResetMatchingWordList()
        {
            if (MatchingWordList != null)
            {
                MatchingWordList.Clear();
                MatchingWordList = null;
            }
        }

        private static string GetHashKey(string word)
        {
            var charArray = word.ToCharArray();
            Array.Sort(charArray);
            return new string(charArray);
        }

        private void AddWordToHash(string newWord)
        {
            //if (string.IsNullOrEmpty(newWord))
            //{
            //    throw new ApplicationException("Word is invalid.");
            //}

            var key = GetHashKey(newWord);

            if (WordHash.ContainsKey(key))
            {
                var wordList = WordHash[key];

                if (!wordList.Contains(newWord))
                {
                    wordList.Add(newWord);
                }
            }
            else
            {
                var wordList = new List<string> { newWord };
                WordHash.Add(key, wordList);
            }
        }

        private void AddTestWordsToHash()
        {
            foreach (var word in File.ReadAllLines("AllEnglishWords.txt").ToList())
            {
                AddWordToHash(word.Trim().ToLower());
            }
        }

        private void GetMatchingWords()
        {
            var wordList = new List<string>();

            foreach (var keyValuePair in WordHash.Where(keyValuePair => IsSubKey(keyValuePair.Key)))
            {
                wordList.AddRange(WordHash[keyValuePair.Key]);
            }

            MatchingWordList = wordList.OrderBy(x => x).ToList();
        }

        private static Dictionary<char, int> GetCharCountFromString(string stringToCount)
        {
            var countDictionary = new Dictionary<char, int>();

            foreach (var character in stringToCount.Trim().ToCharArray())
            {
                if (countDictionary.ContainsKey(character))
                {
                    countDictionary[character]++;
                }
                else
                {
                    countDictionary.Add(character, 1);
                }
            }

            return countDictionary;
        }

        private bool IsSubKey(string key)
        {
            var isSubKey = true;

            // key must be shorter than or equal to the length of the hint phrase
            if (key.Length <= SearchWord.Length)
            {
                var wordCharCountDictionary = GetCharCountFromString(key);

                if ((from keyValuePair in wordCharCountDictionary let character = keyValuePair.Key where !SearchWordCharCount.ContainsKey(character) || keyValuePair.Value > SearchWordCharCount[character] select keyValuePair).Any())
                {
                    isSubKey = false;
                }
            }
            else
            {
                isSubKey = false;
            }

            return isSubKey;
        }

        #endregion Private Methods
    }
}

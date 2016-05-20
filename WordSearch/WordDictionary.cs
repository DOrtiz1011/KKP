using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KKP.WordSearch
{
    class WordDictionary
    {
        public WordDictionary()
        {
            AddTestWords();
        }

        private string SearchWord { get; set; }
        private Dictionary<char, int> SearchWordCharCount { get; set; }

        private Dictionary<string, List<string>> wordHash = new Dictionary<string, List<string>>();

        private string GetHashKey(string word)
        {
            var charArray = word.ToCharArray();
            Array.Sort(charArray);
            return new string(charArray);
        }

        public void Add(string newWord)
        {
            if (string.IsNullOrEmpty(newWord))
            {
                throw new ApplicationException("Word is invalid.");
            }

            var key = GetHashKey(newWord);

            if (wordHash.ContainsKey(key))
            {
                var wordList = wordHash[key];

                if (!wordList.Contains(newWord))
                {
                    wordList.Add(newWord);
                }
            }
            else
            {
                var wordList = new List<string>();
                wordList.Add(newWord);

                wordHash.Add(key, wordList);
            }
        }

        private void AddTestWords()
        {
            foreach (var word in File.ReadAllLines("WordSearch\\wordlist.txt").ToList())
            {
                Add(word.Trim().ToLower());
            }
        }

        public List<string> GetMatchingWords(string searchWord)
        {
            SearchWord = searchWord.ToLower();
            SearchWordCharCount = GetCharCountFromString(SearchWord);

            var wordList = new List<string>();

            foreach (var keyValuePair in wordHash)
            {
                if (IsSubKey(keyValuePair.Key))
                {
                    wordList.AddRange(wordHash[keyValuePair.Key]);
                }
            }

            return wordList;
        }

        private Dictionary<char, int> GetCharCountFromString(string stringToCount)
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


        public bool IsSubKey(string key)
        {
            var isSubKey = true;

            // key must be shorter than or equal to the length of the hint phrase
            if (key.Length <= SearchWord.Length)
            {
                var wordCharCountDictionary = GetCharCountFromString(key);

                foreach (var keyValuePair in wordCharCountDictionary)
                {
                    var character = keyValuePair.Key;

                    if (!SearchWordCharCount.ContainsKey(character) || keyValuePair.Value > SearchWordCharCount[character])
                    {
                        // if the hash table does have the char or the number of times the char appears is to large the word will be excluded.
                        isSubKey = false;
                        break;
                    }
                }
            }
            else
            {
                isSubKey = false;
            }

            return isSubKey;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KKP
{
    public class MatchWords
    {
        #region Properties

        public List<string> MatchingWordList { get; private set; }

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
                Console.WriteLine("\n\nNo matches found for {0}.", SearchWord);
            }
            else
            {
                Console.WriteLine("\n\nMatches found for {0}:", SearchWord);

                foreach (var word in MatchingWordList)
                {
                    Console.WriteLine(word);
                }

                Console.WriteLine("\n\nEnd of list for {0}", SearchWord);
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

        private string GetHashKey(string word)
        {
            var charArray = word.ToCharArray();
            Array.Sort(charArray);
            return new string(charArray);
        }

        private void AddWordToHash(string newWord)
        {
            if (string.IsNullOrEmpty(newWord))
            {
                throw new ApplicationException("Word is invalid.");
            }

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
                var wordList = new List<string>();
                wordList.Add(newWord);

                WordHash.Add(key, wordList);
            }
        }

        private void AddTestWordsToHash()
        {
            foreach (var word in File.ReadAllLines("wordlist.txt").ToList())
            {
                AddWordToHash(word.Trim().ToLower());
            }
        }

        private void GetMatchingWords()
        {
            var wordList = new List<string>();

            foreach (var keyValuePair in WordHash)
            {
                if (IsSubKey(keyValuePair.Key))
                {
                    wordList.AddRange(WordHash[keyValuePair.Key]);
                }
            }

            MatchingWordList = wordList.OrderBy(x => x).ToList();
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

        private bool IsSubKey(string key)
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

        #endregion Private Methods
    }
}

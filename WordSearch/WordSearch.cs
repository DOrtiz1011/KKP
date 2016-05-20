using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKP.WordSearch
{
    public class MatchWords
    {
        private WordDictionary wordDictionary = new WordDictionary();

        public List<string> Search(string searchWord)
        {
            var wordList = default(List<string>);

            wordList = wordDictionary.GetMatchingWords(searchWord).OrderBy(x => x).ToList();

            return wordList;
        }
    }
}

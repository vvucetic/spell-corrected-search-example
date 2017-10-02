using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Autosuggest
{
    public class SpellChecker
    {
        private Dictionary<string, int> _dict = new Dictionary<string, int>();
        private List<string> _knownWords = new List<string>();
        private List<Suggestion> _suggestions = new List<Suggestion>();

        private Dictionary<string, List<int>> _wordTermIndex = new Dictionary<string, List<int>>();
        private List<string> _terms = new List<string>();

        public SpellChecker()
        {
            //add empty to increase index to 1, needed for dual meaning of each index
            _knownWords.Add("");
            _suggestions.Add(new Suggestion());
        }

        public IEnumerable<string> Edits1(string word)
        {
            var splits = from i in Enumerable.Range(0, word.Length)
                         select new { a = word.Substring(0, i), b = word.Substring(i) };
            var deletes = from s in splits
                          where s.b != "" // we know it can't be null
                          select s.a + s.b.Substring(1);
            return deletes;
        }

        private IEnumerable<string> ParseWords(string term)
        {
            return Regex.Matches(term.ToLower(), @"\b[\w]+\b").Cast<Match>().Select(m => m.Value);
        }

        public void Train(IEnumerable<string> terms)
        {
            foreach (var term in terms.Distinct())
            {
                var termIndex = AddTerm(term);
                foreach (var word in ParseWords(term).ToList())
                {
                    //build word <-> term index
                    if (_wordTermIndex.ContainsKey(word))
                        _wordTermIndex[word].Add(termIndex);
                    else
                        _wordTermIndex.Add(word, new List<int>() { termIndex });

                    //build edited word <-> word index
                    int knownWordIndex = -1;
                    if (!_dict.ContainsKey(word))
                    {
                        knownWordIndex = AddKnownWords(word);
                        _dict.Add(word, knownWordIndex * -1);
                    }
                    else
                    {
                        continue;
                    }
                    //first edit
                    foreach (var wordEdit1 in Edits1(word).Where(t => !string.IsNullOrEmpty(t)).ToList())
                    {
                        if (!_dict.ContainsKey(wordEdit1))
                        {
                            _dict.Add(wordEdit1, knownWordIndex * -1);
                        }
                        else
                        {
                            var index = _dict[wordEdit1];
                            if (index * -1 != knownWordIndex)
                            {
                                //known word
                                if (index < 0)
                                {
                                    //add new known word
                                    _dict[wordEdit1] = AddNewSuggestion(knownWordIndex);
                                    _suggestions[_dict[wordEdit1]].KnownWords.Add(index * -1);
                                }
                                else
                                {
                                    _suggestions[_dict[wordEdit1]].KnownWords.Add(knownWordIndex);
                                }
                            }
                        }
                        //second edit
                        foreach (var wordEdit2 in Edits1(wordEdit1).Where(t => !string.IsNullOrEmpty(t)).ToList())
                        {
                            if (!_dict.ContainsKey(wordEdit2))
                            {
                                _dict.Add(wordEdit2, knownWordIndex * -1);
                            }
                            else
                            {
                                var index = _dict[wordEdit2];
                                if (index * -1 != knownWordIndex)
                                {
                                    //known word
                                    if (index < 0)
                                    {
                                        //add new known word
                                        _dict[wordEdit2] = AddNewSuggestion(knownWordIndex);
                                        _suggestions[_dict[wordEdit2]].KnownWords.Add(index * -1);
                                    }
                                    else
                                    {
                                        _suggestions[_dict[wordEdit2]].KnownWords.Add(knownWordIndex);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private int AddNewSuggestion(int knownWordIndex)
        {
            _suggestions.Add(new Suggestion()
            {
                KnownWords = new List<int>() { knownWordIndex }
            });
            return _suggestions.Count - 1;
        }

        private int AddKnownWords(string word)
        {
            _knownWords.Add(word);
            return _knownWords.Count - 1;
        }

        private int AddTerm(string term)
        {
            _terms.Add(term);
            return _terms.Count - 1;
        }

        //private int AddKnownWord(string word)
        //{
        //    _knownWords.Add(new KnownWord()
        //    {
        //        Suggestions = new List<int>() { AddSuggestion(word) },
        //        Word = word
        //    });
        //    return _knownWords.Count - 1;
        //}


        public IEnumerable<string> Suggest(string term)
        {
            var dict = new Dictionary<string, int>();
            foreach (var correctedWord in GetAllCorrectedWords(term).Distinct())
            {
                foreach (var termIndex in _wordTermIndex[correctedWord])
                {
                    var existingTerm = _terms[termIndex];
                    if (dict.ContainsKey(existingTerm))
                    {
                        dict[existingTerm]++;
                    }
                    else
                    {
                        dict.Add(existingTerm, 1);
                    }
                }
            }
            return dict.OrderByDescending(t => t.Value).Select(t => t.Key);
        }

        private IEnumerable<string> GetAllCorrectedWords(string term)
        {
            var words = ParseWords(term);
            foreach (var word in words)
            {
                if (_dict.ContainsKey(word))
                {
                    var index = _dict[word];
                    if (index < 0)
                        yield return _knownWords[Math.Abs(index)];
                    else
                    {
                        foreach (var kwIndex in _suggestions[index].KnownWords)
                        {
                            yield return _knownWords[kwIndex];
                        }
                    }
                }
            }
        }
    }

    public class Suggestion
    {
        public List<int> KnownWords { get; set; }
    }

    public class WordTermMapping
    {
        public List<int> LinkedTerms { get; set; }
    }

    public class SuggestionResult
    {
        public string Word { get; set; }

        public int Score { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Anagram_Finder
{
    public class Program
    {
        static void Main(string[] args)
        {
            var wordListPath = @".\ordbok-utf8.txt";
            var baseWordList = File.ReadAllLines(wordListPath).Where(w => !string.IsNullOrEmpty(w)).ToList();
            var anagramsFound = new Dictionary<string, List<string>>();

            foreach (var word in baseWordList)
            {
                var sameLengthWordList = baseWordList.Where(w => w.Length == word.Length).ToList();
                var foundAnagrams = FindAnagrams(word, sameLengthWordList);

                if (foundAnagrams.Count > 0)
                    anagramsFound.Add(word, foundAnagrams);
            }

            PrintFoundAnagramLineToConsole(anagramsFound);

            Console.ReadKey();
        }

        private static List<string> FindAnagrams(string word, List<string> sameLengthWordList)
        {
            var anagramsFound = new List<string>();
            foreach (var checkWord in sameLengthWordList)
            {
                if (IsAnagram(word, checkWord))
                    anagramsFound.Add(checkWord);
            }

            return anagramsFound;
        }

        private static void PrintFoundAnagramLineToConsole(Dictionary<string, List<string>> anagramsFound)
        {
            foreach (var anagramList in anagramsFound)
            {
                var anagramPrintLine = anagramList.Key;
                foreach (var anagram in anagramList.Value)
                {
                    anagramPrintLine = $"{anagramPrintLine} {anagram}";
                }

                Console.WriteLine(anagramPrintLine);
            }
        }

        public static bool IsAnagram(string baseWord, string anagramWord)
        {
            if (baseWord == anagramWord)
                return false;
            return baseWord.OrderBy(c => c).SequenceEqual(anagramWord.OrderBy(c => c));
        }
    }
}

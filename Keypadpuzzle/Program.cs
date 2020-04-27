using System;
using System.Collections.Generic;

namespace Keypadpuzzle
{
    public class Program
    {
        static void Main(string[] args)
        {
            var rules = SetUpRules();
            var answers = new List<string>();
            for (int i = 0; i <= 999; i++)
            {
                var guess = i.ToString("000");
                var guessIsCorrect = true;
                foreach (var rule in rules)
                {
                    if (guess.BreaksRule(rule))
                    {
                        guessIsCorrect = false;
                        break;
                    }
                }

                if (guessIsCorrect)
                {
                    answers.Add(guess);
                }
            }
            foreach (var answer in answers)
            {
                Console.WriteLine($"Correct answer: {answer}");
            }
        }

        static List<Rule> SetUpRules()
        {
            var rules = new List<Rule>
            {
                new Rule("147")
                {
                    NumberOfCorrectDigitsInWrongPlace = 1,
                    NumberOfCorrectDigitsInRightPlace = 0
                },
                new Rule("189")
                {
                    NumberOfCorrectDigitsInWrongPlace = 0,
                    NumberOfCorrectDigitsInRightPlace = 1
                },
                new Rule("964")
                {
                    NumberOfCorrectDigitsInWrongPlace = 2,
                    NumberOfCorrectDigitsInRightPlace = 0
                },
                new Rule("523")
                {
                    NumberOfCorrectDigitsInWrongPlace = 0,
                    NumberOfCorrectDigitsInRightPlace = 0
                },
                new Rule("286")
                {
                    NumberOfCorrectDigitsInWrongPlace = 1,
                    NumberOfCorrectDigitsInRightPlace = 0
                }
            };
            return rules;
        }
    }

    public static class StringExtensions
    {
        public static bool BreaksRule(this string guess, Rule rule)
        {
            if (guess.NumberOfDigitsInRightPlace(rule.Hint) != rule.NumberOfCorrectDigitsInRightPlace)
            {
                return true;
            }
            else if (guess.NumberOfDigitsInWrongPlace(rule.Hint) != rule.NumberOfCorrectDigitsInWrongPlace)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static int NumberOfDigitsInWrongPlace(this string guess, string hint)
        {
            var result = 0;
            for (int i = 0; i < guess.Length; i++)
            {
                for (int j = 0; j < hint.Length; j++)
                {
                    if (guess[i] == hint[j] && i != j)
                        result++;
                }
            }
            return result;
        }

        static int NumberOfDigitsInRightPlace(this string guess, string hint)
        {
            var result = 0;
            for (int i = 0; i < guess.Length; i++)
            {
                if (guess[i] == hint[i])
                    result++;
            }
            return result;
        }
    }

    public class Rule
    {
        public Rule(string hint)
        {
            Hint = hint;
        }

        public string Hint { get; }
        public int NumberOfCorrectDigitsInRightPlace { get; set; }
        public int NumberOfCorrectDigitsInWrongPlace { get; set; }
    }
}
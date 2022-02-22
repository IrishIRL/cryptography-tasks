using System;
using System.Collections.Generic;

namespace IndexOfCoincidence
{
    class Program
    {
        // First we initialize alphabet that we will use.
        // We can change our alphabet directly from here.
        // NB! Alphabet should stay in Upper-Case. I have added ".ToUpper()" just in case.
        public static readonly char[] AlphabetLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToUpper().ToCharArray();
        
        // Here we have the initial message that is used to count everything.
        // We can change our text directly from here.
        public static readonly string Message =
           "The most beautiful things in the world cannot be seen or touched, they are felt with the heart.";
        
        static void Main()
        { 
            // Here we initialize a dictionary which will later contain our letters and how many times the letters were used
            var alphabetCounter = new Dictionary<char, int>();

            // Here we add all the letters that we initialized in 'AlphabetLetters' to our dictionary
            // I have design it like that so we can easily change the alphabet at any time (changing 'AlphabetLetters')
            // We will have the actual letter for the Key and the number of occurrences for the Value (since the key is considered unique and the value (count) can be the same for different Keys). 
            foreach (var letter in AlphabetLetters)
            {
                alphabetCounter.Add(letter, 0);
            }

            // There is also a possibility of having letters thar are not in the alphabet.
            // I am not sure what is the right way to do with them, but I assume we have to just dispose them later.
            // This will count things like spaces, comas, dots and other symbols which are not useful in our counter.
            int uncountableChars = 0;
            
            // The IC will be used to count the Index Of Coincidence (or IC for short).
            // Should be double, since we are going to work with <1 probabilities.
            double ic = 0;
            
            // Since I love working with arrays, I will change 'message' from string to array.
            // Additionally, since our alphabet is Upper-Case, we make the 'Message' to upper too.
            char[] messageArray = Message.ToUpper().ToCharArray();
            
            // Could have made that in better way, but though will use nothing but loops :)
            // Here we take each letter from the message and then compare it with all the letters from the alphabet.
            // When we find the correct letter, we add +1 in the dictionary to the corresponding letter.
            // If the character was not identified as the letter after looping through all the letters from the alphabet, we are adding +1 to 'uncountableChars'
            // First loop is for the letters in the message.
            foreach (var messageLetter in messageArray)
            {
                // 'counter' is used to detect how many times we tried to find the correct letter from the alphabet letters.
                // if the 'counter' is equal to the 'AlphabetLetters.Length' that means, that we went through all the letters in the alphabet and found no suitable letter.
                // since we check this for each letter individually, we are also re-declaring the counter each time the first loop goes to the next letter in the message.
                int counter = 0;
                
                // Second loop is for the letters in the alphabet
                foreach (var alphabetLetter in alphabetCounter)
                {
                    // Here we compare the letter we have from the message and the letter from the alphabet.
                    if (messageLetter == alphabetLetter.Key)
                    {
                        // We have found the correct letter, so now we add +1 to the current letter's Value counter.
                        alphabetCounter[messageLetter] += 1;
                    }
                    else
                    {
                        // We increase the 'counter' size, since the letter was not equal to the current 'alphabetLetter.Key'
                        counter++;
                        // If the loop went through all the letters and did not find the correct letter, the counter will be equal to the lenght of the alphabet.
                        if (counter == AlphabetLetters.Length)
                        {
                            uncountableChars++;
                        }
                    }
                }
            }
            
            // Here we go through all the letters in our dictionary.
            // I am using the index of coincidence formula: IC(X) = (na / N) * ((na - 1) / (N - 1)) + (nb / N) * ((nb - 1) / (N - 1)) + ... + (nz / N) * ((nz - 1) / (N - 1)), where:
            // X is an N-letter text
            // and na, nb, ..., nz are denoting the numbers of occurrences of a, b, ..., z in X.
            // The only difference to that formula is the subtraction of 'uncountableChars' from the 'messageArray', since it contain not only the correct letter but spaces/ commas and dots.
            // totalLetterCount is equal to 'N' from the formula.
            // letter.Value is equal to 'na', 'nb', ..., 'nz' from the formula
            double totalLetterCount = (messageArray.Length - uncountableChars);
            foreach (var letter in alphabetCounter)
            {
                ic += (letter.Value / totalLetterCount) * ((letter.Value - 1) / (totalLetterCount-1));
            }
            
            // Frequency table for the message.
            // Not visually ideal, sorry.
            Console.WriteLine("+-----------+-----------+");
            foreach (var letter in alphabetCounter)
            {
                Console.WriteLine("| Letter: " + letter.Key + " | Count: " + letter.Value + " |");
            }
            Console.WriteLine("+----------+----------+");
            
            // Some basic statistics which is not required in the task, but useful to see.
            Console.WriteLine("The amount of non-alphabet letters: " + uncountableChars);
            Console.WriteLine("The total number of letters: " + Message.Length);

            // The index of coincidence. 
            Console.WriteLine("The current IC(X) = " + ic);
            Console.WriteLine("IC(X) ~ 0.038 for a random text ~ 0.065 for a meaningful text.");
            
            Console.ReadLine();
        }
    }
}
/*
 * Consider the following sequence of operations:
 * Plaintext -> S1 -> P1 -> S2 -> P2 -> Ciphertext
 * Plaintext is MOTIVATION.
 * S1 is a shift cipher with key kS1 = 17.
 * S2 is a shift cipher with the key kS2 = 8.
 * P1 is a permutation cipher with a key kP1 = (5, 1, 3, 2, 4).
 * P2 is a permutation cipher with a key kP2 = (3, 4, 5, 1, 2).
 * The task: what is the ciphertext?
*/
using System;

namespace Decipher
{
    class Program
    {
        // This is the declaration of our Alphabet Letters (in the Alphabetic order).
        public static readonly char[] AlphabetLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        
        static void Main()
        {
            // DECLARATIONS START
            // For shiftOne and shiftTwo we are declaring the shift number for our shift ciphers.
            int shiftOne = 17;
            int shiftTwo = 8;
            
            // For permutationOne and permutationTwo we are declaring the keys of permutation
            int[] permutationOne = {5, 1, 3, 2, 4};
            int[] permutationTwo = {3, 4, 5, 1, 2};
            
            // Our plaintext is the text that we want to encrypt
            string plaintext = "MOTIVATION";
            
            // I am also working with arrays everywhere after, so making from the plaintext array. 
            char[] plaintextArray = plaintext.ToCharArray();
            // DECLARATIONS END
            
            // LOGIC START
            char[] cipherOne = ShiftCipher(shiftOne, plaintextArray);
            char[] cipherTwo = PermutationCipher(permutationOne, cipherOne);
            char[] cipherThree = ShiftCipher(shiftTwo, cipherTwo);
            char[] cipherFour = PermutationCipher(permutationTwo, cipherThree);
            // LOGIC END
            
            // OUTPUT THE RESULTS START
            Console.WriteLine(plaintext + " - Before the encryption");
            Console.WriteLine(new string(cipherOne) + " - After the first phase (shift with a key kS1 = 17)");
            Console.WriteLine(new string(cipherTwo) + " - After the second phase (permutation with a key kP1 = (5, 1, 3, 2, 4))");
            Console.WriteLine(new string(cipherThree) + " - After the third phase (shift with a key kS1 = 8))");
            Console.WriteLine(new string(cipherFour) + " - After the last phase (permutation with a key kP1 = (3, 4, 5, 1, 2))");
            Console.ReadLine();
            // OUTPUT THE RESULTS END
        }

        // This is the Shift Cipher encryption function
        private static char[] ShiftCipher(int shiftCount, char[] plaintextArray)
        {
            // First we are declaring a new char array.
            // It will contain our encrypted plaintext result.
            // The lenght of the 'encrypted' char is the same as the plaintextArray, because it should contain the same amount of letters but shifted.
            char[] encrypted = new char[plaintextArray.Length];
            
            // We are looping through the whole text.
            // (So working with each letter individually).
            for (int i = 0; i < plaintextArray.Length; i++)
            {
                // We are searching within the 'AlphabetLetters' for the letter that is equal to our plaintext letter. 
                // When we find the correct letter, we save the index of it to the 'letterToIndex'
                var letterToIndex = Array.FindIndex(AlphabetLetters, letter => letter == plaintextArray[i]);
                // Now we add to the "old" unencrypted index the shift that we want to use.
                // So we shift the letter to the amount that was declared in shiftCount integer.
                letterToIndex += shiftCount;
                // Here we use the new found index of the letter, modulo it through the amount of letters
                // (so if the amount of letters in the alphabet was 26 and the letter is now 30, it will go back to the beginning, not out of range)
                // and save the new letter to the 'encrypted' char[i], where i is the correct placing index of letter in array. 
                encrypted[i] = AlphabetLetters[letterToIndex % AlphabetLetters.Length];
            }
            
            // Here we return the newly encrypted text
            return encrypted;
        }
        
        // This is the Permutation Cipher encryption function
        private static char[] PermutationCipher(int[] permutationInts, char[] plaintextArray)
        {
            // The counter is used to find when we are proceeding to the next block in the cipher.
            // As for an example, if the Key lenght is equal to 5 each time it reaches the 6th element (the 6th 'i' is equal to 5), the counter will get +1,
            // because we are searching for the elements that will give zero as the result.
            // Since the first element is also a 0, I am starting the counter as -1, so it will get to 0 as soon as the loop starts. 
            int counter = -1;
            
            // Here we are declaring a new char array.
            // It will contain our encrypted plaintext result.
            // The lenght of the 'encrypted' char is the same as the plaintextArray, because it should contain the same amount of letters but shifted.
            char[] encrypted = new char[plaintextArray.Length];

            // We are looping through the whole text.
            // (So working with each letter individually).
            for (int i = 0; i < plaintextArray.Length; i++)
            {
                // Here we check that the letter from the text is equal to the start of new block. If it is, then the counter gets +1.
                if (i % permutationInts.Length == 0) counter++;
                // location is the integer of where the current letter should go in the block.
                // for example, if the permutation int has the rule of first letter going on the 5th place, 
                // then the right side will return 5 (as for place) -1 (because indexes in the array are counted from the zero, not from one).
                int location = (permutationInts[i % permutationInts.Length] - 1);

                // Here we take the new location of the letter (location) and add the index of the letter in the text.
                // It is done by multiplying the current block and the amount of letters in the block.
                encrypted[i] = plaintextArray[location + counter * permutationInts.Length];
            }

            // Here we return the newly encrypted text
            return encrypted;
        }
    }
}

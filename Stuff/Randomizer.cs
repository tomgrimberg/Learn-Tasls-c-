using System;
using System.Collections.Generic;
using System.Text;

namespace Stuff
{
    public class Randomizer
    {
        private readonly int[] randomizedArray;
        public int[] RandomizedArray { 
            get { return randomizedArray; }
        }
    

    public Randomizer(int size)
        {
            randomizedArray = new int[size];

            for (int index = 0; index < randomizedArray.Length; index++)
            {
                randomizedArray[index] = index+1;
            }

            Console.WriteLine("Original Array:");
            for (int i = 0; i < randomizedArray.Length; i++)
            {
                Console.WriteLine($"Array[{i}] = {randomizedArray[i]}");
            }

            RandomizeArray();

            Console.WriteLine("Randomized Array:");
            for (int i = 0; i < randomizedArray.Length; i++)
            {
                Console.WriteLine($"Array[{i}] = {randomizedArray[i]}");
            }

        }

        public void RandomizeArray()
        {
            int i = randomizedArray.Length;
            int temp;
            Random random = new Random();

            while (i > 0) // for(i=randomizedArray.Length ; i>0 ; --i)
            {
                int randomNumber = random.Next(i);
                temp = randomizedArray[randomNumber];
                randomizedArray[randomNumber] = randomizedArray[i-1];
                randomizedArray[i-1] = temp;
                --i;
            }
        }



    }
}

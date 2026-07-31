
using System.Collections;
using System.Runtime.CompilerServices;

class Primeinator
{ 
    static void Main()
    {
        //Grabs our program directory and initializes the string to the location of our PrimesList then reads that file into rawPrimes
        string primesPath = Directory.GetCurrentDirectory() + "\\PrimeList.txt";
        string rawPrimes = File.ReadAllText(primesPath);

        Console.WriteLine("test");

        List<int> parsedPrimes = NumberParse(rawPrimes);

        Console.WriteLine("How many numbers would you like to evaluate?");

        string primeSplitsInput = "";

        do
        {
            primeSplitsInput = PromptUser();
        }
        while (primeSplitsInput == "");

        int primeDivisons = Convert.ToInt16(primeSplitsInput);

        for (int i = 0; i < parsedPrimes.Count; i++)
        {
            Console.WriteLine(parsedPrimes[i]);
        }
        

    }

    //logic to convert our CSV primes to a simple, ordered list of primes
    static private List<int> NumberParse(string rawPrimes)
    {
        Queue<char> intermediatePrime = new Queue<char>();
        string completePrime = "";
        List<int> parsedPrimes = new List<int>();

        for (int i = 0; i + 1 <= rawPrimes.Length; i++)
        {
            if (rawPrimes[i] == ',')
            {
                //convert queue to string, add that as converted int32 to list then clear queue

                completePrime = string.Concat(intermediatePrime);
                parsedPrimes.Add(Convert.ToInt32(completePrime));

                for (int j = 0; intermediatePrime.Count != 0; j++)
                {
                    intermediatePrime.Dequeue();
                }
                completePrime = "";
            }
            else if (rawPrimes[i] == ' ') 
            {

            }
            else
            {
                intermediatePrime.Enqueue(rawPrimes[i]);
            }
        }
        return parsedPrimes;
    }

    static private string PromptUser()
    {
        string? userInput = Console.ReadLine();

        //TODO: LIMIT THE LENGTH OF WHAT THE USER CAN INPUT HERE TO LESS THAN THE TOTAL NUMBER OF PRIMES

        if (userInput is null)
        {
            Console.WriteLine("You must enter a value");
            userInput = "";
            return userInput;
        }
        else if (NumberCheck(userInput))
        {
            Console.WriteLine("Input must be a whole number");
            userInput = "";
            return userInput;
        }
        else
        {
            return userInput;
        }
           
    }
    static private bool NumberCheck(string userInput)
    {
        foreach(char c in userInput)
        {
            if (char.IsNumber(c))
            {

            }
            else
            {
                return true;
            }
           
        }
        return false;
    }
}


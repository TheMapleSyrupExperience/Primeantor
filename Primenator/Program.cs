
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;

class Primeinator
{
    static void Main()
    {
        var runningTime = Stopwatch.StartNew();

        //Grabs our program directory and initializes the string to the location of our PrimesList then reads that file into rawPrimes
        string primesPath = Directory.GetCurrentDirectory() + "\\PrimeList.txt";
        string rawPrimes = File.ReadAllText(primesPath);

        List<int> parsedPrimes = NumberParse(rawPrimes);

        Console.WriteLine("How many numbers would you like to evaluate?");

        string primeSplitsInput = "";

        do
        {
            primeSplitsInput = PromptUser();

        }
        while (primeSplitsInput == "");

        int primeDivisions = Convert.ToInt32(primeSplitsInput);

        //initialise our array to the size and depth relative to what the user wants
        // i.e. 4 inputs, devide the length of  our list by 4 and create array of that depth with 4 
        // slots. I feel issues might arise with uneven devisors (i.e. if we return
        // a value that would be a decimal; to investigate.

        int[,] sortedPrimes = new int[parsedPrimes.Count/primeDivisions,primeDivisions];
        int[] userInputValues = new int[primeDivisions];

        int iterationCount = 0;
        int currentIndex = 0;

        while (iterationCount < sortedPrimes.GetUpperBound(0))
        {
          
            for (int i = 0; i < primeDivisions; i++)
            {
                //i suspect this is a funky way to achieve this.
                
                sortedPrimes[currentIndex, i] = parsedPrimes[iterationCount];
                iterationCount++;
            }
            currentIndex++;
        }
        
        while (userInputValues.Length < primeDivisions)
        {
            for (int i = 0; i < primeDivisions; i++)
            {

            }
        }

       

    }

    //logic to convert our CSV primes to a simple list maintaining their order. Could be used on any CSV input of numbers
    // but will not play well with headers or text input.
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
        else if (userInput == "0")
        {
            Console.WriteLine("Input cannot be Zero");
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
        //simple check for any non-numeric characters in userInput
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


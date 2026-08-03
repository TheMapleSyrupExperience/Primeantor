
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

        string userInput = "";

        Console.WriteLine("How many numbers would you like to evaluate? Must be 10 or fewer");

        do
        {
            userInput = PromptUser();
            try
            {

                if (userInput == "")
                {

                }
                else if (Convert.ToInt32(userInput) > 10)
                {
                    Console.WriteLine("Input must be less than 10");
                    userInput = "";
                }
            }
            catch (OverflowException)
            {
                Console.WriteLine("Input is too large, are you trying to break things, punk??");
                userInput = "";
            }


        }
        while (userInput == "");

        int primeDivisions = Convert.ToInt32(userInput);

        // initialise our array to the size and depth relative to what the user wants
        // i.e. 4 inputs, devide the length of  our list by 4 and create array of that depth with 4 
        // slots. I feel issues might arise with uneven devisors (i.e. if we return
        // a value that would be a decimal; to investigate.

        int[,] sortedPrimes = new int[parsedPrimes.Count/primeDivisions,primeDivisions];
        int[] userTargetValues = new int[primeDivisions];

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



        // Now we accept user inputs for values targeted by subtraction

        Console.Clear();
        Console.WriteLine("Please enter each desired value followed by the ENTER key");


        for (int i = 0; i < primeDivisions; i++)
        {
            Console.WriteLine($"Current Value: {i + 1} of {primeDivisions}");

            userInput = "";
            while (userInput == "")
            {
                userInput = PromptUser();

                try
                {
                    userTargetValues[i] = Convert.ToInt32(userInput);
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Input is too large, are you trying to break things, punk??");
                    Console.WriteLine("I bet you think you're real funny.");
                    userInput = "";
                }

            }
        }

        //then we'll perform our calcuations and return our winning index of userTargetValues[]
        int response = WinnerCalculation(userTargetValues, sortedPrimes, primeDivisions);

        if (response != -1)
        {
            Console.WriteLine($" {userTargetValues[response]} is the first number to run-out");
        }
        else
        {
            Console.WriteLine("Out of Primes, no winner :( ");
        }

        Console.WriteLine(" ");
        Console.WriteLine("Press Any Key to End Program");
        Console.ReadLine();

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

    // method for accepting how many divisions the user wants of the list of primes. Limits based on value input
    // and must be less than ten
    static private string PromptUser()
    { 
        string? userInput = Console.ReadLine();
        
        //probably should change this out to a switch statement

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

    static private int WinnerCalculation(int[] userInputs, int[,] splitPrimes, int inputCount)
    {
        int userInputIndex = 0;
        int primesIndex = 0;

        //copy our argumetn array because we don't want to mess with the parent values. We just want an index back.

        int[] localInputs = new int[inputCount];
        userInputs.CopyTo(localInputs, 0);


        while(primesIndex < splitPrimes.GetLength(0))
        {
            localInputs[userInputIndex] -= splitPrimes[primesIndex, userInputIndex];
            if (localInputs[userInputIndex] <= 0)
            {
                return userInputIndex;
            }
            else if (userInputIndex < inputCount - 1)
            {
                userInputIndex++;
            }
            else
            {
                userInputIndex = 0;
                primesIndex++;
            }
        }

        return -1;
       
    }
}


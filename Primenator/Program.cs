
using System.Runtime.CompilerServices;

class Primeinator
{
    static void Main()
    {
        //Grabs our program directory and initializes the string to the location of our PrimesList and 
        string primesPath = Directory.GetCurrentDirectory() + "\\PrimeList.txt";
        string rawPrimes = File.ReadAllText(primesPath);
        Console.WriteLine("How many numbers would you like to evaluate?");

        string primeSplits = Promptuser();

        while (primeSplits == "")
        {
            primeSplits = Promptuser();
        }

        Console.WriteLine("thank you!");
        
    }

    static private string Promptuser()
    {
        string? userInput = Console.ReadLine();

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



// See https://aka.ms/new-console-template for more information

class Integrals
{
    public static void Main(String[] args)
    {
        IntegralCalculator calculator = new IntegralCalculator();

        calculator.Run();
    }
}



class IntegralCalculator
{

    public IntegralCalculator()
    {
    }

    public void Run()
    {
        while (true)
        {


            Console.WriteLine($"Please, specify the number of elements in the polynomial:");

            string lengthInput = Console.ReadLine();

            if (int.TryParse(lengthInput, out int length))
            {
                if (length < 2 || length > 4)
                {
                    Console.WriteLine("Incorrect length.");
                }
                else
                {
                    Console.WriteLine($"Specified length: {length}.");

                    string funcBody = "";
                    List<FunctionArgument> arguments = new List<FunctionArgument>();


                    int previousPower = 0;

                    bool canProceed = false;

                    for (int i = 1; i <= length; i++)
                    {
                        Console.WriteLine($"Specify the power for argument #{i}:");
                        string input = Console.ReadLine();

                        if (int.TryParse(input, out int power))
                        {
                            // check order:
                            if (i != 1 && power >= previousPower)
                            {
                                Console.WriteLine("The power must be greater than the previous power. \n");
                                break;
                            }

                            Console.WriteLine("Specify the coefficient:");
                            string coefficientInput = Console.ReadLine();

                            if (int.TryParse(coefficientInput, out int coefficient))
                            {
                                if (coefficient != 0)
                                {
                                    string temp = "";

                                    if (power == 1)
                                    {
                                        if (coefficient == 1)
                                        {
                                            temp += "x";
                                        }
                                        else
                                        {
                                            temp += $"{coefficient}x";
                                        }

                                    }
                                    else if (coefficient == 1)
                                    {
                                        temp += $"x^{power}";
                                    }
                                    else
                                    {
                                        temp += $"{coefficient}x^{input}";
                                    }

                                    if (funcBody.Length == 0)
                                    {
                                        funcBody += temp;
                                    }
                                    else
                                    {
                                        funcBody += $" + {temp}";
                                    }


                                }

                                arguments.Add(new FunctionArgument("x", coefficient, power));

                                previousPower = power;

                                if (i == length)
                                {
                                    canProceed = true;
                                }
                            }


                        }

                        Console.WriteLine(funcBody);
                    }

                    if (canProceed)
                    {
                        Console.WriteLine("Please select definite or indefinite integration:");

                        Console.WriteLine("1 - indefinite, 2 - definite, 0 - return");

                        bool picking = true;

                        while (picking == true)
                        {
                            string choiceInput = Console.ReadLine();

                            if (int.TryParse(choiceInput, out int choice))
                            {
                            
                                if (choice == 0)
                                {
                                    picking = false;
                                } else if (choice == 1)
                                {
                                    IndefiniteEvaluator indefiniteEvaluator = new IndefiniteEvaluator();

                                    string indefinite = indefiniteEvaluator.EvaluateIndefiniteIntegral(arguments);

                                    Console.WriteLine($"Indefinite integral: {indefinite}");

                                    picking = false;
                                } else if (choice == 2)
                                {

                                    Console.WriteLine("Specify lower bound:");
                                    string lowerBoundInput = Console.ReadLine();

                                    Console.WriteLine("Specify upper bound:");
                                    string upperBoundInput = Console.ReadLine();

                                    if (double.TryParse(lowerBoundInput, out double lower) && double.TryParse(upperBoundInput, out double upper))
                                    {
                                        DefiniteEvaluator definiteEvaluator = new DefiniteEvaluator();

                                        string definite = definiteEvaluator.EvaluteDefiniteIntegral(arguments, lower, upper);

                                        Console.WriteLine($"Definite integral: {definite}");

                                        picking = false;
                                    }

                                }
                            }

                        }
                    }
                }


            }


        }

    }
}


class FunctionArgument
{
    private string symbol;
    private int coefficient;
    private int power;


    public FunctionArgument(string symbol, int coefficient, int power) {
        this.symbol = symbol;
        this.coefficient = coefficient;
        this.power = power;
    }

    public string Symbol { get { return symbol; } }
    public int Coefficient { get { return coefficient; } }
    public int Power { get { return power; } }
}


class IndefiniteEvaluator
{

    public string EvaluateIndefiniteIntegral(List<FunctionArgument> arguments) 
    {
        string result = "";

        foreach (FunctionArgument arg in arguments) {
            string integral = Calculate(arg);

            result += integral + " + ";
        }

        return $"{result} C";
    }

    private string Calculate(FunctionArgument argument)
    {
        return $"({argument.Coefficient} * x^{argument.Power + 1}) / {argument.Power + 1}";
    }

}

class DefiniteEvaluator
{
    public string EvaluteDefiniteIntegral(List<FunctionArgument> arguments, double lowerBound, double upperBound)
    {
        double result = 0.0;


        foreach (FunctionArgument arg in arguments)
        {
            double integral = Calculate(arg, lowerBound, upperBound);

            result += integral;
        }

        return $"{result}";
    }

    private double Calculate(FunctionArgument argument, double lowerBound, double upperBound)
    {
        double result = (argument.Coefficient / (argument.Power + 1.0)) *
                (Math.Pow(upperBound, argument.Power + 1) - Math.Pow(lowerBound, argument.Power + 1));

        return result;
    }
}

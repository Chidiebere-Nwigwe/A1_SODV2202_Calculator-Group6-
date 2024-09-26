using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1_SODV2202_Calculator
{

    // TODO Add supporting classes here
    public class Token
    {
        //This is used to store the math expressions...
        public string expression;

        public Token(string expression)
        {
            
            this.expression = expression;
        }

        /*We use this block of code to basically loop through the expression, then when we see a number, we will collect it and put
         it in a box called current number, and when we see an operator, we will then empty the current number box into the tokens box alongside
        the operator, then we will start again with the next number until we reach the end
        */
        //public List<string> Tokenize()
        public List<string> Tokenize()
        {
            //we use tokens to store the number that is collected in current number string
            List<string> tokens = new List<string>();
            //we use this code to declare an empty sting that we will use to collect the numbers
            string current_Num = "";

            //logic for looping through the characters in the math expression
            for (int i = 0; i < expression.Length; i++)
            {
                //accessing the characters in expression using i as the index number.
                Char ch = expression[i];

                //conditional logic for checking if the character is a number or a decimal point
                if (char.IsDigit(ch) || ch == '.')
                {
                    //If the character is a digit or a decimal point, add it to the empty current number string
                    current_Num += ch;
                }
                //conditional logic for checking if the character is one of the operators below.
                else if ("+-/*()".Contains(ch))
                {
                    //conditional logic to check if the current number string is not empty
                    if (!string.IsNullOrEmpty(current_Num))
                    {
                        //if current number string is not empty, then add the number into tokens to store it and start again
                        tokens.Add(current_Num);
                        current_Num = "";
                    }
                    //conditional logic to handle unary minus or negative number
                    if (ch == '-' && (i == 0 || "()+-/*".Contains(expression[i - 1])))
                    {
                        current_Num = "-";
                    }
                    else
                    {
                        //if the character is an operator, add it to tokens
                        tokens.Add(ch.ToString());
                    }
                }
            }
            //if there is any remaining number, add it to tokens
            if (!string.IsNullOrEmpty(current_Num))
            {
                tokens.Add(current_Num);
            }
            return tokens;
        }

    }


    public class Parse
    {

    }
    public class Program

    {

        public static string ProcessCommand(string input)

        {

            try
            {
                //functionality to remove all spaces in the input string
                input = input.Replace(" ", "");
                input = System.Text.RegularExpressions.Regex.Replace(input, @"\s+", "");
                return input;
            }

            catch (Exception e)

            {

                return "Error evaluating expression: " + e;

            }

        }

        static void Main(string[] args)

        {

            string input;

            while ((input = Console.ReadLine()) != "exit")

            {

                Console.WriteLine(ProcessCommand(input));

            }

        }

    }

}


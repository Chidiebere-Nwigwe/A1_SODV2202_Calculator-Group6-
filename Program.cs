using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1_SODV2202_Calculator
{
    // TODO Add supporting classes here

    //A token class is created to split the user input and seperate it into numbers and operators.
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

    //In this class, we will parse and evaluate the expression using the shunting yard algorithm (the algorithm that uses RPN)
    // Link to source for Shunting Yard Algorithm research: https://rosettacode.org/wiki/Parsing/Shunting-yard_algorithm#C#
    public class Parse
    {
        //to apply precedence, we prioritize the operators according to bedmas rules.
        // 1 means less priority, and 2 means high piority
        //We did not bother with associative, because all the operaters here are all false
        public Dictionary<string, int> Precedence = new Dictionary<string, int>()
            {
                {"+", 1 },
                {"-", 1 },
                {"*", 2 },
                {"/", 2 }
            };

        // Method to convert from infix to postfix
        public List<string> Convert(List<string> tokens)
        {
            //creating a stack for the final postfix notation
            List<string> num_stack = new List<string>();
            //creating a stack for the operators. More info here: https://youtu.be/QM_RsQ9Yeio?si=dY-VyWYluLbSMiQo
            Stack<string> operators = new Stack<string>();

            //we go through the list of tokens and check if it is a number
            foreach (string token in tokens)
            {
                if (double.TryParse(token, out double num))
                {
                    //If it is a number, we will add it straight to the final stack as shown in the video from the link above
                    num_stack.Add(token);
                }

                //I moved this logic to the token class,because it was clashing with the operator precedence logic
                /* //Conditional logic to check for negative numbers. MUST COME BEFORE OPERATORS CONDITIONAL LOGIC!!!!!(Learnt this from experience)
                 else if (token == "-" && (operators.Count == 0|| operators.Peek() == "(" || Precedence.ContainsKey(operators.Peek())))
                 {
                         //if the token is a minus sign, and there are no operators or there is an opening bracket or there is an operator before it, then add 0 to the postfix stack
                         num_stack.Add("0");
                         //then push the token to the operators stack
                         operators.Push(token);
                 }*/

                //conditional logic to check if operator is available in tokens list
                else if (Precedence.ContainsKey(token))
                {
                    //loop for the availability and precedence of operator
                    while (operators.Count > 0 && Precedence.ContainsKey(operators.Peek()) &&
                    Precedence[token] <= Precedence[operators.Peek()])
                    {
                        //if the operator is of high priority, add it into the final postfix stack
                        num_stack.Add(operators.Pop());
                    }
                    //if operator is available and is of low priority, push it into the operators stack
                    operators.Push(token);
                }
                //conditional logic to check for opening parenthesis
                else if (token == "(")
                {
                    operators.Push(token);
                }
                //conditional logic to check for closing parenthesis
                else if (token == ")")
                {
                    //loop to find the matching parenthesis and pop it to the num_stack stack.
                    while (operators.Peek() != "(")
                    {
                        num_stack.Add(operators.Pop());
                    }
                    operators.Pop();
                }
            }
            //loop to pop operators into the num_stack stack
            while (operators.Count > 0)
            {
                num_stack.Add(operators.Pop());
            }
            return num_stack;
        }
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


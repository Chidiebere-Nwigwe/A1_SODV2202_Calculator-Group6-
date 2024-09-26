using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A1_SODV2202_Calculator
{

        // TODO Add supporting classes here
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


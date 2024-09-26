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






                return "";

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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.UI;

namespace Assignment
{
    static class InputOutput
    {
        public static int GetOption(int numOfOptions) //Function that returns which option the user enters. Requires the number of options to check if they entered a valid option
        {
            bool validInput = false;
            int input = 0;
            while (!validInput)  //Keep asking for an input until they enter one that's valid
            {
                Console.WriteLine("");

                if (Int32.TryParse(Console.ReadLine(), out input)) //Make sure they input an integer
                {
                    if (input <= numOfOptions && input > 0) //Check the integer was actually one of the options
                    {
                        validInput = true; //They entered a valid input
                    }
                    else
                    {
                        Console.WriteLine("{0} is not a valid option.\nEnter an option between 1 and {1}", input, numOfOptions); //They entered a number below 1 or a number thats too high. Tell them what numbers they can choose
                    }
                }
                else
                {
                    Console.WriteLine("Your input was invalid\nPlease enter a number representing the option you would like to choose"); //They didn't enter a number. Get them to try again.
                }
            }
            return input;
        }

        public static void DisplayOptions(string title, string[] options) //Display some options for the user as well as a title/question
        {
            Console.WriteLine(title + "\n\n"); //Show the title/ask the question
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + options[i]); //Write all the options on a new line
            }
        }

        public static string[] ReadFile(string path) //read a file and return its data
        {
            string[] entries = File.ReadAllLines(path); //get the data from the file
            for (int i = 0; i < entries.Length; i++)
            {
                entries[i] = entries[i].Trim(' ', '\r', '\n'); //trim off any spaces and new lines
            }

            return entries; //return the data
        }

        public static string[] ReadFile(string[] path) //read 2 files and return both their data combined
        {
            string[] entries1 = File.ReadAllLines(path[0]); //get the first set of data
            string[] entries2 = File.ReadAllLines(path[1]); //get the second set of data
            string[] entries = new string[entries1.Length + entries2.Length]; //create a new array big enough to fit both sets of data
            for (int i = 0; i < entries1.Length; i++) //add the first set of data
            {
                entries[i] = entries1[i];
            }
            for (int i = entries1.Length; i < entries.Length; i++) //add the second set of data
            {
                entries[i] = entries2[i - entries1.Length];
            }

            for (int i = 0; i < entries.Length; i++) //trim off any spaces and new lines from the data
            {
                entries[i] = entries[i].Trim(' ', '\r', '\n');
            }

            return entries; //return the data
        }

        public static void OutputToHTML(string title, string method, int steps, string[][] tableData, string[] columnTitles, bool search = false) //Create a HTML file to output data
        {
            string operation; //set the operation string based on what we've done
            if (search)
                operation = "Search";
            else
                operation = "Sort";

            StringWriter stringWriter = new StringWriter(); //Create a StringWriter for use in HTMLTextWriter
            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter)) //Create a new HTMLTextWriter in a using so that it gets disposed of once we're done
            {
                writer.RenderBeginTag(HtmlTextWriterTag.H1);    //<h1>
                writer.Write(title);    //Title
                writer.RenderEndTag();  //</h1>

                writer.RenderBeginTag(HtmlTextWriterTag.H2);    //<h2>
                writer.Write(operation + " method: " + method);     //Sort/search Method
                writer.RenderEndTag();      //</h2>

                writer.RenderBeginTag(HtmlTextWriterTag.H2);    //<h2>
                writer.Write("Steps: " + steps.ToString());     //Comparisons
                writer.RenderEndTag();      //</h2>

                writer.RenderBeginTag(HtmlTextWriterTag.Table);     //<table>

                writer.RenderBeginTag(HtmlTextWriterTag.Tr);        //<tr>
                for (int i = 0; i < columnTitles.Length; i++)
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Th);        //Put each column title inside <th> </th>
                    writer.Write(columnTitles[i]);
                    writer.RenderEndTag();
                }
                writer.RenderEndTag();      //</tr>

                for (int i = 0; i < tableData[0].Length; i++)   //For each row of data
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);        //Create a <tr>

                    for (int x = 0; x < tableData.Length; x++)  //For each column of data
                    {
                        writer.RenderBeginTag(HtmlTextWriterTag.Td);    //Create a <td>
                        writer.Write(tableData[x][i]);                  //Add the data
                        writer.RenderEndTag();                          //Create a </td>
                    }

                    writer.RenderEndTag();  //Create a </tr>
                }

                writer.RenderEndTag(); //</table>
            }

            string HTML = stringWriter.ToString(); //Get the HTML from the stringWriter and store it in a string
            File.WriteAllText("output.html", HTML); //Write the HTML to the file
            System.Diagnostics.Process.Start("output.html"); //Open the file
            
        }

        public static string GetKeyboardEntry() //Get an input from the keyboard
        {
            string input = Console.ReadLine(); //Get the input
            input.Trim(' ', '\r', '\n'); //trim any spaces and new lines off the start and end
            return input; //return the value
        }

        public static void DisplayPrompt(string prompt) //Display a promt for the user
        {
            Console.WriteLine(prompt + "\n\n"); //Write the prompt with some new lines
        }
    }
}

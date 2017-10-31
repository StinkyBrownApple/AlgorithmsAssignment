using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    static class MainClass
    {
        //Instantiate some variabes to use in the class
        static string filesFolder = @"..\..\Files\";
        static string extenstion = @".txt";
        static string[] dataNames = new string[] { "Day", "Depth", "IRIS ID", "Latitude", "Longitude", "Magnitude", "Month", "Region", "Time", "Timestamp", "Year"};
        static bool bothData = false;
        static int dataSet = 1;

        static void Main(string[] args) //Program entry point
        {
            DataSelect(); //Get the data to be used
        }

        private static void DataSelect() //Data select menu
        {
            string[] options = { "Data set 1", "Data set 2", "Data set 1 and 2" }; //Options to display

            InputOutput.DisplayOptions("What data would you like to use?", options); //Display the options with a title

            switch(InputOutput.GetOption(3)) //Process input
            {
                case 1: //If 1
                    bothData = false;   //use only data set 1
                    dataSet = 1;
                    break;
                case 2: //If 2
                    bothData = false;   //use only data set 2
                    dataSet = 2;
                    break;
                case 3: //If 3
                    bothData = true;    //use both data sets
                    break;
            }
            MainMenu(); //Go to the main menu

        }

        private static void MainMenu() //Main menu
        {
            string[] options = dataNames; //Get options from the array of data names

            InputOutput.DisplayOptions("Select an array to search/sort", options); //Display options with title
            OperationMenu(options[InputOutput.GetOption(11) - 1]); //Show operation menu and pass data selected
        }

        private static void OperationMenu(string selection) //Operation menu
        {
            //Instantiate some variables we'll need
            string dataType = "string";
            double[] doubleData = new double[0];
            Month[] monthsData = new Month[0];
            string[] options = new string[3];
            string[] data;

            if (bothData) //get the data we need depending on the selection and what data sets we're using
                data = InputOutput.ReadFile(GetFilePaths(selection));
            else
                data = InputOutput.ReadFile(GetFilePath(selection));

            options[0] = "Search";
            options[1] = "Sort";                //Set the options for the user to choose
            options[2] = "Find min and max";
            InputOutput.DisplayOptions("What would you like to do with this data?", options); //Display the options with a title

            switch (selection) //Convert the data to whatever we need based on their selection
            {
                case "Day":
                case "Depth":
                case "IRIS ID":
                case "Latitude":
                case "Longitude":       //if the data is numbers
                case "Magnitude":
                case "Year":
                case "Timestamp":
                    doubleData = Array.ConvertAll(data, double.Parse);  //convert to double
                    dataType = "double";    //set that we're using a double
                    break;
                case "Time":    //if the data is a string
                case "Region":
                    dataType = "string"; //no need t o convert, just set that we're using a string
                    break;
                case "Month":   //if the data is months
                    monthsData = new Month[data.Length];
                    for (int i = 0; i < data.Length; i++)
                    {
                        monthsData[i] = new Month(data[i]);     //convert the data to months
                    }
                    dataType = "month"; //set that we're using months
                    break;
                default:
                    break;
            }

            switch (InputOutput.GetOption(3)) //Get the operation they want to do
            {
                case 1: //If we're searching
                    switch(dataType) //Check the data type were searching and execute using respective array
                    {
                        case "double":
                            Searching(doubleData, selection);
                            break;
                        case "string":
                            Searching(data, selection);
                            break;
                        case "month":
                            Searching(monthsData, selection);
                            break;
                        default:
                            break;
                    }
                    break;
                case 2: //if we're sorting
                    switch (dataType) //Check the data type were sorting and execute using respective array
                    {
                        case "double":
                            Sorting(doubleData, selection);
                            break;
                        case "string":
                            Sorting(data, selection);
                            break;
                        case "month":
                            Sorting(monthsData, selection);
                            break;
                        default:
                            break;
                    }
                    break;
                case 3: //if we're finding min and max
                    switch (dataType) //Check the data type were finding min and max of and execute using respective array
                    {
                        case "double":
                            FindMinMax(doubleData, selection);
                            break;
                        case "string":
                            FindMinMax(data, selection);
                            break;
                        case "month":
                            FindMinMax(monthsData, selection);
                            break;
                        default:
                            break;
                    }

                    break;
                default:
                    break;
            }
        }

        private static void Sorting<T>(T[] selectedData, string selection) where T : IComparable<T>
        {
            //Instantiate some variables we need
            string[][] data = new string[0][];
            string[][] otherSortedData = new string[0][];
            string[] columns = new string[0];
            string[] firstData = new string[0];
            int steps = 0;
            bool descending = false;

            string[] options = new string[5];   //Set the sorting options
            options[0] = "Bubble Sort";
            options[1] = "Insertion Sort";
            options[2] = "Merge Sort";
            options[3] = "Quick Sort";
            options[4] = "Heap Sort";

            string[] sortOption = new string[2]; //Set the ascending/descending options
            sortOption[0] = "Ascending";
            sortOption[1] = "Descending";


            InputOutput.DisplayOptions("Which sorting algorithm would you like to use?", options); //Ask what algorithm they want
            int sortAlg = InputOutput.GetOption(5); //store their choice

            InputOutput.DisplayOptions("Would you like to sort in ascending or descending order?", sortOption); //Ask how they want to sort
            if (InputOutput.GetOption(2) == 1)
                descending = false;
            else
                descending = true; //store their choice


            switch (sortAlg) //Sort the data accordnig to the algorithm they chose
            {
                //for each case, first line sorts chose data and rearranges other data with respect to chosen data, then converts the array to string in order to output
                //second line merges the chosen data and other data into 1 2d array
                //3rd line outputs the data to HTML

                case 1:
                    firstData = Array.ConvertAll(BubbleSort<T>.SortMultiple(selectedData, CreateOtherDataArrays(selection, out columns),out otherSortedData, out steps, descending), x => x.ToString());
                    data = MergeArrays(firstData, otherSortedData);
                    InputOutput.OutputToHTML(selection + " sorting results", "Bubble Sort", steps, data, columns);
                    break;
                case 2:
                    firstData = Array.ConvertAll(InsertionSort<T>.SortMultiple(selectedData, CreateOtherDataArrays(selection, out columns), out otherSortedData, out steps, descending), x => x.ToString());
                    data = MergeArrays(firstData, otherSortedData);
                    InputOutput.OutputToHTML(selection + " sorting results", "Insertion Sort", steps, data, columns);
                    break;
                case 3:
                    firstData = Array.ConvertAll(MergeSort<T>.SortMultiple(selectedData, CreateOtherDataArrays(selection, out columns), out otherSortedData, out steps, descending), x => x.ToString());
                    data = MergeArrays(firstData, otherSortedData);
                    InputOutput.OutputToHTML(selection + " sorting results", "Merge Sort", steps, data, columns);
                    break;
                case 4:
                    firstData = Array.ConvertAll(QuickSort<T>.SortMultiple(selectedData, CreateOtherDataArrays(selection, out columns), out otherSortedData, out steps, descending), x => x.ToString());
                    data = MergeArrays(firstData, otherSortedData);
                    InputOutput.OutputToHTML(selection + " sorting results", "Quick Sort", steps, data, columns);
                    break;
                case 5:
                    firstData = Array.ConvertAll(HeapSort<T>.SortMultiple(selectedData, CreateOtherDataArrays(selection, out columns), out otherSortedData, out steps, descending), x => x.ToString());
                    data = MergeArrays(firstData, otherSortedData);
                    InputOutput.OutputToHTML(selection + " sorting results", "Heap Sort", steps, data, columns);
                    break;
                default:
                    break;
            }
        }

        private static void Searching<T>(T[] selectedData, string selection) where T : IComparable<T>
        {
            //Instantiate some variables we'll need
            int[] dataPositions = new int[0];
            string[][] data = new string[1][];
            string[][] otherData = new string[0][];
            string[][] otherFilteredData = new string[dataNames.Length - 1][];
            string[] columns = new string[0];
            string[] firstData = new string[0];
            int steps = 0;

            string[] options = new string[2]; //Set searching options
            options[0] = "Binary Search";
            options[1] = "Linear Search";

            InputOutput.DisplayOptions("Which search algorithm would you like to use?", options); //Ask which algorithm they want to use
            int sortOption = InputOutput.GetOption(2); //store their choice

            InputOutput.DisplayPrompt("Enter the value you would like to find:");
            string findVal = InputOutput.GetKeyboardEntry(); //get the value we need to find
            switch (sortOption) //find the data using the selected algorithm
            {
                case 1: //For binary search
                    int tempSteps = 0; //temp var for steps
                    selectedData = QuickSort<T>.SortMultiple(selectedData, CreateOtherDataArrays(selection, out columns), out otherData, out tempSteps); //sort the data
                    steps += tempSteps; //add the steps the sort took to the total
                    dataPositions = BinarySearch<T>.Find(selectedData, findVal, true, out tempSteps); //find the positions in the data that the value is at
                    steps += tempSteps; //add the steps the search took
                    if(dataPositions.Length == 0) //if there are no positions, we couldnt find the value
                    {
                        data[0] = new string[]{ "No item found" }; //set the ouput to an error
                    }
                    else //if we did find a value
                    {
                        firstData = PopulateStringArrayFromPositions(selectedData, dataPositions); //get the data using the positions we found for the selected data
                        for (int i = 0; i < otherData.Length; i++)
                        {
                            otherFilteredData[i] = PopulateStringArrayFromPositions(otherData[i], dataPositions); //get the data using the positions we found for all the other data
                        }
                        data = MergeArrays(firstData, otherFilteredData); //merge the data together
                    }
                    InputOutput.OutputToHTML(selection + " search results", "Binary Search", steps, data, columns, true); //output the results
                    break;

                case 2: //For linear search
                    dataPositions = LinearSearch<T>.Find(selectedData, findVal, true, out steps); //find the positions in the data that the value is at
                    if (dataPositions.Length == 0) //if there are no positions, we couldn't find the value
                    {
                        data[0] = new string[] { "No item found" }; //set the output to an error
                    }
                    else //if we did find a value
                    {
                        firstData = PopulateStringArrayFromPositions(selectedData, dataPositions); //get the data using the positions we found for the selected data
                        otherData = CreateOtherDataArrays(selection, out columns); 
                        for (int i = 0; i < otherData.Length; i++)
                        {
                            otherFilteredData[i] = PopulateStringArrayFromPositions(otherData[i], dataPositions); //get the data using the positions we found for all the other data
                        }
                        data = MergeArrays(firstData, otherFilteredData); //merge the data together
                    }
                    InputOutput.OutputToHTML(selection + " search results", "Linear Search", steps, data, columns, true); //output the results
                    break;
                default:
                    break;
            }

        }

        private static void FindMinMax<T>(T[] selectedData, string selection) where T : IComparable<T>
        {
            //Instantiate some variables we'll need
            string[][] data;
            string[] firstData;
            string[][] otherData;
            string[][] otherDataMinMax = new string[dataNames.Length - 1][];
            string[] columns;
            int[] minMaxPositions = { 0, selectedData.Length - 1 };
            int steps;

            firstData = PopulateStringArrayFromPositions(QuickSort<T>.SortMultiple(selectedData, CreateOtherDataArrays(selection, out columns), out otherData, out steps), minMaxPositions); //Sort the data and then get the first and last element in the array
            for (int i = 0; i < otherData.Length; i++)
            {
                otherDataMinMax[i] = PopulateStringArrayFromPositions(otherData[i], minMaxPositions); //get the first and last element in the other data
            }
            data = MergeArrays(firstData, otherDataMinMax); //merge the data together
            InputOutput.OutputToHTML("Minimum and Maximum of " + selection, "Min and max using QuickSort", steps, data, columns); //output the results
        }


        private static string GetFilePath(string option) //Return the file path of the data
        {
            switch(option) //Return the file path based on the input
            {
                case "Day":
                    return filesFolder + "Day_" + dataSet + extenstion;
                case "Depth":
                    return filesFolder + "Depth_" + dataSet + extenstion;
                case "IRIS ID":
                    return filesFolder + "IRIS_ID_" + dataSet + extenstion;
                case "Latitude":
                    return filesFolder + "Latitude_" + dataSet + extenstion;
                case "Longitude":
                    return filesFolder + "Longitude_" + dataSet + extenstion;
                case "Magnitude":
                    return filesFolder + "Magnitude_" + dataSet + extenstion;
                case "Month":
                    return filesFolder + "Month_" + dataSet + extenstion;
                case "Region":
                    return filesFolder + "Region_" + dataSet + extenstion;
                case "Time":
                    return filesFolder + "Time_" + dataSet + extenstion;
                case "Timestamp":
                    return filesFolder + "Timestamp_" + dataSet + extenstion;
                case "Year":
                    return filesFolder + "Year_" + dataSet + extenstion;
                default:
                    return "";
            }
        }

        private static string[] GetFilePaths(string option) //Return the file paths of both sets of data
        {
            switch (option) //Return the files paths of both sets of data in an array based on input
            {
                case "Day":
                    return new string[] { filesFolder + "Day_1" + extenstion, filesFolder + "Day_2" + extenstion };
                case "Depth":
                    return new string[] { filesFolder + "Depth_1" + extenstion, filesFolder + "Depth_2" + extenstion };
                case "IRIS ID":
                    return new string[] { filesFolder + "IRIS_ID_1" + extenstion, filesFolder + "IRIS_ID_2" + extenstion };
                case "Latitude":
                    return new string[] { filesFolder + "Latitude_1" + extenstion, filesFolder + "Latitude_2" + extenstion };
                case "Longitude":
                    return new string[] { filesFolder + "Longitude_1" + extenstion, filesFolder + "Longitude_2" + extenstion };
                case "Magnitude":
                    return new string[] { filesFolder + "Magnitude_1" + extenstion, filesFolder + "Magnitude_2" + extenstion };
                case "Month":
                    return new string[] { filesFolder + "Month_1" + extenstion, filesFolder + "Month_2" + extenstion };
                case "Region":
                    return new string[] { filesFolder + "Region_1" + extenstion, filesFolder + "Region_2" + extenstion };
                case "Time":
                    return new string[] { filesFolder + "Time_1" + extenstion, filesFolder + "Time_2" + extenstion };
                case "Timestamp":
                    return new string[] { filesFolder + "Timestamp_1" + extenstion, filesFolder + "Timestamp_2" + extenstion };
                case "Year":
                    return new string[] { filesFolder + "Year_1" + extenstion, filesFolder + "Year_2" + extenstion };
                default:
                    return new string[0];
            }
        }

        private static string[] PopulateStringArrayFromPositions<T>(T[] inputData, int[] positions) //Get data from provided positions in array
        {
            string[] data = new string[positions.Length]; //create an array to store the data
            for (int i = 0; i < data.Length; i++) //loop through the positions
            {
                data[i] = inputData[positions[i]].ToString(); //get the data at that position as a string
            }

            return data; //return the data
        }

        private static string[][] CreateOtherDataArrays(string excludedData, out string[] _dataTitles) //Create the other sets of data, exluding the one the user hase chosen
        {
            //instatiate some variables we'll need
            string[][] data = new string[dataNames.Length - 1][];
            string[] dataTitles = new string[dataNames.Length];
            int i = 0;
            int j = i;
            dataTitles[0] = excludedData;
            while (i < data.Length) //go through the data
            {
                if (dataNames[i] == excludedData) //if this is the data we dont want to include, skip it and continue
                    j++;
                if(!bothData)   //Get the data from the file based on the data set we're using
                    data[i] = InputOutput.ReadFile(GetFilePath(dataNames[j]));
                else
                    data[i] = InputOutput.ReadFile(GetFilePaths(dataNames[j]));
                dataTitles[i + 1] = dataNames[j]; //set the titles for the data for use in the output
                i++; //increment i and j for next loop
                j++;
            }

            _dataTitles = dataTitles; //set the titles to the ones we generated for when we return
            return data; //return the data ( and titles in out )
        }
        
        private static string[][] MergeArrays(string[] first, string[][] other) //Merge and array with a 2d array
        {
            string[][] data = new string[other.Length + 1][]; //create an aray 1 bigger than the 2d array
            data[0] = first; //set the first set of data to the 1d array passed
            for (int i = 1; i < data.Length; i++)
            {
                data[i] = other[i - 1]; //set the rest of the data to the 2d array passed
            }
            return data; //return the data
        }
    }
}

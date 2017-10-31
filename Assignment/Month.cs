using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    struct Month : IComparable<Month> //A data structure used to manage Months
    {
        //Instantiate some variables we'll need
        string month;
        string[] validMonths;

        public Month(string _month) //Constructor
        {
            validMonths = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" }; //Set the valid months that can be assigned
            _month = _month.Trim(' ', '\r', '\n'); //trim the input
            char[] tempCharArray = _month.ToCharArray();    //convert the string to chars
            tempCharArray[0] = char.ToUpper(tempCharArray[0]);  //capitalise the first character
            month = new string(tempCharArray); //convert back to a string and store it
        }



        public override string ToString()
        {
            return month; //Return the month value when ToString() is called on the object
        }

        int IComparable<Month>.CompareTo(Month other) //logic for comparing 2 months
        {
            return GetCorrespondingInt(month).CompareTo(other.GetCorrespondingInt(other.month)); //Get the corresponding number for this month and the compared month, and return the CompareTo of those numbers
        }

        private int GetCorrespondingInt(string monthString) //Get the number which represents which month of the year this month is
        {
            switch(monthString)
            {
                case "January":
                    return 1;
                case "February":
                    return 2;
                case "March":
                    return 3;
                case "April":
                    return 4;
                case "May":
                    return 5;
                case "June":
                    return 6;
                case "July":
                    return 7;
                case "August":
                    return 8;
                case "September":
                    return 9;
                case "October":
                    return 10;
                case "November":
                    return 11;
                case "December":
                    return 12;
                default:
                    throw new Exception("Month wasn't a valid month string");
            }
        }
    }
}

using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace RelativeTimeModifier
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Method to capture the update string passed in by the user
        private void Parse(string myRelativeTimeModifier)
        {
            //AI generated Regex.  Text used is as follows (missing expressions were later extrapolated)...
            //...Begins with now() with either an @ followed by the letter y or d or h or m or s or the letters mon, which are optional,
            //or an optional expression attached that contains a positive or negative number followed by the letter y and another optional
            //expression attached that contains a positive or negative number followed by the letters mon and an optional @y or optional @d
            //or optional @h or optional @m or optional @s or optional @mon

            // Please note that none of the code was AI generated!

            Regex myCheck = new Regex("^now\\(\\)((@[ydhms]|@mon)?([+-]\\d+y)?([+-]\\d+mon)?([+-]\\d+d)?([+-]\\d+h)?([+-]\\d+m)?([+-]\\d+s)?(@[ydhms]|@mon)?)?$");

            //Check that the string entered is in the correct format.  If not then display an error message.
            if (myCheck.IsMatch(myRelativeTimeModifier))
            {
                Update_Array(myRelativeTimeModifier);
            }
            else
            {
                Display_Error_Label_and_Message();
            }
        }

        // Method to store values in array based on the numerical modifer associated with the character
        // (e.g. 3y - where 'y' is the character and '3' is the numerical modifer).
        protected void Update_Array(string myRelativeTimeModifier)
        {
            // Set all array values initially to zero
            int[] myDateTimeNumbers = { 0, 0, 0, 0, 0, 0, 0 };

            // Update array based on the numerical modifer values extracted by the Identify_Modifier method
            // ( e.g. +3y becomes 3 and thus 3 is placed in postion [0] in the array)
            myDateTimeNumbers[0] = Identify_Modifier(myRelativeTimeModifier, "y");
            myDateTimeNumbers[1] = Identify_Modifier(myRelativeTimeModifier, "mon");
            myDateTimeNumbers[2] = Identify_Modifier(myRelativeTimeModifier, "d");
            myDateTimeNumbers[3] = Identify_Modifier(myRelativeTimeModifier, "h");
            myDateTimeNumbers[4] = Identify_Modifier(myRelativeTimeModifier, "m");
            myDateTimeNumbers[5] = Identify_Modifier(myRelativeTimeModifier, "s");

            Update_DateTime(myRelativeTimeModifier, myDateTimeNumbers);
        }

        // Method to update the date/time and display it, based on values stored in an array
        protected void Update_DateTime(string myRelativeTimeModifier, int[] myDateTimeNumbers)
        {
            DateTime myDatenow = DateTime.Now;
            DateTime myDateNew = myDatenow
                .AddYears(myDateTimeNumbers[0])
                .AddMonths(myDateTimeNumbers[1])
                .AddDays(myDateTimeNumbers[2])
                .AddHours(myDateTimeNumbers[3])
                .AddMinutes(myDateTimeNumbers[4])
                .AddSeconds(myDateTimeNumbers[5]);

            Display_Date_if_Valid(myRelativeTimeModifier, myDateNew);
        }

        // Method to display the date (including the snap-date where the @ sign is used)
        protected void Display_Date_if_Valid(string myRelativeTimeModifier, DateTime myDateNew)
        {
            // Displays the modified date
            lbl_time_display.Text = myDateNew.ToString("yyyy-MM-ddTHH:mm:ss.FFFZ");

            // If the string includes the '@'character then invoke the Snap_to_Relative_Time_Date method
            if (myRelativeTimeModifier.Contains("@"))
            {
                Snap_to_Relative_Time_Date(myDateNew, myRelativeTimeModifier);
            }
        }

        // Method to identify the snap character, check it is the only character and return the date in the correct format
        protected void Snap_to_Relative_Time_Date(DateTime myDateNew, string myRelativeTimeModifier)
        {
            Identify_snap_modifier(myDateNew, myRelativeTimeModifier, "y", "yyyy-00-00T00:00:00.000Z");
            Identify_snap_modifier(myDateNew, myRelativeTimeModifier, "n", "yyyy-MM-00T00:00:00.000Z");
            Identify_snap_modifier(myDateNew, myRelativeTimeModifier, "d", "yyyy-MM-ddT00:00:00.000Z");
            Identify_snap_modifier(myDateNew, myRelativeTimeModifier, "h", "yyyy-MM-ddTHH:00:00.000Z");
            Identify_snap_modifier(myDateNew, myRelativeTimeModifier, "m", "yyyy-MM-ddTHH:mm:00.000Z");
            Identify_snap_modifier(myDateNew, myRelativeTimeModifier, "s", "yyyy-MM-ddTHH:mm:ss.000Z");
        }

        protected void Identify_snap_modifier(DateTime myDateNew, string myRelativeTimeModifier, string strModifier, string strDateFormat)
        {
            if (myRelativeTimeModifier.EndsWith(strModifier))
            {
                lbl_time_display.Text = myDateNew.ToString(strDateFormat);

            }
        }

        // Method to return the numerical modifer associated with the character
        // (e.g. 3y - where y is the character).
        protected int Identify_Modifier(string myRelativeTimeModifier, string myRelativeTimeModifierCharacter)
        {
            // Regex pattern used to identify the Relative Time Modifier character
            // (e.g. 'y', 'mon', 'd', 'h', 'm' or 's')
            string pattern = @"([-]?\b+)[^-?\b]+" + myRelativeTimeModifierCharacter + @"";

            string myExtractRelativeTimeModifier = string.Empty;
            int myEndCharaterToRemove;

            string myExtractResult;
            int myEndModifier;

            // Check if input contains the snap character '@'
            switch (myRelativeTimeModifier.Contains("@"))
            {
                // If true
                case true:
                    {
                        // Find the location of the snap character
                        int index = myRelativeTimeModifier.IndexOf("@");

                        // Remove the snap character before continuing processing
                        if (index >= 0)
                            myRelativeTimeModifier = myRelativeTimeModifier.Substring(0, index);
                    }
                    break;
            }

            // Check if the modifier character is single digit
            // (e.g. 'y', 'd', 'h', 'm', 's') or 3 digits (e.g. 'mon')
            if (myRelativeTimeModifierCharacter.Length == 3)
            {
                //Remove 'mon'
                myEndCharaterToRemove = 3;
            }
            else
            {
                //Remove 'y', 'd', 'h', 'm' or 's'
                myEndCharaterToRemove = 1;
            }

            // Find and remove the identified character from the input string
            // ( e.g. Identified character = 'y' so 'now()+2y' becomes 'now()+2' )
            Regex rg = new Regex(pattern);
            MatchCollection myMatchedCharacters = rg.Matches(myRelativeTimeModifier);
            for (int count = 0; count < myMatchedCharacters.Count; count++)
                myExtractRelativeTimeModifier = myMatchedCharacters[count].Value.Remove(myMatchedCharacters[count].Value.Length - myEndCharaterToRemove);

            // If modifier is postive then run this code to remove all other character surrounding numerical modifier
            // (e.g. 'now()+2' becomes '2')            
            if (myExtractRelativeTimeModifier.Contains("+"))
            {
                myExtractResult = myExtractRelativeTimeModifier.Substring(myExtractRelativeTimeModifier.LastIndexOf('+') + 1);
                myEndModifier = int.Parse(myExtractResult);
            }
            // If modifier is negative then run this code to remove all other character surrounding numerical modifier
            // (e.g. 'now()-2' becomes '-2')
            else if (myExtractRelativeTimeModifier.Contains("-"))
            {
                myExtractResult = myExtractRelativeTimeModifier;
                myEndModifier = int.Parse(myExtractResult);
            }
            // if there are no modifiers - e.g. 'now()' - then run this code
            else
            {
                myEndModifier = 0;
            }

            // Return the final numerical modifier (e.g. +3y finally becomes 3)
            return myEndModifier;
        }

        // Method invoked when the GO! button is hit
        protected void BtnRelativeTimeModifier_Click(object sender, EventArgs e)
        {
            // If an input string has been entered then render the data display pane
            if (!(string.IsNullOrEmpty(txtRTM.Text)))
            {
                Format_Date_Display_Pane();

                //Note the input text is automatically set to lower case
                Parse(txtRTM.Text.ToLower());
            }
            // If the text box is empty then display error message
            else
            {
                Display_Error_Label_and_Message();
            }
        }

        // Method to format the date display pane.
        protected void Format_Date_Display_Pane()
        {
            lbl_time_display.ForeColor = Color.White;
            lblError.Visible = false;
        }

        // Method to display the error message
        protected void Display_Error_Label_and_Message()
        {
            lblError.Visible = true;
            lbl_time_display.ForeColor = Color.FromArgb(108, 117, 125);
        }

        //****************************************************************
        //**************** TEST FUNCTION CODE ****************************
        //****************************************************************
        protected void TimeSaverOne_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()+2y+3mon+4d+5h+6m+7s";
        }

        protected void TimeSaverTwo_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()-2y-3mon-4d-5h-6m-7s";
        }

        protected void TimeSaverThree_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()+2y-3mon+4d-5h+6m-7s";
        }

        protected void TimeSaverFour_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()+3mon+5h+7s";
        }

        protected void TimeSaverFive_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()-2y+4d-6m";
        }

        protected void TimeSaverSix_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()+2y+3mon+4d+5h+6m+7s@y";
        }

        protected void TimeSaverSeven_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()-2y-3mon-4d-5h-6m-7s@mon";
        }

        protected void TimeSaverEight_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()+2y-3mon+4d-5h+6m-7s@d";
        }

        protected void TimeSaverNine_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()+3mon+5h+7s@h";
        }

        protected void TimeSaverTen_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()-2y+4d-6m@m";
        }

        protected void TimeSaverEleven_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()@y";
        }

        protected void TimeSaverTwelve_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()@mon";
        }

        protected void TimeSaverThirteen_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()@d";
        }

        protected void TimeSaverFourteen_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()@h";
        }

        protected void TimeSaverFifteen_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()@m";
        }

        protected void TimeSaverSixteen_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()@s";
        }

        protected void TimeSaverSeventeen_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()2y+3mon+4d+5h+6m+7s";
        }

        protected void TimeSaverEighteen_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()-2y3mon-4d-5h-6m-7s";
        }

        protected void TimeSaverNineteen_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()+2y-3mo+4d-5h+6m-7s";
        }

        protected void TimeSaverTwenty_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()+3mon+5+7s";
        }

        protected void TimeSaverTwentyOne_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now+3mon+5h+7s";
        }

        protected void TimeSaverTwentyTwo_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()+2y+3mon+4d+5h+6m+7s@";
        }

        protected void TimeSaverTwentyThree_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()-2y-3mon-4d-5h-6m7s@mon";
        }

        protected void TimeSaverTwentyFour_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()+2y-3mo+4d-5h+6m-7s@d";
        }

        protected void TimeSaverTwentyFive_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now(3mon+5h+7s@h";
        }

        protected void TimeSaverTwentySix_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()+3mo+5hr+7s@m";
        }

        protected void TimeSaverTwentySeven_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()@ye";
        }

        protected void TimeSaverTwentyEight_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()@mno";
        }

        protected void TimeSaverTwentyNine_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now@d";
        }

        protected void TimeSaverThirty_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()ss@h";
        }

        protected void TimeSaverThirtyOne_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "Mow()@m";
        }

        protected void TimeSaverThirtyTwo_Click(object sender, EventArgs e)
        {
            txtRTM.Text = "now()s";
        }
    }
}
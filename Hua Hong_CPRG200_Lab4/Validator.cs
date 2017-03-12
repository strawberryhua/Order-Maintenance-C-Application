using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hua_Hong_CPRG200_Lab4
{
    /*************************
     * Description:create validation to check input
     * Date:Jan 7 ,2017
     * Authour:Hua Hong
     * Course Code:CPRG200
     **************************/
    public static class Validator
    {
        //check if the combobox input
        public static bool IsPresent(Control control)
        {
            if (control.GetType().ToString() == "System.Windows.Forms.ComboBox")
            {
                ComboBox comboBox = (ComboBox)control;
                if (comboBox.SelectedIndex == -1)
                {
                    MessageBox.Show(comboBox.Tag + " is a required field, please select OrderId.", "Entry Error");
                    comboBox.Focus();
                    return false;
                }
            }
            return true;
        }

        //check if the input is Datetime value
        public static bool IsDatetime(TextBox tb)
        {
            DateTime dt;//auxillary for trying to parse
            if (DateTime.TryParse(tb.Text, out dt))
            {
                return true;
            }
            else
            {
                MessageBox.Show(tb.Tag + " has to be Datetime value", "Input Error");
                tb.Focus();
                return false;
            }
        }

        //identify if the Datetime value is null
        public static bool IsNull(DateTime? dt)
        {
            if (dt != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //compare two  Datetime values which may have null values
        public static int CompareTo(DateTime? dt1, DateTime? dt2)
        {
            if (dt1 != null && dt2 != null)
            {
                if (dt1 > dt2)
                {
                    return 1;
                }
                else if (dt1 < dt2)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }

            return 2;
        }
    }
}

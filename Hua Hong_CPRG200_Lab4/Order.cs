using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hua_Hong_CPRG200_Lab4
{
    /*************************
     * Description:Define Order class 
     * Date:Jan 7 ,2017
     * Authour:Hua Hong
     * Course Code:CPRG200
     **************************/
    public class Order 
    {
        //default constructor (no parameters) 
        public Order() { }

        // public properties with private data automatically created
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public DateTime? OrderDate { get; set; }//can have null value
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }

        
        
    }
}

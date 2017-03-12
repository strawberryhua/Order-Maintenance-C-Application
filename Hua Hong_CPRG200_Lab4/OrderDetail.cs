using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hua_Hong_CPRG200_Lab4
{
    /*************************
     * Description:Define OrderDetail class 
     * Date:Jan 7 ,2017
     * Authour:Hua Hong
     * Course Code:CPRG200
     **************************/
    public class OrderDetail
    {
        //default constructor (no parameters) 
        public OrderDetail() { }

        // public properties with private data automatically created
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
    }
}
